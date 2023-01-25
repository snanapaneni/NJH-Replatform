// Copyright (c) NJH. All rights reserved.

namespace Njh.Kernel.Services
{
    using System;
    using System.Collections.Generic;
    using CMS.DocumentEngine;

    /// <summary>
    /// Defines the service to work with
    /// <see cref="TreeNode"/> documents.
    /// </summary>
    public interface ITreeNodeService
        : IDocumentService<TreeNode>
    {
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
        TreeNode GetDocument(
            IEnumerable<string> pageTypes,
            IEnumerable<string> columns,
            Guid nodeGuid,
            bool published = true);

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
        IEnumerable<TreeNode> GetDocumentsByCategories(
            string path,
            IEnumerable<string> pageTypes,
            IEnumerable<string> columns,
            Guid?[] categoriesGuids,
            string orderBy = "",
            int level = 1,
            bool published = true);
        /// <summary>
        /// Get document by document Id.
        /// </summary>
        /// <param name="documentId">Document id.</param>
        /// <returns></returns>
        TreeNode GetDocument(int documentId);

        /// <summary>
        /// Get document by node Id.
        /// </summary>
        /// <param name="nodeId">The node id.</param>
        /// <returns></returns>
        public TreeNode GetDocumentByNodeID(int nodeId);
    }
}