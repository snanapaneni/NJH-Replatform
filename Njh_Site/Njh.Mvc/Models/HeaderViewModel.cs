using Njh.Kernel.Models.DTOs;
using Njh.Mvc.Components.Master;

namespace Njh.Mvc.Models
{
    using System;

    /// <summary>
    /// Implements the view model for the
    /// <see cref="HeaderViewComponent"/> view component.
    /// </summary>
    public class HeaderViewModel
    {
        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        public string Logo { get; set; }
        
        /// <summary>
        /// Gets or sets the logo alt text.
        /// </summary>
        public string LogoAltText { get; set; }

        /// <summary>
        /// Gets or sets the Phone Number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Phone Number Text.
        /// </summary>
        public string PhoneNumberText { get; set; }

        /// <summary>
        /// Gets or sets the global search URI.
        /// </summary>
        public string GlobalSearchUrl { get; set; }

        public NavItem CurrentTree { get; set; }

    }
}
