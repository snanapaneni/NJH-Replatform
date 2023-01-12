using Njh.Kernel.Extensions;
using Njh.Kernel.Kentico.Models.PageTypes;

namespace Njh.Mvc.ViewComponents.Navigation
{
    using System;
    using Njh.Kernel.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;

    /// <summary>
    /// Implements the Footer Navigation view component.
    /// </summary>
    public class FooterButtonsViewComponent
        : SafeViewComponent<FooterButtonsViewComponent>
    {
        private INavigationService navservice;
        private readonly ISettingsKeyRepository settingsKeyRepository;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FooterButtonsViewComponent"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="navigationService">
        /// The navigation service.
        /// </param>
        /// <param name="viewComponentErrorVisibility">
        /// The view component error visibility.
        /// </param>
        public FooterButtonsViewComponent(
            ILogger<FooterButtonsViewComponent> logger,
            INavigationService navigationService,
            ISettingsKeyRepository settingsKeyRepository,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.navservice = navigationService ??
                              throw new ArgumentNullException(nameof(navigationService));

            this.settingsKeyRepository = settingsKeyRepository;
        }

        /// <summary>
        /// Renders the view component.
        /// </summary>
        /// <returns>
        /// The view component result.
        /// </returns>
        public IViewComponentResult Invoke()
        {
            return
                this.TryInvoke((vc) =>
                {
                    var navItems = vc.navservice.GetNavItems<PageType_NavItem>(settingsKeyRepository.GetFooterButtonsPath());

                    return vc.View(
                        "~/Views/Shared/Navigation/_FooterButtons.cshtml",
                        navItems);
                });
        }
    }
}