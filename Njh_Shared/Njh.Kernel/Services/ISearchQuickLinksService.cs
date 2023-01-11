using Njh.Kernel.Kentico.Models.CustomTables;
namespace Njh.Kernel.Services
{
    public interface ISearchQuickLinksService
    {
      IEnumerable<CustomTable_SearchQuickLinksItem> GetSearchQuickLinks(bool enabled = true, bool cached = true);
    }
}
