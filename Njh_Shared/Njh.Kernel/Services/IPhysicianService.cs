using Njh.Kernel.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Services
{
    /// <summary>
    /// Service for NJH Physician data
    /// </summary>
    public interface IPhysicianService
    {
        /// <summary>
        /// Get all Physicians in the CMS
        /// </summary>
        /// <returns>list of Physicians</returns>
        IEnumerable<Physician> GetPhysicians();
    }
}
