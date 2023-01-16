using Njh.Kernel.Models.DTOs;

namespace Njh.Mvc.Models
{
    public class UtilityNavViewModel
    {
        /// <summary>
        /// Gets or sets the Utility Links.
        /// </summary>
        public IEnumerable<NavItem> Links { get; set; }

        /// <summary>
        /// Gets or sets the Phone Number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Phone Number Text.
        /// </summary>
        public string PhoneNumberText { get; set; }
    }
}
