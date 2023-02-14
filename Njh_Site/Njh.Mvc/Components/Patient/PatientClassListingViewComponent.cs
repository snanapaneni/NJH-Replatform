namespace Njh.Mvc.Components.Patient
{
    using Kentico.PageBuilder.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using Njh.Kernel.Definitions;
    using Njh.Kernel.Models;
    using Njh.Kernel.Models.Dto;
    using Njh.Kernel.Services;
    using Njh.Mvc.Models.Widgets;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;

    /// <summary>
    /// Displays list of Patient Education classes from a specified path, grouped by days of the week they are offered.
    /// </summary>
    public class PatientClassListingViewComponent : SafeViewComponent<PatientClassListingViewComponent>
    {
        /// <summary>
        /// The widget identifier.
        /// </summary>
        public const string Identifier =
            "Njh.PatientClassListing";

        private readonly ContextConfig context;
        private readonly IPatientClassService patientClassService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientClassListingViewComponent"/> class.
        /// </summary>
        /// <param name="context">The context config.</param>
        /// <param name="patientClassService">Service to retrieve Patient Education Classes data.</param>
        /// <param name="logger">A logger for errors.</param>
        /// <param name="viewComponentErrorVisibility">View component error visibility methods.</param>
        /// <exception cref="ArgumentNullException">Throws exception if media file info provider is missing or null.</exception>
        public PatientClassListingViewComponent(
            ContextConfig context,
            IPatientClassService patientClassService,
            ILogger<PatientClassListingViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.context = context ??
                throw new ArgumentNullException(nameof(context));

            this.patientClassService = patientClassService ??
                throw new ArgumentNullException(nameof(patientClassService));
        }

        /// <summary>
        /// Renders the view component markup.
        /// </summary>
        /// <param name="componentProperties">
        /// The properties set on the widget instance in the CMS.
        /// </param>
        /// <returns>
        /// The view component result.
        /// </returns>
        public IViewComponentResult Invoke(ComponentViewModel<PatientClassListingComponentProperties> componentProperties)
        {
            return this.TryInvoke((vc) =>
            {
                var props = componentProperties?.Properties
                    ?? new PatientClassListingComponentProperties();

                // get selected path to the NJH Patient Education documents
                var path = props.ClassesPaths.FirstOrDefault()?.NodeAliasPath;
                var groupedClasses = new Dictionary<string, List<PatientClass>>();

                // load data only if a path is selected
                if (!string.IsNullOrEmpty(path))
                {
                    var patientClasses = this.patientClassService.GetPatientClasses(path);

                    // loop over the days of the week in this order; add entries only for days having classes
                    foreach (var day in GlobalConstants.Utils.DaysOfTheWeek)
                    {
                        var dayClasses = patientClasses.Where(
                                pc => pc.ClassDays.Contains(day, StringComparer.InvariantCultureIgnoreCase))
                            .ToList();
                        if (dayClasses.Any())
                        {
                            groupedClasses.Add(day, dayClasses);
                        }
                    }
                }

                PatientClassesViewModel viewModel = new ()
                {
                    PatientClasses = groupedClasses,
                };

                return vc.View(
                    "~/Views/Shared/Widgets/_PatientClassListing.cshtml",
                    viewModel);
            });
        }
    }
}
