using Njh.Kernel.Models.DTOs;

namespace Njh.Mvc.Models.Widgets
{
    public class AdditionalLinksViewModel
    {
        public string Title { get; set; }

        public IEnumerable<NavItem> Items { get; set; }
    }
}
