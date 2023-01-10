using Njh.Kernel.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Services
{
    public interface IReviewerService
    {
        Reviewers GetReviewersByNodeGuid(Guid nodeGuid);
    }
}
