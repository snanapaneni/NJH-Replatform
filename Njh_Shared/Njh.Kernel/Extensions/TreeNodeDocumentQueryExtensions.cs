using CMS.DataEngine;
using CMS.DocumentEngine;
using System.Diagnostics.CodeAnalysis;

namespace Njh.Kernel.Extensions
{
    /// <summary>
    /// Implements extension methods for the
    /// <see cref="T:CMS.DocumentEngine.DocumentQuery`1" /> class.
    /// </summary>
    public static class TreeNodeDocumentQueryExtensions
    {
        /// <summary>
        /// Adds the where condition for document
        /// filtering by NodeGUID to the query.
        /// </summary>
        /// <typeparam name="TDocument">The type of the document.</typeparam>
        /// <param name="documentQuery">The document query.</param>
        /// <param name="nodeGuid">The GUID of the node.</param>
        /// <returns>
        /// The document query filter by node GUIDs.
        /// If the node GUIDs are not given,
        /// no condition is added to the query.
        /// </returns>
        public static DocumentQuery<TDocument>? WhereNodeGuidIn<TDocument>(
          this DocumentQuery<TDocument> documentQuery,
          Guid nodeGuid)
          where TDocument : TreeNode, new()
        {
            if (documentQuery == null)
                return (DocumentQuery<TDocument>)null;
            return TreeNodeDocumentQueryExtensions.WhereNodeGuidIn<TDocument>(documentQuery, (ICollection<Guid>)new List<Guid>(1)
      {
        nodeGuid
      });
        }

        /// <summary>
        /// Adds the where condition for document
        /// filtering by NodeGUID to the query.
        /// </summary>
        /// <typeparam name="TDocument">The type of the document.</typeparam>
        /// <param name="documentQuery">The document query.</param>
        /// <param name="nodeGuids">The GUID of the nodes.</param>
        /// <returns>
        /// The document query filter by node GUIDs.
        /// If the node GUIDs are not given,
        /// no condition is added to the query.
        /// </returns>
        public static DocumentQuery<TDocument>? WhereNodeGuidIn<TDocument>(
          this DocumentQuery<TDocument> documentQuery,
          params Guid[] nodeGuids)
          where TDocument : TreeNode, new()
        {
            return documentQuery == null ? (DocumentQuery<TDocument>)null : TreeNodeDocumentQueryExtensions.WhereNodeGuidIn<TDocument>(documentQuery, (ICollection<Guid>)((IEnumerable<Guid>)nodeGuids).ToList<Guid>());
        }

        /// <summary>
        /// Adds the where condition for document
        /// filtering by NodeGUID to the query.
        /// </summary>
        /// <typeparam name="TDocument">The type of the document.</typeparam>
        /// <param name="documentQuery">The document query.</param>
        /// <param name="nodeGuids">The GUID of the nodes.</param>
        /// <returns>
        /// The document query filter by node GUIDs.
        /// If the node GUIDs are not given,
        /// no condition is added to the query.
        /// </returns>
        public static DocumentQuery<TDocument>? WhereNodeGuidIn<TDocument>(
          this DocumentQuery<TDocument> documentQuery,
          IEnumerable<Guid> nodeGuids)
          where TDocument : TreeNode, new()
        {
            return documentQuery == null ? (DocumentQuery<TDocument>)null : TreeNodeDocumentQueryExtensions.WhereNodeGuidIn<TDocument>(documentQuery, (ICollection<Guid>)nodeGuids.ToList<Guid>());
        }

        /// <summary>
        /// Adds the where condition for document
        /// filtering by NodeGUID to the query.
        /// </summary>
        /// <typeparam name="TDocument">The type of the document.</typeparam>
        /// <param name="documentQuery">The document query.</param>
        /// <param name="nodeGuids">The GUID of the nodes.</param>
        /// <returns>
        /// The document query filter by node GUIDs.
        /// If the node GUIDs are not given,
        /// no condition is added to the query.
        /// </returns>
        public static DocumentQuery<TDocument>? WhereNodeGuidIn<TDocument>(
          this DocumentQuery<TDocument> documentQuery,
          ICollection<Guid> nodeGuids)
          where TDocument : TreeNode, new()
        {
            return documentQuery?.Where((Action<WhereCondition>)(w => w.WhereIn("NodeGUID", nodeGuids)));
        }
    }
}
