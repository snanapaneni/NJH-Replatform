using Njh.Kernel.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Services
{
    internal interface IReviewerService
    {
        IEnumerable<Reviewer> GetReviewersByNodeGuid(Guid nodeGuid);
    }
}
