using Njh.Kernel.Constants;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models;
using Njh.Kernel.Models.Dto;

namespace Njh.Kernel.Services
{
    public class AccordionService : BaseContentService<PageType_AccordionItem>, IAccordionService
    {
        private readonly ICacheService cacheService;
        private readonly ContextConfig contextConfig;

        public AccordionService(ICacheService cacheService, ContextConfig contextConfig)
        {
            this.cacheService = cacheService;
            this.contextConfig = contextConfig;
        }

        public IEnumerable<AccordionItem> GetItems(string path)
        {
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByPathByType,
                    "Accordions",
                    path,
                    PageType_AccordionItem.CLASS_NAME),
                IsCultureSpecific = true,
                CultureCode = this.contextConfig?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.contextConfig?.SiteName,
                CacheDependencies = new List<string>
                {
                    $"node|{this.contextConfig.SiteName}|{path}|childnodes",
                },
            };

            IEnumerable<AccordionItem> GetAccordions(CacheParameters cs)
            {
                var toReturn = this.GetUncachedItems(path)
                    .Columns(nameof(PageType_AccordionItem.NodeAlias), nameof(PageType_AccordionItem.NodeID), nameof(PageType_AccordionItem.Title), nameof(PageType_AccordionItem.Content))
                    .ToList()
                    .Select(item => new AccordionItem()
                    {
                        CodeName = item.NodeAlias, ID = item.NodeID, Content = item.Content, Title = item.Title,
                    });

                return toReturn;
            }

            return this.cacheService.Get(GetAccordions, cacheParameters);
        }
    }
}
