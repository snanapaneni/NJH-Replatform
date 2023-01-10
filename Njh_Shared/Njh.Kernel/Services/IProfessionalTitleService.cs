using Njh.Kernel.Kentico.Models.CustomTables;

namespace Njh.Kernel.Services
{
    public interface IProfessionalTitleService
    {
        List<string> GetProfessionalTitlesByGuids( params Guid[] professionalTitleGuids);

        IEnumerable<CustomTable_ProfessionalTitleItem> GetProfessionalTitles();
    }
}
