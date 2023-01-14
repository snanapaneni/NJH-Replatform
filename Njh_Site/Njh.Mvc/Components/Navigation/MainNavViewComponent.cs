using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Services;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.Navigation
{
    /// <summary>
    /// Implements the Primary Navigation view component.
    /// </summary>
    public class MainNavViewComponent
        : SafeViewComponent<MainNavViewComponent>
    {
        private readonly INavigationService navigationService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MainNavViewComponent"/> class.
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
        public MainNavViewComponent(
            ILogger<MainNavViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility,
            INavigationService navigationService)
            : base(logger, viewComponentErrorVisibility)
        {
            this.navigationService = navigationService ??
                                     throw new ArgumentNullException(nameof(navigationService));
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
                    var navItems = vc.navigationService.GetPrimaryNav();

                    return vc.View(
                        isMobile ? "~/Views/Shared/Navigation/_MobileMainNav.cshtml" :
                        "~/Views/Shared/Navigation/_MainNav.cshtml",
                        navItems);
                });
        }
    }
}
