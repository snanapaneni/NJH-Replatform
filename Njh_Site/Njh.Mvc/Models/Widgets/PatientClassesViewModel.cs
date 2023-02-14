namespace Njh.Mvc.Models.Widgets
{
    using Njh.Kernel.Models.Dto;
    using Njh.Mvc.Components.Patient;

    /// <summary>
    /// Implements the view model for the <see cref="PatientClassListingViewComponent"/> widget.
    /// </summary>
    public class PatientClassesViewModel
    {
        /// <summary>
        /// Gets or sets Patient Education Class listings grouped by day of the week.
        /// </summary>
        public Dictionary<string, List<PatientClass>> PatientClasses { get; set; } = new Dictionary<string, List<PatientClass>>();
    }
}
