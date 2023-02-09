using Njh.Kernel.Models.DTOs;

namespace Njh.Mvc.Models
{
    public class SpecialtyConditionProgramViewModel
    {
        public SpecialtyConditionProgramViewModel(string id)
        {
            ID = id;
        }

        public string ID { get; }

        public string HeaderText { get; set; }

        public string MoreText { get; set; }

        public string ImagePath { get; set; }

        public string ImageAltText { get; set; }

        public string Url { get; set; }

        public string SearchText { get; set; }

        public string SearchUrl { get; set; }

        public NavItem OverviewLink { get; set; }

        public List<NavItem> Links { get; set; }
    }
}
