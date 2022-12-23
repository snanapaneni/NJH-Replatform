using CMS.CustomTables;
using Njh.Kernel.Constants;
using Njh.Kernel.Kentico.Models.CustomTables;
using Njh.Kernel.Models;

namespace Njh.Kernel.Services
{
    public class SectionThemeService : ServiceBase, ISectionThemeService
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
        public SectionThemeService(
            ContextConfig contextConfig,
            ICacheService cacheService)
        {
            this.contextConfig = contextConfig ??
                throw new ArgumentNullException(nameof(contextConfig));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));
        }
        internal CustomTable_SectionColorThemesItem GetThemesItemByGuid(Guid themeGuid)
        {
            return CustomTableItemProvider
              .GetItems<CustomTable_SectionColorThemesItem>()
              .WithGuid(themeGuid)
              .ToList()
              .FirstOrDefault();
        }
        public CustomTable_SectionColorThemesItem GetThemesItemByGuid(Guid themeGuid, bool cache = true)
        {
            if (!cache)
            {
                return GetThemesItemByGuid(themeGuid);
            }
            // Todo: update cache dependency to be on item id
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                   DataCacheKeys.DataSetByKey,
                   "sectionthemes",
                   themeGuid.ToString()),
                IsCultureSpecific = false,
                IsSiteSpecific = false,
                CacheDependencies = new List<string>
                    {
                        string.Format(
                            DummyCacheKeys.CustomTableItemsAll,
                            CustomTable_SectionColorThemesItem.CLASS_NAME),
                    },
            };

            var result = this.cacheService.Get(
                cp => GetThemesItemByGuid(themeGuid), cacheParameters);

            return result;
            
        }
    public IEnumerable<CustomTable_SectionColorThemesItem> GetThemesItems()
    {
        return CustomTableItemProvider
            .GetItems<CustomTable_SectionColorThemesItem>();
    }
}
}
