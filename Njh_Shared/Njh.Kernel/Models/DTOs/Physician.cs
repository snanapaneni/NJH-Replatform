using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Models.DTOs
{
    /// <summary>
    /// NJH Physician transfer object
    /// </summary>
    public class Physician
    {
        public string PageName { get; set; }

        public string Title { get; set; }

        public string Categories1 { get; set; }

        public bool FeaturedOnHomePage { get; set; }

        public string PhysicianDisplayName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

        public string Email { get; set; }

        public string PageBlurb { get; set; }

        // TODO more properties here
    }
}
