using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Models.Dto
{
    public class PatientClass
    {
        public string? Title { get; set; }

        // NOTE: could be string[] or IEnumerable
        public IList<string> ClassDays { get; set; }

        public string? DateInfo { get; set; }

        public string? Summary { get; set; }
        
        public string? Fees { get; set; }

        public string? Location { get; set; }

        public string? Url { get; set; }
    }
}
