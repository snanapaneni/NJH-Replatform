namespace Njh.Mvc.ViewComponents.Navigation
{
    using System;
    using Njh.Kernel.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;
    using Njh.Mvc.Models;
    using Njh.Kernel.Extensions;

    /// <summary>
    /// Implements the Primary Navigation view component.
    /// </summary>
    public class SearchBoxViewComponent
        : SafeViewComponent<SearchBoxViewComponent>
    {
        private readonly INavigationService navigationService;
        private readonly ISettingsKeyRepository settingsKeyRepository;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SearchBoxViewComponent"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="viewComponentErrorVisibility">
        /// The view component error visibility.
        /// </param>
        /// <param name="navigationService">
        /// The navigation service.
        /// </param>
        public SearchBoxViewComponent(
            ILogger<SearchBoxViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility,
            INavigationService navigationService,
            ISettingsKeyRepository settingsKeyRepository)
            : base(logger, viewComponentErrorVisibility)
        {
            this.navigationService = navigationService ??
                                     throw new ArgumentNullException(nameof(navigationService));

            this.settingsKeyRepository = settingsKeyRepository;
        }

        /// <summary>
        /// Renders the view component.
        /// </summary>
        /// <returns>
        /// The view component result.
        /// </returns>
        public IViewComponentResult Invoke(bool isMobile = false)
        {
            return
                this.TryInvoke((vc) =>
                {
                    QuickNavDto model = new ()
                    {
                        MakeAnAppointmentUri = settingsKeyRepository.GetMakeAnAppointmentPage(),
                        MakeAnAppointmentText = settingsKeyRepository.GetMakeAnAppointmentText(),
                        GlobalSearchUrl = settingsKeyRepository.GetGlobalSearchPage(),
                        DonateUri = settingsKeyRepository.GetGlobalDonatePage(),
                        DonateText = settingsKeyRepository.GetGlobalDonateText(),
                    };

                    return vc.View(
                        isMobile ? "~/Views/Shared/Navigation/_MobileQuickNav.cshtml" :
                        "~/Views/Shared/Navigation/_QuickNav.cshtml",
                        model);
                });
        }
    }
}
