namespace Njh.Mvc.Components.Navigation
{
    using CMS.DocumentEngine;
    using DocumentFormat.OpenXml.Drawing.Charts;
    using Kentico.Content.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;
    using Njh.Kernel.Services;
    using Njh.Kernel.Models.DTOs;

    /// <summary>
    /// Implements the Breadcrumbs navigation component.
    /// </summary>
    public class BreadcrumbsViewComponent 
        : SafeViewComponent<BreadcrumbsViewComponent>
    {
        private readonly INavigationService navigationService;
        private readonly IPageDataContextRetriever dataRetriever;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbsViewComponent"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dataRetriever">The data retriever.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="viewComponentErrorVisibility">The view component error visibility.</param>
        /// <exception cref="ArgumentNullException">Throws exception on missing data retriever.</exception>
        public BreadcrumbsViewComponent(
            INavigationService navigationService,
            IPageDataContextRetriever dataRetriever,
            ILogger<BreadcrumbsViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.navigationService = navigationService ??
                throw new ArgumentNullException(nameof(navigationService));

            this.dataRetriever = dataRetriever ??
                throw new ArgumentNullException(nameof(dataRetriever));
        }

        /// <summary>
        /// Renders the view component markup.
        /// </summary>
        /// <returns>
        /// The view component result.
        /// </returns>
        public IViewComponentResult Invoke()
        {
            return this.TryInvoke(vc =>
            {
                IEnumerable<NavItem> navItems;

                var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page;

                if (currentPage != null)
                {
                    // get nav items for current page and ancestors, stopping at the root document
                    navItems = vc.navigationService.GetBreadcrumbNav(currentPage);
                }
                else
                {
                    navItems = new List<NavItem>();
                }

                return vc.View("~/Views/Shared/Navigation/_Breadcrumbs.cshtml", navItems);
            });
        }
    }
}
