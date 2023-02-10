using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.Taxonomy;
using Njh.Kernel.Constants;
using Njh.Kernel.Definitions;
using Njh.Kernel.Extensions;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models;
using Njh.Kernel.Models.DTOs;

namespace Njh.Kernel.Services
{
    /// <inheritdoc cref="IRelatedDocuments" />
    public class RelatedDocumentService : ServiceBase, IRelatedDocumentService
    {
        private readonly ContextConfig contextConfig;
        private readonly ICacheService cacheService;
        private readonly ISettingsKeyRepository settingsKeyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedDocumentService"/> class.
        /// </summary>
        /// <param name="contextConfig">Context config.</param>
        /// <param name="cacheService">cache service.</param>
        /// <param name="settingsKeyRepository">Settings Key Repo.</param>
        public RelatedDocumentService(
            ContextConfig contextConfig,
            ICacheService cacheService,
            ISettingsKeyRepository settingsKeyRepository)
        {
            this.contextConfig = contextConfig;
            this.cacheService = cacheService;
            this.settingsKeyRepository = settingsKeyRepository;
        }

        /// <inheritdoc />
        public IEnumerable<NavItem> GetRelatedDocuments(TreeNode currentDocument, RelatedDocumentType type, string sourceField, string targetField)
        {
            var categories = currentDocument.GetValue(sourceField, string.Empty)
                .Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(item => ValidationHelper.GetGuid(item, Guid.Empty)).ToList();

            switch (type)
            {
                case RelatedDocumentType.ClinicalTrials:
                case RelatedDocumentType.Conditions:
                    return this.GetRelatedDocuments<PageType_Condition>(currentDocument, targetField, categories);
                default:
                    return new List<NavItem>();
            }
        }

        private IEnumerable<NavItem> GetRelatedDocuments<TDocument>(TreeNode currentPage, string fieldName, IList<Guid> categories)
            where TDocument : TreeNode, new()
        {
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByPathByType,
                    "RelatedDocs",
                    currentPage.NodeAliasPath,
                    typeof(TDocument).Name),
                IsCultureSpecific = true,
                CultureCode = this.contextConfig?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.contextConfig?.SiteName,
                CacheDependencies = new List<string>
                {
                    $"nodeid|{currentPage.NodeID}",
                },
            };

            IEnumerable<NavItem> DocCache(CacheParameters cs)
            {
                var query = new DocumentQuery<TDocument>().OnSite(this.contextConfig.SiteName)
                    .Culture(this.contextConfig.CultureName)
                    .CombineWithDefaultCulture(false)
                    .PublishedVersion()
                    .Published()
                    .OrderBy("DocumentName");

                WhereCondition where = new WhereCondition();
                foreach (var category in categories)
                {
                    where = where.Or().WhereContains(fieldName, category.ToString());
                }

                var data = query.Where(where).ToList();

                data.ForEach(doc =>
                {
                    cs.CacheDependencies.Add($"nodeid|{doc.NodeID}");
                });

                return data.Select(x => new NavItem
                {
                    DisplayTitle = x.GetValue("Title", x.DocumentName),
                    Link = DocumentURLProvider.GetUrl(x),
                });
            }

            return cacheService.Get(DocCache, cacheParameters);
        }

        /// <inheritdoc />
        public IEnumerable<NavItem> GetRelatedDocuments(TreeNode currentPage, string[] pageTypes)
        {
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByPathByType,
                    "RelatedDocs",
                    currentPage.NodeAliasPath,
                    string.Join("_",pageTypes)),
                IsCultureSpecific = true,
                CultureCode = this.contextConfig?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.contextConfig?.SiteName,
                CacheDependencies = new List<string>
                {
                    $"nodeid|{currentPage.NodeID}",
                },
            };

            IEnumerable<NavItem> DocCache(CacheParameters cs)
            {
                var categories = new List<int>();
                if (currentPage.GetIntegerValue("PrimaryCategory", 0) != 0)
                {
                    categories.Add(currentPage.GetIntegerValue("PrimaryCategory", 0));
                }
                else
                {
                    categories = currentPage.Categories.Cast<CategoryInfo>()
                        .Where(c => c.GetStringValue("CategoryNamePath", string.Empty).StartsWith("/Adult-Peds") ==
                                    false).Select(c => c.CategoryID).ToList();
                }

                if (!categories.Any())
                {
                    return new List<NavItem>();
                }

                var sql = $"(DocumentID in (Select DocumentID from CMS_DocumentCategory where CategoryID in ({string.Join(",", categories)})))";

                // TODO:Add columns to query
                var query = new MultiDocumentQuery().OnSite(this.contextConfig.SiteName)
                    .Types(pageTypes)
                    .Culture(this.contextConfig.CultureName)
                    .CombineWithDefaultCulture(false)
                    .PublishedVersion()
                    .Published()
                    .WhereNotEquals(nameof(TreeNode.NodeID), currentPage.NodeID)
                    .Where(sql)
                    .OrderBy("DocumentName");

                var adultPedFilterItem = GetAdultOrPediatricStringBasedOnCategory(currentPage);
                if (adultPedFilterItem != null)
                {
                    query.And().Where(
                        CategoryInfoProvider.GetCategoriesDocumentsWhereCondition(
                            new List<string> { adultPedFilterItem.CategoryIDPath }, true));
                }

                var data = query.ToList();

                data.ForEach(doc =>
                {
                    cs.CacheDependencies.Add($"nodeid|{doc.NodeID}");
                });

                return data.Select(x => new NavItem
                {
                    DisplayTitle = x.GetValue("Title", x.DocumentName),
                    Link = DocumentURLProvider.GetUrl(x),
                });
            }

            return cacheService.Get(DocCache, cacheParameters);
        }

        public TDocument? GetDocument<TDocument>(Guid nodeGuid, bool published = true)
            where TDocument : TreeNode, new()
        {
            return this
                .GetDocuments<TDocument>(
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
        private IEnumerable<TDocument> GetDocuments<TDocument>(
            bool published = true,
            params Guid[] nodeGuids)
            where TDocument : TreeNode, new()
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
                    "relateddocument",
                    $"{cacheKey}|{typeof(TDocument).Name}"),
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
                    () => this.GetUncachedDocuments<TDocument>(published, nodeGuids),
                    cacheParameters)
                : this.GetUncachedDocuments<TDocument>(published, nodeGuids);
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
        protected IEnumerable<TDocument> GetUncachedDocuments<TDocument>(
            bool published = true,
            params Guid[] nodeGuids)
            where TDocument : TreeNode, new()
        {
            var query =
                new DocumentQuery<TDocument>();

            var documents = query
                .OnSite(this.contextConfig.SiteName)
                .Culture(this.contextConfig.CultureName)
                .CombineWithDefaultCulture(false)
                .Published(published)
                .WhereNodeGuidIn(nodeGuids)?
                .ToList() ?? new List<TDocument>();

            return documents;
        }

        private CategoryInfo? GetAdultOrPediatricStringBasedOnCategory(TreeNode currentPage)
        {
            return currentPage.Categories.Cast<CategoryInfo>()
                        .FirstOrDefault(c => c.CategoryID == settingsKeyRepository.GetAdultCategoryId())
                    ?? currentPage.Categories.Cast<CategoryInfo>().FirstOrDefault(c =>
                        c.CategoryID == settingsKeyRepository.GetPediatricCategoryId());
        }
    }
}
