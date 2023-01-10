using CMS.CustomTables;
using CMS.DocumentEngine;
using Njh.Kernel.Constants;
using Njh.Kernel.Kentico.Models.CustomTables;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models;
using ReasonOne.KenticoXperience.Extensions;
namespace Njh.Kernel.Services
{
    public class ProfessionalTitleService : ServiceBase, IProfessionalTitleService
    {
        private readonly ContextConfig contextConfig;

        private readonly ICacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ProfessionalTitleService"/> class.
        /// </summary>
        /// <param name="contextConfig">
        /// The context config.
        /// </param>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        public ProfessionalTitleService(
            ContextConfig contextConfig,
            ICacheService cacheService)
        {
            this.contextConfig = contextConfig ??
                throw new ArgumentNullException(nameof(contextConfig));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));
        }

        public IEnumerable<CustomTable_ProfessionalTitleItem> GetProfessionalTitles()
        {
            throw new NotImplementedException();
        }

        public List<string> GetProfessionalTitlesByGuids(params Guid[] professionalTitlesGuids)
        {


            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByTableName,
                    "ProfessionalTitles",
                    CustomTable_ProfessionalTitleItem.CLASS_NAME),
                IsCultureSpecific = true,
                CultureCode = this.contextConfig?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.contextConfig?.SiteName,
                CacheDependencies = new List<string>
                    {
                        string.Format(
                            DummyCacheKeys.CustomTableItemsAll,
                            CustomTable_SectionColorThemesItem.CLASS_NAME),
                    },
            };

            var professionalTitlesDictionary = this.cacheService.Get(
                cp => GetProfessionalTitleItems()
                        .Select(pt => new KeyValuePair<Guid, string>(pt.ItemGUID, pt.Title))
                        .ToDictionary(p => p.Key, p => p.Value),
                cacheParameters);


            var results = professionalTitlesDictionary
                .Where(item => professionalTitlesGuids
                .Any(s => s.Equals(item.Key)))
                .Select(s => s.Value)
                .ToList();

            return results;
        }

        /// <summary>
        /// Returns all Professional Title Items straight from the database (no cache).
        /// </summary>
        /// <returns></returns>

        public IEnumerable<CustomTable_ProfessionalTitleItem> GetProfessionalTitleItems()
        {
            return CustomTableItemProvider
            .GetItems<CustomTable_ProfessionalTitleItem>();
        }


    }
}
