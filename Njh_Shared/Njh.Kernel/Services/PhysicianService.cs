namespace Njh.Kernel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Njh.Kernel.Models.DTOs;

    /// <summary>
    /// A service for getting Physician data
    /// </summary>
    public class PhysicianService : ServiceBase, IPhysicianService
    {
        /// <summary>
        /// Get all Physicians in the CMS
        /// </summary>
        /// <returns>list of Physicians</returns>
        /// <exception cref="NotImplementedException">TODO implement</exception>
        IEnumerable<Physician> IPhysicianService.GetPhysicians()
        {
            throw new NotImplementedException();
        }
    }
}
