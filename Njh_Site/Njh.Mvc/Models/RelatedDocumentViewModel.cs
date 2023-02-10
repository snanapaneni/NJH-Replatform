using Njh.Kernel.Models.DTOs;

namespace Njh.Mvc.Models
{
    public class RelatedDocumentViewModel
    {
        public IEnumerable<NavItem> RelatedDocuments { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
