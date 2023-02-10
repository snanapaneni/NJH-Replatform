namespace Njh.Kernel.Services
{
    using System.Configuration;
    using CMS.Base;
    using CMS.Core;
    using CMS.DocumentEngine;
    using CMS.Helpers;
    using Njh.Kernel.Models;
    using Njh.Kernel.Models.Dto;
    using Njh.Kernel.Extensions;
    using Njh.Kernel.Constants;
    using System.Net;
    using Newtonsoft.Json;

    public class PressGaneyService : ServiceBase, IPressGaneyService
    {
        private readonly ContextConfig contextConfig;

        private readonly ICacheService cacheService;
        private readonly ISettingsKeyRepository settingsKeyRepository;
        private readonly IEventLogService eventLogService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PressGaneyService"/> class.
        /// </summary>
        /// <param name="contextConfig">
        /// The context config.
        /// </param>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        /// <param name="settingsKeyRepository">
        /// The settings repository.
        /// </param>
        public PressGaneyService(
            ContextConfig contextConfig,
            ICacheService cacheService,
            ISettingsKeyRepository settingsKeyRepository,
            IEventLogService eventLogService)
        {
            this.contextConfig = contextConfig ??
                throw new ArgumentNullException(nameof(contextConfig));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));

            this.settingsKeyRepository = settingsKeyRepository ??
                throw new ArgumentNullException(nameof(settingsKeyRepository));

            this.eventLogService = eventLogService ??
            throw new ArgumentNullException(nameof(eventLogService));
        }
        public int CacheTime()
        {
            return this.settingsKeyRepository?.GetPressGaneyCacheDuration() ?? (60 * 24);
        }

        public string RefreshCache(bool initialRefresh)
        {
           
            // This method is called on application start and by a scheduled task running every thirty minutes.  The idea
            // is to prime the cache and keep Press Ganey data available without needing to call the Press Ganey API when 
            // a page is requested.  On the initial run, cache times are randomized across 48 thirty minute periods, to 
            // prevent every record from expiring at the same time.  Subsequent runs set the cache times to 24 hours to 
            // maintain the original distribution.
            try
            {
                bool running = CacheHelper.TryGetItem("PressGaneyRefreshRunning", out bool flag);

                if (running)
                {
                    return string.Format("The Press Ganey refresh process is already running on {0}.", SystemContext.MachineName);
                }
                else
                {
                    // add a lock for protection against this task running on top of itself
                    CacheHelper.Add("PressGaneyRefreshRunning", true, null, DateTime.Now.AddMinutes(60), default);
                    this.eventLogService.LogInformation("PressGaneyHelper.RefreshCache", "Starting",
                        string.Format("The Press Ganey cache refresh process is starting on {0}...",
                        SystemContext.MachineName));

                    TreeProvider tree = new TreeProvider();
                    int numberProcessed = 0;
                    int numberCached = 0;
                    int numberErrors = 0;
                    Random rnd = new Random(DateTime.Now.Millisecond);

                    var query = new MultiDocumentQuery();

                    var path = this.settingsKeyRepository.GetPressGaneyPhysiciansPath();
                    if (!path.EndsWith("%"))
                    {
                        path = $"{path}/%";
                    }

                    var pages = query
                        .OnSite(this.contextConfig.SiteName)
                        .Path(path)
                        //.WithCoupledColumns(true)
                        .Types(this.settingsKeyRepository.GetPressGaneyPageTypes())
                        .Columns("DocumentName,NPI, NodeAliasPath, NodeGuid, NodeId")
                        .CombineWithDefaultCulture(false)
                        .Published(true)                        
                        .ToList();
                    

                    foreach (TreeNode page in pages)
                    {
                        string id = "";
                        string name = "";

                        try
                        {
                            id = page.GetStringValue("NPI", string.Empty);
                            name = page.DocumentName;

                            if (id != string.Empty) // for Person_Physician who is in not in PG this ID will be blank, so no data will be or need to be retrieved from PG
                            {
                                PressGaneyInfo pressGaneyInfo = null;
                                string cacheKey = string.Format(DataCacheKeys.PressGaneyPersonKey, id);

                                if (!CacheHelper.TryGetItem(cacheKey, out pressGaneyInfo))
                                {
                                    pressGaneyInfo = FetchRatings(id, pressGaneyInfo);
                                    pressGaneyInfo = FetchComments(id, pressGaneyInfo);

                                    // on the initial refresh randomize the next run time, after the initial run then run them each 24h.
                                    int cacheMinutes = initialRefresh ? rnd.Next(1, 49) * 30 : 1440; // 1 to 49 bc .Next() is exclusive of the upper bound
                                    CacheHelper.Add(cacheKey, pressGaneyInfo, null, DateTime.Now.AddMinutes(cacheMinutes), default);
                                    numberCached++;
                                }
                            }
                            numberProcessed++;
                        }
                        catch (Exception ex)
                        {
                            numberErrors++;
                            string error = string.Format("PressGaneyHelper.RefreshCache : {0}", ex.Source);
                            string r = string.Format("An error occurred while processing physician {0}: {1}.  Moving on to the next physician...", name, error);
                            this.eventLogService.LogException(r, "Error", ex);
                        }
                    }

                    string result = string.Format("Number of physicians processed = {0}, Number of physicians cached = {1}, Number of errors = {2}", numberProcessed, numberCached, numberErrors);
                    this.eventLogService.LogInformation("PressGaneyHelper.RefreshCache", "Results", result);

                    // remove lock 
                    CacheHelper.Remove("PressGaneyRefreshRunning", false, false, false);
                    return result;
                }
            }
            catch (Exception ex)
            {
                string error = string.Format("PressGaneyHelper.RefreshCache : {0}", ex.Source);
                string r = string.Format("A critical error occurred: {0}. The cache refresh process is shutting down.", error);
                this.eventLogService.LogException(r, "Error", ex);
                return r;
            }
        }

        public PressGaneyInfo PopulateData(string id)
        {
            PressGaneyInfo pressGaneyInfo = null;

            if (id != null)
            {
                string cacheKey = string.Format(DataCacheKeys.PressGaneyPersonKey, id);

                if (!CacheHelper.TryGetItem(cacheKey, out pressGaneyInfo))
                {
                    pressGaneyInfo = FetchAllPressGaneyData(id, pressGaneyInfo);
                    CacheHelper.Add(cacheKey, pressGaneyInfo, null, DateTime.Now.AddMinutes(this.CacheTime()), default);
                }
            }

            return pressGaneyInfo;
        }

        public PressGaneyInfo FetchAllPressGaneyData(string id, PressGaneyInfo pressGaneyInfo)
        {
            if (pressGaneyInfo == null || !pressGaneyInfo.RetrievedRatings)
            {
                pressGaneyInfo = this.FetchRatings(id, pressGaneyInfo);
            }

            if (pressGaneyInfo == null || !pressGaneyInfo.RetrievedComments) pressGaneyInfo = FetchComments(id, pressGaneyInfo);
            return pressGaneyInfo;
        }

        public PressGaneyInfo PopulateCommentData(string id)
        {
            if (id != null)
            {
                return PopulateData(id);
            }

            return null;
        }

        public PressGaneyInfo PopulateRatingData(string id)
        {
            if (id != null)
            {
                return PopulateData(id);
            }
            return null;
        }

        private PressGaneyInfo FetchInternal(string id)
        {
            string authToken = this.Login();

            if (authToken == null)
            {
                throw (new ApplicationException("Not able to login/get token."));
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri(this.settingsKeyRepository.GetPressGaneyBaseUrl());
            client.DefaultRequestHeaders.Add("Access-Token", authToken);

            HttpResponseMessage response = new HttpResponseMessage();
            List<string> querystring = new List<string>();
            string format = "{0}={1}";



            querystring.Add(string.Format(format, "personId", id));
            querystring.Add(string.Format(format, "page", "1"));
            querystring.Add(string.Format(format, "perPage", "1000")); // 1000 should be enough to get all, will handle paging as needed
            querystring.Add(string.Format(format, "days", "548")); // 1.5 years or 18 months
            querystring.Add(string.Format(format, "minSurveyCount", "0"));
            querystring.Add(string.Format(format, "minRatingCount", this.settingsKeyRepository.GetPressGaneyMinRatingCount()));
            querystring.Add(string.Format(format, "showRatings", "true"));
            querystring.Add(string.Format(format, "showComments", "true"));

            try
            {
                response = client.GetAsync(this.settingsKeyRepository.GetPressGaneyCommentsAndRatingsURLPath() + "?" + querystring.Join("&")).Result;
            }
            catch (System.Net.WebException wex)
            {
                string x = wex.Message;
                return null;
            }

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                // parse json and get token.
                dynamic responseJson = JsonConvert.DeserializeObject(result);

                if (responseJson.status != null && responseJson.status.code != null && responseJson.status.code == 200)
                {
                    if (responseJson.data != null && responseJson.data.entities != null && responseJson.data.entities.Count > 0)
                    {
                        dynamic entity = responseJson.data.entities[0];

                        PressGaneyInfo pg = new PressGaneyInfo();

                        pg.Id = id;
                        pg.Name = entity.name != null ? entity.name : "";
                        pg.RatingCount = entity.totalRatingCount != null ? entity.totalRatingCount : "";

                        //check min count threshhold..in that case treat as an empty record
                        if (pg.RatingCount < this.settingsKeyRepository.GetPressGaneyMinRatingCount())
                        {
                            return null;
                        }

                        pg.SurveyCount = entity.totalSurveyCount != null ? entity.totalSurveyCount : string.Empty;
                        pg.OverallRating = entity.overallRating != null ? new PressGaneyRatingInfo() { Name = entity.overallRating.name, Value = entity.overallRating.value, Count = pg.RatingCount } : null;

                        // To Do ratings
                        pg.Ratings = new List<PressGaneyRatingInfo>();
                        if (entity.overallRating.questionRatings != null && entity.overallRating.questionRatings.Count > 0)
                        {
                            foreach (dynamic rating in entity.overallRating.questionRatings)
                            {
                                var pgr = new PressGaneyRatingInfo();
                                pgr.Name = rating.name;
                                pgr.Name = NameMapper(pgr.Name); // fix names
                                pgr.Value = rating.value;
                                pgr.Count = rating.responseCount;

                                pg.Ratings.Add(pgr);
                            }
                        }

                        if (entity.comments != null && entity.comments.Count > 0)
                        {
                            pg.Comments = new List<PressGaneyCommentInfo>();
                            foreach (dynamic comment in entity.comments)
                            {
                                var pgc = new PressGaneyCommentInfo();

                                pgc.Comment = comment.comment;
                                pgc.MentionTime = Convert.ToDateTime(comment.mentionTime);
                                // single rating stored with the comment...hard code count to 1
                                pgc.OverallRating = comment.overallRating != null ? new PressGaneyRatingInfo() { Name = comment.overallRating.name, Value = comment.overallRating.value, Count = 1 } : null;
                                pgc.Source = comment.source;
                                pgc.WordCount = comment.wordCount;

                                pg.Comments.Add(pgc);
                            }
                        }

                        // sorting
                        if (pg.Comments != null)
                        {
                            pg.Comments = pg.Comments.OrderByDescending(o => o.MentionTime).ToList();
                        }

                        return pg;
                    }
                }
                else
                {
                    throw (new AccessViolationException("Not able to authenticate to service"));
                }

            }
            return null;

        }

        private PressGaneyInfo FetchRatings(string id, PressGaneyInfo pg)
        {
            string authToken = this.Login();

            if (authToken == null)
            {
                throw (new ApplicationException("Not able to login/get token."));
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri(this.settingsKeyRepository.GetPressGaneyBaseUrl());
            client.DefaultRequestHeaders.Add("Access-Token", authToken);

            HttpResponseMessage response = new HttpResponseMessage();
            List<string> querystring = new List<string>();
            string format = "{0}={1}";

            querystring.Add(string.Format(format, "personId", id));
            querystring.Add(string.Format(format, "page", "1"));
            querystring.Add(string.Format(format, "perPage", "1000")); // 1000 should be enough to get all, will handle paging as needed
            querystring.Add(string.Format(format, "days", "548")); // 1.5 years or 18 months
            querystring.Add(string.Format(format, "minSurveyCount", "30"));
            querystring.Add(string.Format(format, "minRatingCount", this.settingsKeyRepository.GetPressGaneyMinRatingCount()));
            querystring.Add(string.Format(format, "showRatings", "true"));
            querystring.Add(string.Format(format, "showComments", "false"));

            try
            {
                response = client.GetAsync($"{this.settingsKeyRepository.GetPressGaneyCommentsAndRatingsURLPath()}?{querystring.Join("&")}").Result;
            }
            catch (System.Net.WebException wex)
            {
                string x = wex.Message;
                return null;
            }

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                // parse json and get token.
                dynamic responseJson = JsonConvert.DeserializeObject(result);

                if (responseJson.status != null && responseJson.status.code != null && responseJson.status.code == 200)
                {
                    if (responseJson.data != null && responseJson.data.entities != null && responseJson.data.entities.Count > 0)
                    {
                        dynamic entity = responseJson.data.entities[0];

                        if (pg == null)
                        {
                            pg = new PressGaneyInfo();
                            pg.RetrievedComments = false;
                        }

                        pg.Id = id;
                        pg.Name = entity.name != null ? entity.name : "";
                        pg.RatingCount = entity.totalRatingCount != null ? entity.totalRatingCount : "";

                        pg.RetrievedRatings = true;

                        if (pg.RatingCount >= this.settingsKeyRepository.GetPressGaneyMinRatingCount())
                        {
                            pg.SurveyCount = entity.totalSurveyCount != null ? entity.totalSurveyCount : "";
                            pg.OverallRating = entity.overallRating != null ? new PressGaneyRatingInfo() { Name = entity.overallRating.name, Value = entity.overallRating.value, Count = pg.RatingCount } : null;

                            pg.Ratings = new List<PressGaneyRatingInfo>();
                            if (entity.overallRating.questionRatings != null && entity.overallRating.questionRatings.Count > 0)
                            {
                                foreach (dynamic rating in entity.overallRating.questionRatings)
                                {
                                    var pgr = new PressGaneyRatingInfo();
                                    pgr.Name = rating.name;
                                    pgr.Name = NameMapper(pgr.Name); // fix names
                                    pgr.Value = rating.value;
                                    pgr.Count = rating.responseCount;
                                    pg.Ratings.Add(pgr);
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw (new AccessViolationException("Not able to authenticate to service"));
                }
            }
            return pg;
        }

        private PressGaneyInfo FetchComments(string id, PressGaneyInfo pg)
        {
            // Ratings must be retireved first, as we will only display comments if there are 30 or more ratings/surveys in last 18 months...
            if (pg == null || pg.RetrievedRatings == false || pg.RatingCount < 30 || pg.SurveyCount < 30) return pg;

            string authToken = this.Login();

            if (authToken == null)
            {
                throw (new ApplicationException("Not able to login/get token."));
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri(this.settingsKeyRepository.GetPressGaneyBaseUrl());
            client.DefaultRequestHeaders.Add("Access-Token", authToken);

            HttpResponseMessage response = new HttpResponseMessage();
            List<string> querystring = new List<string>();
            string format = "{0}={1}";

            querystring.Add(string.Format(format, "personId", id));
            querystring.Add(string.Format(format, "page", "1"));
            querystring.Add(string.Format(format, "perPage", "1000")); // 1000 should be enough to get all, will handle paging as needed
            querystring.Add(string.Format(format, "days", this.settingsKeyRepository.GetPressGaneyCommentFilter()));
            querystring.Add(string.Format(format, "minSurveyCount", "30"));
            querystring.Add(string.Format(format, "minRatingCount", this.settingsKeyRepository.GetPressGaneyMinRatingCount()));
            querystring.Add(string.Format(format, "showRatings", "false"));
            querystring.Add(string.Format(format, "showComments", "true"));

            try
            {
                response = client.GetAsync(this.settingsKeyRepository.GetPressGaneyCommentsAndRatingsURLPath() + "?" + querystring.Join("&")).Result;
            }
            catch (System.Net.WebException wex)
            {
                string x = wex.Message;
                return null;
            }

            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                // parse json and get token.
                dynamic responseJson = JsonConvert.DeserializeObject(result);

                if (responseJson.status != null && responseJson.status.code != null && responseJson.status.code == 200)
                {
                    if (responseJson.data != null && responseJson.data.entities != null && responseJson.data.entities.Count > 0)
                    {
                        dynamic entity = responseJson.data.entities[0];

                        pg.RetrievedComments = true;
                        pg.Id = id;
                        pg.Name = entity.name != null ? entity.name : "";

                        if (entity.comments != null && entity.comments.Count > 0)
                        {
                            pg.Comments = new List<PressGaneyCommentInfo>();
                            foreach (dynamic comment in entity.comments)
                            {
                                var pgc = new PressGaneyCommentInfo();
                                pgc.Comment = comment.comment;
                                pgc.MentionTime = Convert.ToDateTime(comment.mentionTime);
                                // single rating stored with the comment...hard code count to 1
                                pgc.OverallRating = comment.overallRating != null ? new PressGaneyRatingInfo() { Name = comment.overallRating.name, Value = comment.overallRating.value, Count = 1 } : null;
                                pgc.Source = comment.source;
                                pgc.WordCount = comment.wordCount;
                                pg.Comments.Add(pgc);
                            }
                        }

                        // sorting
                        if (pg.Comments != null)
                        {
                            pg.Comments = pg.Comments.OrderByDescending(o => o.MentionTime).ToList();
                        }
                    }
                }
                else
                {
                    throw (new AccessViolationException("Not able to authenticate to service"));
                }
            }
            return pg;
        }

        /// <summary>
        /// Names stored in the PG JSON are not friendly ...map them to friendly names
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string NameMapper(string name)
        {
            var stringList = new Dictionary<string, string>();

            stringList.Add("Friendliness/courtesy of CP", "Friendliness");
            stringList.Add("CP explanations of prob/condition", "Explanation of problem/condition");
            stringList.Add("CP concern for questions/worries", "Shows concerns for your questions");
            stringList.Add("CP efforts to include in decisions", "Includes you in decisions");
            stringList.Add("CP spoke using clear language", "Spoke using clear language");
            stringList.Add("Time CP spent with patient", "Amount of time spent with you");
            stringList.Add("Patients' confidence in CP", "Confidence in your doctor");
            stringList.Add("Likelihood of recommending CP", "Recommended to others");
            //no map - use as is - Wait times	
            //no map - use as is - Listens	
            //no map - use as is - Knows Medical History	
            //no map - use as is - Patient Rating of Doctor	

            if (stringList.ContainsKey(name))
            {
                return stringList[name];
            }

            return name;
        }

        private string Login()
        {
            string token = "";
            CacheHelper.TryGetItem(DataCacheKeys.PressGaneyTokenKey, out token);

            if (token == null)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(this.settingsKeyRepository.GetPressGaneyBaseUrl());

                HttpResponseMessage response = new HttpResponseMessage();

                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("appId", this.settingsKeyRepository.GetPressGaneyApplicationId()),
                new KeyValuePair<string, string>("appSecret", this.settingsKeyRepository.GetPressGaneyApplicationSecret())
            });

                try
                {
                    response = client.PostAsync(this.settingsKeyRepository.GetPressGaneyAuthenticationUrl(), formContent).Result;
                }
                catch (System.Net.WebException wex)
                {
                    string x = wex.Message;
                    return string.Empty;
                }

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    // parse json and get token.
                    dynamic responseJson = JsonConvert.DeserializeObject(result);

                    if (responseJson.status != null && responseJson.status.code != null && responseJson.status.code == 200)
                    {
                        if (responseJson.accessToken != null)
                        {
                            token = responseJson.accessToken;

                            System.TimeZoneInfo tzi = System.TimeZoneInfo.FindSystemTimeZoneById(this.settingsKeyRepository.GetPressGaneyServerTimeZoneString());
                            DateTime localDateTime = System.TimeZoneInfo.ConvertTimeFromUtc(ValidationHelper.GetDateTime(responseJson.expiresIn, DateTime.MinValue), tzi);
                            DateTime expiry = localDateTime.AddSeconds(-120); // shift 2 min... to prevent early terminations due to clock variance

                            CacheHelper.Add(DataCacheKeys.PressGaneyTokenKey, token, null, expiry, TimeSpan.Zero);
                            return token;
                        }
                    }
                    else
                    {

                        throw (new AccessViolationException("Not able to Authenticate to Service:"));
                    }



                }
                return null;
            }
            return token;
        }
    }
}
