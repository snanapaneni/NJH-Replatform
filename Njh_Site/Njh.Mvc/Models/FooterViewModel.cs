using Njh.Kernel.Models.DTOs;
using Njh.Mvc.Components.Master;

namespace Njh.Mvc.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Implements the view model for the
    /// <see cref="FooterViewComponent"/> widget.
    /// </summary>
    public class FooterViewModel
    {
        public IEnumerable<NavItem> PolicyLinks { get; set; }
        public IEnumerable<NavItem> SupportedLanguages { get; set; }
        public string NewsletterSignUpText { get; set; }
        public string CopyRightText { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberText { get; set; }
        public string Address { get; set; }
        public string AddressUrl { get; set; }
        public IEnumerable<CTAItem> Badges { get; set; }

    }
}