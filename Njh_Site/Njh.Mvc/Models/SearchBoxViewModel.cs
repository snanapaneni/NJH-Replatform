using Njh.Kernel.Models.Dto;

namespace Njh.Mvc.Models
{
    public class SearchBoxViewModel
    {
        public string SearchResultsPageUrl { get; set; }
        public string SearchBoxLabel { get; set; }
        public string SearchBoxPlaceholderText { get; set; }
        public string QuickLinksDropdownPlaceholder { get; set; }
        public string SearchBoxValidationErrorMessage { get; set; }
        public List<SimpleLink> QuickLinks { get; set; }

    }
}
