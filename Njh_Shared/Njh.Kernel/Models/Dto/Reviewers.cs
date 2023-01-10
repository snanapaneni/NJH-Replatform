using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Models.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class Reviewers
    {
        public bool HideUrl { get; set; } = false;
        public DateTime ReviewDate { get; set; } = DateTime.MinValue;
        public List<Reviewer> ReviewersList { get; set; } = new List<Reviewer>();
    }
}
