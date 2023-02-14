namespace Njh.Kernel.Services
{
    using CMS.DocumentEngine;
    using CMS.Helpers;
    using Njh.Kernel.Constants;
    using Njh.Kernel.Kentico.Models.PageTypes;
    using Njh.Kernel.Models;
    using Njh.Kernel.Models.Dto;

    /// <inheritdoc/>
    public class PatientClassService : ServiceBase, IPatientClassService
    {
        private readonly ICacheService cacheService;
        private readonly ContextConfig context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientClassService"/> class.
        /// </summary>
        /// <param name="cacheService">The cache service.</param>
        /// <param name="context">The context configuration.</param>
        public PatientClassService(ICacheService cacheService, ContextConfig context)
        {
            this.cacheService = cacheService;
            this.context = context;
        }

        /// <inheritdoc/>
        public IEnumerable<PatientClass> GetPatientClasses(string path)
        {
            var pageType = PageType_PatientEducation.CLASS_NAME;

            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByPathByType,
                    "PatientClasses",
                    path,
                    pageType),
                IsCultureSpecific = true,
                CultureCode = this.context?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.context?.Site?.SiteName,
            };

            var cachedData = this.cacheService.Get(
                cp =>
                {
                    var documents = DocumentHelper.GetDocuments()
                        .OnCurrentSite()
                        .CombineWithDefaultCulture()
                        .Published()
                        .WithCoupledColumns()
                        .LatestVersion()
                        .Path(path, PathTypeEnum.Children)
                        .Type(pageType)
                        .OrderBy("NodeOrder")
                        .ToList();

                    var patientClasses = new List<PatientClass>();

                    // TODO populate each PatientClass using fields:
                    // Title, Class_Days, Date_Info, Fees, Location, Summary
                    // and generate URL to document
                    foreach (var doc in documents)
                    {
                        var classDays = doc.GetStringValue("Class_Days", string.Empty).Split(new char[] { ';', ',', '|' });
                        patientClasses.Add(new PatientClass()
                        {
                            Title = doc.GetStringValue("Title", string.Empty),
                            ClassDays = classDays,
                            DateInfo = doc.GetStringValue("Date_Info", string.Empty),
                            Summary = doc.GetStringValue("Summary", string.Empty),
                            Fees = doc.GetStringValue("Fees", string.Empty),
                            Location = doc.GetStringValue("Location", string.Empty),
                            Url = DocumentURLProvider.GetAbsoluteUrl(doc),
                        });
                    }

                    // set dependency: all documents in list should bust cache
                    cp.CacheDependencies = documents.Select(item =>
                            this.cacheService.GetCacheKey(
                            string.Format(DummyCacheKeys.PageSiteNodeAlias, this.context?.Site?.SiteName, item.NodeAliasPath),
                            cacheParameters.CultureCode,
                            cacheParameters.SiteName))
                            .ToList();

                    return patientClasses;
                }, cacheParameters);

            return cachedData;
        }
    }
}
