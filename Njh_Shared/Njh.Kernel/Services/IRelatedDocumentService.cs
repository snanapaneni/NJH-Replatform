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
        /// <param name="type">type.</param>
        /// <param name="fieldName">target field names for categories.</param>
        /// <param name="categories">Categories.</param>
        /// <returns>List of related documents.</returns>
        IEnumerable<NavItem> GetRelatedDocuments(RelatedDocumentType type, string fieldName, IList<Guid> categories);
    }
}
