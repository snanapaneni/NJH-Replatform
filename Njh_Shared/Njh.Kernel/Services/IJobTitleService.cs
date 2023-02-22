using Njh.Kernel.Kentico.Models.CustomTables;
using Njh.Kernel.Models.Dto;

namespace Njh.Kernel.Services
{
    /// <summary>
    /// A service to get JobTitle custom table items.
    /// </summary>
    public interface IJobTitleService
    {
        /// <summary>
        /// Gets JobTitle items as SimpleLinks.
        /// </summary>
        /// <param name="cached">Whether to get from cache.</param>
        /// <returns>Enumerable SimpleLinks.</returns>
        IEnumerable<SimpleLink> GetJobTitleSimpleLinks(bool cached = true);

        /// <summary>
        /// Gets JobTitle items as custom table items.
        /// </summary>
        /// <param name="cached">Whether to get from cache.</param>
        /// <returns>Enumerable custom table items.</returns>
        IEnumerable<CustomTable_JobTitleItem> GetJobTitles(bool cached = true);
    }
}
