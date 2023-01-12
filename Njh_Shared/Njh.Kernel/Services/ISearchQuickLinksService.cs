using Njh.Kernel.Kentico.Models.CustomTables;
using Njh.Kernel.Models.Dto;

namespace Njh.Kernel.Services
{
    public interface ISearchQuickLinksService
    {
        IEnumerable<SimpleLink> GetSearchQuickSimpleLinks(bool enabled = true, bool cached = true);
        IEnumerable<CustomTable_SearchQuickLinksItem> GetSearchQuickLinks(bool enabled = true, bool cached = true);
    }
}
