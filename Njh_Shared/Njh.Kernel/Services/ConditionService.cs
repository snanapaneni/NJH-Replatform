using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DocumentEngine;
using Njh.Kernel.Constants;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models;

namespace Njh.Kernel.Services
{
    public class ConditionService : ServiceBase, IConditionService
    {
        private readonly ContextConfig contextConfig;

        private readonly ICacheService cacheService;

        public ConditionService(ContextConfig contextConfig, ICacheService cacheService)
        {
            this.contextConfig = contextConfig;
            this.cacheService = cacheService;
        }

        public IEnumerable<PageType_Condition> GetConditions(string path, int nestedLevel, string[] orderBy)
        {
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByPathByType,
                    "conditions",
                    path,
                    PageType_Condition.CLASS_NAME),
                IsCultureSpecific = true,
                CultureCode = this.contextConfig?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.contextConfig?.SiteName,
                CacheDependencies = new List<string>
                {
                    $"node|{this.contextConfig.SiteName}|{path}|childnodes",
                },
            };

            return this.cacheService.Get(
                    () => this.GetUncachedDocuments($"{path}/%", nestedLevel, orderBy),
                    cacheParameters);
        }


        private IEnumerable<PageType_Condition> GetUncachedDocuments(string path, int nestingLevel, string[] orderBy)
        {
            var toReturn =
                new DocumentQuery<PageType_Condition>()
                    .Path(path)
                    .OnSite(this.contextConfig.SiteName)
                    .Culture(this.contextConfig.CultureName)
                    .CombineWithDefaultCulture(false)
                    .PublishedVersion()
                    .Published()
                    .NestingLevel(nestingLevel)
                    .OrderBy(orderBy)
                    .ToList();

            return toReturn;
        }
    }
}
