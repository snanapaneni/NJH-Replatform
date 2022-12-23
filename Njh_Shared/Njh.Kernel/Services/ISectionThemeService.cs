using Njh.Kernel.Kentico.Models.CustomTables;
namespace Njh.Kernel.Services
{
    public interface ISectionThemeService
    {
        CustomTable_SectionColorThemesItem GetThemesItemByGuid(Guid themeGuid, bool cached=true);

        IEnumerable<CustomTable_SectionColorThemesItem> GetThemesItems();
    }
}
