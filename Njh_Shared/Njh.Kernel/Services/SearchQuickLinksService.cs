using CMS.CustomTables;
using Njh.Kernel.Constants;
using Njh.Kernel.Kentico.Models.CustomTables;
using Njh.Kernel.Models;
using Njh.Kernel.Models.Dto;

namespace Njh.Kernel.Services
{
    public class SearchQuickLinksService : ServiceBase, ISearchQuickLinksService
    {
        private readonly ContextConfig contextConfig;

        private readonly ICacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SectionThemeService"/> class.
        /// </summary>
        /// <param name="contextConfig">
        /// The context config.
        /// </param>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        public SearchQuickLinksService(
            ContextConfig contextConfig,
            ICacheService cacheService)
        {
            this.contextConfig = contextConfig ??
                throw new ArgumentNullException(nameof(contextConfig));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));
        }

        public IEnumerable<SimpleLink> GetSearchQuickSimpleLinks(bool enabled = true, bool cached = true)
        {
            if (!cached)
            {
                return GetSearchQuickLinks()
                    .Where(i => i.Enabled)
                    .OrderBy(i => i.ItemOrder)
                    .Select(item => new SimpleLink()
                    {
                        Text = item.DisplayText,
                        Link = item.Url
                    });
            }
            // Todo: update cache dependency to be on item id
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                   DataCacheKeys.DataSetByTableName,
                   "searchquicklinkssimplelinks",
                   CustomTable_SearchQuickLinksItem.CLASS_NAME),
                IsCultureSpecific = false,
                IsSiteSpecific = false,
                CacheDependencies = new List<string>
                    {
                        string.Format(
                            DummyCacheKeys.CustomTableItemsAll,
                            CustomTable_SearchQuickLinksItem.CLASS_NAME),
                    },
            };

            var result = this.cacheService.Get(
                cp => GetSearchQuickLinks()
                .Where(i => i.Enabled)
                .OrderBy(i => i.ItemOrder)
                .Select(item => new SimpleLink()
                {
                    Text = item.DisplayText,
                    Link = item.Url
                }), cacheParameters);

            return result;

        }
        public IEnumerable<CustomTable_SearchQuickLinksItem> GetSearchQuickLinks(bool enabled = true, bool cached = true)
        {
            if (!cached)
            {
                return GetSearchQuickLinks().Where(i => i.Enabled);
            }

            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                   DataCacheKeys.DataSetByTableName,
                   "searchquicklinks",
                   CustomTable_SearchQuickLinksItem.CLASS_NAME),
                IsCultureSpecific = false,
                IsSiteSpecific = false,
                CacheDependencies = new List<string>
                    {
                        string.Format(
                            DummyCacheKeys.CustomTableItemsAll,
                            CustomTable_SearchQuickLinksItem.CLASS_NAME),
                    },
            };

            var result = this.cacheService.Get(
                cp => GetSearchQuickLinks()
                .Where(i => i.Enabled), cacheParameters);

            return result;
        }


        /// <summary>
        /// Returns all Section Color Themes table items straight from the database (no cache).
        /// </summary>
        /// <returns></returns>
        private IEnumerable<CustomTable_SearchQuickLinksItem> GetSearchQuickLinks()
        {
            return CustomTableItemProvider
                .GetItems<CustomTable_SearchQuickLinksItem>();
        }

    }
}





