namespace Njh.Kernel.Services
{
    using Njh.Kernel.Models.Dto;

    /// <summary>
    /// A service to retrieve Patient Education documents (patient classes).
    /// </summary>
    public interface IPatientClassService
    {
        /// <summary>
        /// Gets the Patient Education documents from the specified path.
        /// </summary>
        /// <param name="path">Path in CMS Tree.</param>
        /// <returns>Patient Classes; can be empty.</returns>
        IEnumerable<PatientClass> GetPatientClasses(string path);
    }
}
