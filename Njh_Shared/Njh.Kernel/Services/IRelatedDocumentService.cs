using CMS.DocumentEngine;
using Njh.Kernel.Definitions;
using Njh.Kernel.Models.DTOs;

namespace Njh.Kernel.Services
{
    /// <summary>
    /// Related Documents Class.
    /// </summary>
    public interface IRelatedDocumentService
    {
        /// <summary>
        /// Get Related documents.
        /// </summary>
        /// <param name="currentDocument">current document.</param>
        /// <param name="type">type.</param>
        /// <param name="sourceField">source field names for categories.</param>
        /// <param name="targetField">target field names for categories.</param>
        /// <param name="categories">Categories.</param>
        /// <returns>List of related documents.</returns>
        IEnumerable<NavItem> GetRelatedDocuments(TreeNode currentDocument, RelatedDocumentType type, string sourceField, string targetField);
    }
}
