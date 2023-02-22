using CMS.CustomTables;
using Njh.Kernel.Constants;
using Njh.Kernel.Kentico.Models.CustomTables;
using Njh.Kernel.Models;
using Njh.Kernel.Models.Dto;

namespace Njh.Kernel.Services
{
    /// <inheritdoc/>
    public class JobTitleService : ServiceBase, IJobTitleService
    {
        private readonly ContextConfig contextConfig;

        private readonly ICacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="JobTitleService"/> class.
        /// </summary>
        /// <param name="contextConfig">
        /// The context config.
        /// </param>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        public JobTitleService(
            ContextConfig contextConfig,
            ICacheService cacheService)
        {
            this.contextConfig = contextConfig ??
                throw new ArgumentNullException(nameof(contextConfig));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));
        }

        /// <inheritdoc/>
        public IEnumerable<SimpleLink> GetJobTitleSimpleLinks(bool cached = true)
        {
            if (!cached)
            {
                return this.GetJobTitles()
                    .OrderBy(i => i.ItemOrder)
                    .Select(item => new SimpleLink()
                    {
                        Text = item.Title,
                        Link = item.Url,
                    });
            }
            // Todo: update cache dependency to be on item id
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                   DataCacheKeys.DataSetByTableName,
                   "jobtitlessimplelinks",
                   CustomTable_JobTitleItem.CLASS_NAME),
                IsCultureSpecific = false,
                IsSiteSpecific = false,
                CacheDependencies = new List<string>
                    {
                        string.Format(
                            DummyCacheKeys.CustomTableItemsAll,
                            CustomTable_JobTitleItem.CLASS_NAME),
                    },
            };

            var result = this.cacheService.Get(
                cp => GetJobTitles()
                .OrderBy(i => i.ItemOrder)
                .Select(item => new SimpleLink()
                {
                    Text = item.Title,
                    Link = item.Url
                }), cacheParameters);

            return result;
        }

        /// <inheritdoc/>
        public IEnumerable<CustomTable_JobTitleItem> GetJobTitles(bool cached = true)
        {
            if (!cached)
            {
                return this.GetJobTitles();
            }

            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                   DataCacheKeys.DataSetByTableName,
                   "searchquicklinks",
                   CustomTable_JobTitleItem.CLASS_NAME),
                IsCultureSpecific = false,
                IsSiteSpecific = false,
                CacheDependencies = new List<string>
                    {
                        string.Format(
                            DummyCacheKeys.CustomTableItemsAll,
                            CustomTable_JobTitleItem.CLASS_NAME),
                    },
            };

            var result = this.cacheService.Get(
                cp => this.GetJobTitles(), cacheParameters);

            return result;
        }


        /// <summary>
        /// Returns all Job Title table items straight from the database (no cache).
        /// </summary>
        /// <returns>Enumerable JobTitleItems</returns>
        private IEnumerable<CustomTable_JobTitleItem> GetJobTitles()
        {
            return CustomTableItemProvider
                .GetItems<CustomTable_JobTitleItem>();
        }
    }
}
