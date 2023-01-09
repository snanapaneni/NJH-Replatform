using Njh.Kernel.Kentico.Models.CustomTables;
using Njh.Kernel.Kentico.Models.PageTypes;

namespace Njh.Kernel.Services
{
    public interface IPhysicianService
    {
        IEnumerable<PageType_Physician> GetPhysiciansByGuids(bool cached = true, bool published = true, params Guid[] physiciansGuids);

        IEnumerable<PageType_Physician> GetPhysicians();
    }
}
