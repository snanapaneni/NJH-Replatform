using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Services;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.Navigation
{
    /// <summary>
    /// Implements the Footer Navigation view component.
    /// </summary>
    public class FooterNavViewComponent
        : SafeViewComponent<FooterNavViewComponent>
    {
        private INavigationService navservice;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FooterNavViewComponent"/> class.
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
        public FooterNavViewComponent(
            ILogger<FooterNavViewComponent> logger,
            INavigationService navigationService,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.navservice = navigationService ??
                throw new ArgumentNullException(nameof(navigationService));
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
                    var navItems = vc.navservice.GetFooterNav();

                    return vc.View(
                        "~/Views/Shared/Navigation/_FooterNav.cshtml",
                        navItems);
                });
        }
    }
}