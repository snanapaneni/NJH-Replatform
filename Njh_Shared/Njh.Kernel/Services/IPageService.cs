using CMS.DataEngine;
using Njh.Kernel.Kentico.Models.PageTypes;

namespace Njh.Kernel.Services
{
    /// <summary>
    /// Defines the service to work with
    /// <see cref="PageType_Page"/> documents.
    /// </summary>
    public interface IPageService
        : IDocumentService<PageType_Page>
    {
        IEnumerable<PageType_Page> GetChildPages(string path, IWhereCondition? where = null);
    }
}
