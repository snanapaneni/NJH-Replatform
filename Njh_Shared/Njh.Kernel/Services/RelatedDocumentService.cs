using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.Taxonomy;
using Njh.Kernel.Constants;
using Njh.Kernel.Definitions;
using Njh.Kernel.Kentico.Models.CustomTables;
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

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="contextConfig">Context config.</param>
        /// <param name="cacheService">cache service.</param>
        public RelatedDocumentService(
            ContextConfig contextConfig,
            ICacheService cacheService)
        {
            this.contextConfig = contextConfig;
            this.cacheService = cacheService;
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

        private IEnumerable<NavItem> GetRelatedDocuments<TDocument>(TreeNode currentPage)
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
                var categories = new List<int>();
                if (currentPage.GetIntegerValue("PrimaryCategory", 0) != 0)
                {
                    categories.Add(currentPage.GetIntegerValue("PrimaryCategory", 0));
                }
                else
                {
                    categories = currentPage.Categories.Cast<CategoryInfo>()
                        .Where(c => c.GetStringValue("CategoryNamePath", string.Empty).StartsWith("/Adult-Peds") ==
                                    false).Cast<CategoryInfo>().Select(c => c.CategoryID).ToList();
                }

                var sql = $"(DocumentID in (Select DocumentID from CMS_DocumentCategory where CategoryID in ({string.Join(",", categories)})))";

                var query = new DocumentQuery<TDocument>().OnSite(this.contextConfig.SiteName)
                    .Culture(this.contextConfig.CultureName)
                    .CombineWithDefaultCulture(false)
                    .PublishedVersion()
                    .Published()
                    .Where(sql)
                    .OrderBy("DocumentName");

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
    }
}
