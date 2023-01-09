using Njh.Kernel.Constants;
using Njh.Kernel.Extensions;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models;
using CMS.DocumentEngine;

namespace Njh.Kernel.Services
{
    /// <summary>
    /// Implements the service to work with
    /// <see cref="PageType_Page"/> documents.
    /// </summary>
    public class PageService
        : ServiceBase, IPageService
    {
        private readonly ContextConfig contextConfig;

        private readonly ICacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PageService"/> class.
        /// </summary>
        /// <param name="contextConfig">
        /// The context config.
        /// </param>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        public PageService(
            ContextConfig contextConfig,
            ICacheService cacheService)
        {
            this.contextConfig = contextConfig ??
                throw new ArgumentNullException(nameof(contextConfig));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));
        }

        /// <summary>
        /// Gets the document by ID.
        /// </summary>
        /// <param name="nodeGuid">
        /// The GUID of the document.
        /// </param>
        /// <param name="published">
        /// If only published documents should be returned.
        /// </param>
        /// <returns>
        /// The document. null, otherwise.
        /// </returns>
        public PageType_Page GetDocument(
            Guid nodeGuid,
            bool published = true)
        {
            return this
                .GetDocuments(
                    published,
                    new Guid[] { nodeGuid })
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the documents by IDs.
        /// </summary>
        /// <param name="published">
        /// If only published documents should be returned.
        /// </param>
        /// <param name="nodeGuids">
        /// The GUID of the documents.
        /// </param>
        /// <returns>
        /// The documents. Empty enumerable, otherwise.
        /// </returns>
        public IEnumerable<PageType_Page> GetDocuments(
            bool published = true,
            params Guid[] nodeGuids)
        {
            var cacheKey =
                nodeGuids.Length > 0
                    ? nodeGuids
                        .OrderBy(guid => guid)
                        .Select(guid => guid.ToString())
                        .Aggregate((current, next) => current + next)
                    : string.Empty;

            // TODO: replace with the data key
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByKey,
                    "documents",
                    cacheKey),
                IsCultureSpecific = true,
                CultureCode = this.contextConfig?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.contextConfig?.SiteName,
                CacheDependencies = nodeGuids
                    .Select(guid =>
                        string.Format(
                            DummyCacheKeys.PageSiteNodeGuid,
                            this.contextConfig?.SiteName,
                            guid))
                    .ToList(),
            };

            return published ?
                this.cacheService.Get(
                    () => this.GetUncachedDocuments(published, nodeGuids),
                    cacheParameters)
                : this.GetUncachedDocuments(published, nodeGuids);
        }

        /// <summary>
        /// Get coupled data with the document.
        /// </summary>
        /// <param name="nodeGuid">
        /// The GUID of the document.
        /// </param>
        /// <param name="published">
        /// If only published documents should be returned.
        /// </param>
        /// <returns>
        /// The documents. Empty enumerable, otherwise.
        /// </returns>
        public PageType_Page GetDocumentWithCoupledData(
            Guid nodeGuid,
            bool published = true)
        {
            var query =
                new MultiDocumentQuery();

            var document = query
                .OnSite(this.contextConfig.SiteName)
                .Type(PageType_Page.CLASS_NAME)
                .WithCoupledColumns()
                .Culture(this.contextConfig.CultureName)
                .CombineWithDefaultCulture(false)
                .Published(published)
                .WhereEquals("NodeGUID", nodeGuid)
                .ToList()
                .FirstOrDefault();

            return document.ToPageType<PageType_Page>();
        }

        /// <summary>
        /// Gets the documents by IDs.
        /// </summary>
        /// <param name="published">
        /// If only published documents should be returned.
        /// </param>
        /// <param name="nodeGuids">
        /// The GUID of the documents.
        /// </param>
        /// <returns>
        /// The documents. Empty enumerable, otherwise.
        /// </returns>
        protected IEnumerable<PageType_Page> GetUncachedDocuments(
            bool published = true,
            params Guid[] nodeGuids)
        {
            var query =
                new DocumentQuery<PageType_Page>();

            var documents = query
                .OnSite(this.contextConfig.SiteName)
                .Culture(this.contextConfig.CultureName)
                .CombineWithDefaultCulture(false)
                .Published(published)
                .WhereNodeGuidIn(nodeGuids)?
                .ToList() ?? new List<PageType_Page>();

            return documents;
        }
    }
}
