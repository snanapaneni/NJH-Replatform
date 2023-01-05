namespace Njh.Mvc.Models
{
    using System;
    using Njh.Mvc.ViewComponents.Master;

    /// <summary>
    /// Implements the view model for the
    /// <see cref="HeaderViewComponent"/> view component.
    /// </summary>
    public class HeaderDto
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
        /// Gets or sets the Make An Appointment URI.
        /// </summary>
        public Uri MakeAnAppointmentUri { get; set; }

        /// <summary>
        /// Gets or sets the Make An Appointment Text.
        /// </summary>
        public string MakeAnAppointmentText { get; set; }

        /// <summary>
        /// Gets or sets the Donate URI.
        /// </summary>
        public Uri DonateUri { get; set; }

        /// <summary>
        /// Gets or sets the Donate Text.
        /// </summary>
        public string DonateText { get; set; }

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
        public Uri GlobalSearchUrl { get; set; }

    }
}
