using Njh.Kernel.Models.Dto;

namespace Njh.Mvc.Models.Widgets
{
    /// <summary>
    /// Accordion view model.
    /// </summary>
    public class AccordionViewModel
    {
        /// <summary>
        /// Gets or sets accordion items.
        /// </summary>
        public IEnumerable<AccordionItem> Items { get; set; } = Enumerable.Empty<AccordionItem>();
    }
}
