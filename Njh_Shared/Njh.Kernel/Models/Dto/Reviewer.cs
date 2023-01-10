using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Models.Dto
{
    public class Reviewer
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<string> Titles { get; set; } = new List<string>();

        public Reviewer()
        {

        }
        public Reviewer(string name, string url = "")
        {
            this.Name = name;
            this.Url = url;
        }

    }
}
