using Njh.Kernel.Kentico.Models.CustomTables;
using Njh.Kernel.Kentico.Models.PageTypes;

namespace Njh.Kernel.Services
{
    public interface IPhysicianService
    {
        PageType_Physician GetPhysicianItemByGuid(Guid physicianGuid, bool cached=true);

        IEnumerable<PageType_Physician> GetPhysician();
    }
}
