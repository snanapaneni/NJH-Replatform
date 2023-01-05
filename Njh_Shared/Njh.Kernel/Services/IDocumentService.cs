using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Services
{
    using System;
    using System.Collections.Generic;
    using CMS.DocumentEngine;

    /// <summary>
    /// Defines the service to work with document objects.
    /// </summary>
    /// <typeparam name="TDocument">
    /// The type of the document.
    /// </typeparam>
    public interface IDocumentService<TDocument>
        where TDocument : TreeNode, new()
    {
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
        TDocument GetDocument(
            Guid nodeGuid,
            bool published = true);

        /// <summary>
        /// This method is used to retrieve a tree node with
        /// coupled data.
        /// </summary>
        /// <param name="nodeGuid">
        /// The node GUID of the document.
        /// </param>
        /// <param name="published">
        /// If only published documents should be returned.
        /// </param>
        /// <returns>
        /// The document. null, otherwise.
        /// </returns>
        TDocument GetDocumentWithCoupledData(
            Guid nodeGuid,
            bool published = true);

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
        IEnumerable<TDocument> GetDocuments(
            bool published = true,
            params Guid[] nodeGuids);
    }
}
