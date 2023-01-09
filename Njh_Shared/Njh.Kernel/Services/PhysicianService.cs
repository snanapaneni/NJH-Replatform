using CMS.CustomTables;
using CMS.DocumentEngine;
using Njh.Kernel.Constants;
using Njh.Kernel.Kentico.Models.CustomTables;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models;
using ReasonOne.KenticoXperience.Extensions;
namespace Njh.Kernel.Services
{
    public class PhysicianService : ServiceBase, IPhysicianService
    {
        private readonly ContextConfig contextConfig;

        private readonly ICacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PhysicianService"/> class.
        /// </summary>
        /// <param name="contextConfig">
        /// The context config.
        /// </param>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        public PhysicianService(
            ContextConfig contextConfig,
            ICacheService cacheService)
        {
            this.contextConfig = contextConfig ??
                throw new ArgumentNullException(nameof(contextConfig));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));
        }

        public IEnumerable<PageType_Physician> GetPhysicians()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PageType_Physician> GetPhysiciansByGuids(bool cached = true, bool published = true, params Guid[] physiciansGuids)
        {
            var cacheKey =
             physiciansGuids.Length > 0
                 ? physiciansGuids
                     .OrderBy(guid => guid)
                     .Select(guid => guid.ToString())
                     .Aggregate((current, next) => current + next)
                 : string.Empty;


            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByKey,
                    "physicians",
                    cacheKey),
                IsCultureSpecific = true,
                CultureCode = this.contextConfig?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.contextConfig?.SiteName,
                CacheDependencies = physiciansGuids
                    .Select(guid =>
                        string.Format(
                            DummyCacheKeys.PageSiteNodeGuid,
                            this.contextConfig?.SiteName,
                            guid))
                    .ToList(),
            };

            return published ?
                this.cacheService.Get(
                    () => this.GetPhysiciansByGuids(published, physiciansGuids),
                    cacheParameters)
                : this.GetPhysiciansByGuids(published, physiciansGuids: physiciansGuids);

        }

        /// <summary>
        /// Returns all Physicians straight from the database (no cache).
        /// </summary>
        /// <returns></returns>

        private IEnumerable<PageType_Physician> GetPhysiciansByGuids(bool published = true, params Guid[] physiciansGuids)
        {
            var physicians =
                new DocumentQuery<PageType_Physician>()
                .OnSite(this.contextConfig.SiteName)
                .Culture(this.contextConfig.CultureName)
                .CombineWithDefaultCulture(false)
                .PublishedVersion(published)
                .Published(published)
                .WhereNodeGuidIn(physiciansGuids)
                .ToList();

            return physicians;
        }


    }
}
