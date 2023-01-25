// Copyright (c) NJH. All rights reserved.

namespace Njh.Kernel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CMS.DocumentEngine;
    using CMS.Taxonomy;
    using Njh.Kernel.Constants;
    using Njh.Kernel.Extensions;
    using Njh.Kernel.Helpers;
    using Njh.Kernel.Models;
    using ReasonOne.KenticoXperience.Extensions;

    /// <summary>
    /// Implements the service to work with
    /// <see cref="TreeNode"/> documents.
    /// </summary>
    public class TreeNodeService
        : ServiceBase, ITreeNodeService
    {
        private readonly ContextConfig contextConfig;

        private readonly ICacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="TreeNodeService"/> class.
        /// </summary>
        /// <param name="contextConfig">
        /// The context config.
        /// </param>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        public TreeNodeService(
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
        public TreeNode GetDocument(
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
        /// Gets the document by ID.
        /// </summary>
        /// <param name="pageTypes">
        /// The page types.
        /// </param>
        /// <param name="columns">
        /// The name of the columns.
        /// </param>
        /// <param name="nodeGuid">
        /// The GUID of the document.
        /// </param>
        /// <param name="published">
        /// If only published documents should be returned.
        /// </param>
        /// <returns>
        /// The documents. Empty enumerable, otherwise.
        /// </returns>
        public TreeNode GetDocument(
            IEnumerable<string> pageTypes,
            IEnumerable<string> columns,
            Guid nodeGuid,
            bool published = true)
        {
            var query = new MultiDocumentQuery();

            var document = query
                .OnSite(this.contextConfig.SiteName)
                .Types(pageTypes.ToArray())
                .Columns(columns.ToArray())
                .Culture(this.contextConfig.CultureName)
                .CombineWithDefaultCulture(false)
                .Published(published)
                .WhereEquals("NodeGUID", nodeGuid)
                .ToList()
                .FirstOrDefault();

            return document;
        }

        /// <summary>
        /// Get document by Id.
        /// </summary>
        /// <param name="documentId">
        /// The document ID.
        /// </param>
        /// <returns>
        /// The document.
        /// </returns>
        public TreeNode GetDocument(int documentId)
        {
            var query =
                new MultiDocumentQuery();

            var document = query
                .OnSite(this.contextConfig.SiteName)
                .WithCoupledColumns()
                .Culture(this.contextConfig.CultureName)
                .CombineWithDefaultCulture(false)
                .Published()
                .WhereEquals("DocumentID", documentId)
                .ToList()
                .FirstOrDefault();

            return document;
        }

        /// <inheritdoc />
        public TreeNode GetDocumentByNodeID(int nodeId)
        {
            var query =
                new MultiDocumentQuery();

            var document = query
                .OnSite(this.contextConfig.SiteName)
                .WithCoupledColumns()
                .Culture(this.contextConfig.CultureName)
                .CombineWithDefaultCulture(false)
                .Published()
                .WhereEquals("NodeID", nodeId)
                .ToList()
                .FirstOrDefault();

            return document;
        }

        /// <summary>
        /// Gets the documents by path, categories & type.
        /// </summary>
        /// <param name="path">
        /// The path of the pages to load.
        /// </param>
        /// <param name="pageTypes">
        /// The name of the pages.
        /// </param>
        /// <param name="columns">
        /// The name of the columns.
        /// </param>
        /// <param name="categoriesGuids">
        /// The categories of the documents.
        /// </param>
        /// <param name="orderBy">
        /// The orderby clause.
        /// </param>
        /// <param name="level">
        /// Specifies the nesting level.
        /// </param>
        /// <param name="published">
        /// If only published documents should be returned.
        /// </param>
        /// <returns>
        /// The documents. Empty enumerable, otherwise.
        /// </returns>
        public IEnumerable<TreeNode> GetDocumentsByCategories(
            string path,
            IEnumerable<string> pageTypes,
            IEnumerable<string> columns,
            Guid?[] categoriesGuids,
            string orderBy = "",
            int level = 1,
            bool published = true)
        {
            var cacheKey =
               categoriesGuids.Length > 0
                   ? categoriesGuids
                       .OrderBy(guid => guid)
                       .Select(guid => guid.ToString())
                       .Aggregate((current, next) => current + next)
                   : string.Empty;

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
                CacheDependencies = categoriesGuids
                    .Select(guid =>
                        string.Format(
                            DummyCacheKeys.PageSiteNodeGuid,
                            this.contextConfig?.SiteName,
                            guid))
                    .ToList(),
            };

            return published ?
                this.cacheService.Get(
                    () => this.GetUncachedDocumentsByCategories(
                        path,
                        pageTypes,
                        columns,
                        categoriesGuids,
                        orderBy,
                        level,
                        published),
                    cacheParameters)
                : this.GetUncachedDocumentsByCategories(
                        path,
                        pageTypes,
                        columns,
                        categoriesGuids,
                        orderBy,
                        level,
                        published);
        }

        /// <summary>
        /// Gets the documents with coupled data by IDs.
        /// </summary>
        /// <param name="nodeGuid">
        /// The GUID of the documents.
        /// </param>
        /// <param name="published">
        /// If only published documents should be returned.
        /// </param>
        /// <returns>
        /// The documents. Empty enumerable, otherwise.
        /// </returns>
        public TreeNode GetDocumentWithCoupledData(
            Guid nodeGuid,
            bool published = true)
        {
            var query =
                new MultiDocumentQuery();

            var document = query
                .OnSite(this.contextConfig.SiteName)
                .WithCoupledColumns()
                .Culture(this.contextConfig.CultureName)
                .CombineWithDefaultCulture(false)
                .Published(published)
                .WhereEquals("NodeGUID", nodeGuid)
                .ToList()
                .FirstOrDefault();

            return document;
        }



        /// <summary>
        /// Gets the documents by path, categories & type.
        /// </summary>
        /// <param name="path">
        /// The path of the pages to load.
        /// </param>
        /// <param name="pageTypes">
        /// The name of the pages.
        /// </param>
        /// <param name="columns">
        /// The name of the columns.
        /// </param>
        /// <param name="categoriesGuids">
        /// The categories of the documents.
        /// </param>
        /// <param name="orderBy">
        /// The orderby clause.
        /// </param>
        /// <param name="level">
        /// Specifies the nesting level.
        /// </param>
        /// <param name="published">
        /// If only published documents should be returned.
        /// </param>
        /// <returns>
        /// The documents. Empty enumerable, otherwise.
        /// </returns>
        protected IEnumerable<TreeNode> GetUncachedDocumentsByCategories(
            string path,
            IEnumerable<string> pageTypes,
            IEnumerable<string> columns,
            Guid?[] categoriesGuids,
            string orderBy = "",
            int level = 1,
            bool published = true)
        {
            var query = new MultiDocumentQuery();

            var strCategoriesNames = Category.GetCategoriesNamesByGuid(categoriesGuids)?.ToArray() ?? Array.Empty<string>();
            if (strCategoriesNames == null || strCategoriesNames.Length == 0)
            {
                strCategoriesNames = Array.Empty<string>();
            }

            if (!path.EndsWith("%"))
            {
                path = $"{path}/%";
            }

            var documents = query
                .OnSite(this.contextConfig.SiteName)
                .Path(path)
                .NestingLevel(level)
                .Types(pageTypes.ToArray())
                .Columns(columns.ToArray())
                .Culture(this.contextConfig.CultureName)
                .CombineWithDefaultCulture(false)
                .Published(published)
                .InCategories(strCategoriesNames)
                .OrderBy(orderBy)
                .ToList();

            return documents;
        }

        public IEnumerable<TreeNode> GetDocuments(bool published = true, params Guid[] nodeGuids)
        {
            throw new NotImplementedException();
        }
    }
}
