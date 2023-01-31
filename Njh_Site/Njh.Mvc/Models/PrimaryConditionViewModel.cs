using Njh.Kernel.Models.DTOs;

namespace Njh.Mvc.Models
{
    public class PrimaryConditionViewModel
    {
        public string ImagePath { get; set; }

        public string ImageAltText { get; set; }

        public string Url { get; set; }
        
        public string Title { get; set; }

        public string SearchText { get; set; }

        public string SearchUrl { get; set; }

        public IEnumerable<NavItem> Links { get; set; }
    }
}
