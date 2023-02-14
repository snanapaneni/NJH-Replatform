namespace Njh.Kernel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Njh.Kernel.Constants;
    using Njh.Kernel.Kentico.Models.PageTypes;
    using Njh.Kernel.Models;

    public class AlertService : BaseContentService<PageType_Alert>, IAlertService
    {
        private readonly ContextConfig contextConfig;

        private readonly ICacheService cacheService;

        public AlertService(ICacheService cacheService, ContextConfig contextConfig)
        {
            this.cacheService = cacheService;
            this.contextConfig = contextConfig;
        }

        public PageType_Alert FirstOrDefault(string path)
        {
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByKey,
                    "Alert",
                    PageType_Alert.CLASS_NAME),
                IsCultureSpecific = true,
                CultureCode = this.contextConfig?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.contextConfig?.SiteName,
            };

            Func<CacheParameters, PageType_Alert> GetAlert = cs =>
                        {
                            var toReturn = GetUncachedItems(path).TopN(1).FirstOrDefault();

                            if (toReturn != null)
                            {
                                cs.CacheDependencies = new List<string>
                                {
                                    $"nodeid|{toReturn.NodeID}",
                                    $"node|{this.contextConfig.SiteName}|{path}|childnodes",
                                    DummyCacheKeys.NodeOrder,
                                };
                            }

                            return toReturn;
                        };
            return this.cacheService.Get(GetAlert, cacheParameters);
        }
    }
}
