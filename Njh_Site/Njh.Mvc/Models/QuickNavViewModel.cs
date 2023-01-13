using Njh.Kernel.Models.DTOs;

namespace Njh.Mvc.Models
{
    public class QuickNavViewModel
    {
        /// <summary>
        /// Gets or sets the Make An Appointment URI.
        /// </summary>
        public string MakeAnAppointmentUri { get; set; }

        /// <summary>
        /// Gets or sets the Make An Appointment Text.
        /// </summary>
        public string MakeAnAppointmentText { get; set; }

        /// <summary>
        /// Gets or sets the Donate URI.
        /// </summary>
        public string DonateUri { get; set; }

        /// <summary>
        /// Gets or sets the Donate Text.
        /// </summary>
        public string DonateText { get; set; }

        /// <summary>
        /// Gets or sets the global search URI.
        /// </summary>
        public string GlobalSearchUrl { get; set; }
    }
}
