using CMS.DocumentEngine;
using CMS.Localization;
using CMS.SiteProvider;
using Njh.Kernel.Extensions;
using Njh.Kernel.Models;
using SimpleMvcSitemap;

namespace Njh.Kernel.Services
{
    /// <summary>
    /// Implements a xml sitemap service.
    /// </summary>
    public class SitemapService
        : ServiceBase, ISitemapService
    {
        private readonly ICacheService cacheService;
        private readonly ISettingsKeyRepository settingsKeyRepository;
        private readonly ContextConfig context;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SitemapService"/> class.
        /// </summary>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <param name="context">
        /// The context config.
        /// </param>
        public SitemapService(
            ICacheService cacheService,
            ISettingsKeyRepository settingsKeyRepository,
            ContextConfig context)
            : base()
        {
            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));

            this.settingsKeyRepository = settingsKeyRepository ??
                throw new ArgumentNullException(nameof(settingsKeyRepository));

            this.context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Returns the sitemap.
        /// </summary>
        /// <returns>The sitemap.</returns>
        public SitemapModel GetXmlSitemapPages()
        {
            var cacheParameters = new CacheParameters
            {
                CacheKey = $"sitemap|{this.context.CultureName}",
                IsCultureSpecific = true,
                CultureCode = this.context.CultureName,
                IsSiteSpecific = true,
                SiteName = this.context.SiteName,
                CacheDependencies = new List<string>
                {
                    $"node|this.context.SiteName|/",
                },
            };

            var pageTypes = this.settingsKeyRepository.GetPagePageTypes()?.ToLower()?.Split(new char[] { ';' });

            var result = this.cacheService.Get(
                () =>
                {
                    return new SitemapModel(
                        DocumentHelper
                            .GetDocuments()
                            .OnSite(SiteContext.CurrentSiteName)
                            .Culture(LocalizationContext.CurrentCulture.CultureCode)
                            .Published(true)
                            .Types(pageTypes.ToArray())
                            .OrderBy("NodeLevel", "NodeOrder")
                            .Select(page =>
                                new SitemapNode(DocumentURLProvider.GetUrl(page).TrimStart('~'))
                                {
                                    LastModificationDate = page.DocumentModifiedWhen.ToLocalTime(),
                                })
                            .ToList());
                },
                cacheParameters);

            return result;
        }
    }
}
