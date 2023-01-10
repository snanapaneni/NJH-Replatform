using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.ViewComponents.Navigation
{
    using System;
    using Njh.Kernel.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;
    using Kentico.Content.Web.Mvc;
    using CMS.DocumentEngine;

    /// <summary>
    /// Implements a Left Nav view component.
    /// </summary>
    public class LeftNavViewComponent
        : SafeViewComponent<LeftNavViewComponent>
    {
        private readonly INavigationService navigationService;

        private readonly IPageDataContextRetriever dataRetriever;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeftNavViewComponent"/> class.
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="viewComponentErrorVisibility">The view component error visibility.</param>
        /// <param name="navigationService">Injected NavigationService TODO replace with LeftNav-specific service?</param>
        /// <param name="dataRetriever">Data retriever for current page</param>
        /// <exception cref="ArgumentNullException">Thrown if navigation service is missing</exception>
        public LeftNavViewComponent(
            ILogger<LeftNavViewComponent> logger, 
            IViewComponentErrorVisibility viewComponentErrorVisibility,
            INavigationService navigationService, 
            IPageDataContextRetriever dataRetriever) 
            : base(logger, viewComponentErrorVisibility)
        {
            this.navigationService = navigationService ??
                                     throw new ArgumentNullException(nameof(navigationService));
        }

        /// <summary>
        /// Render the view component markup.
        /// </summary>
        /// <param name="currentDocPath">Starting point for the left nav.</param>
        /// <returns>The view component result.</returns>
        public IViewComponentResult Invoke(string currentDocPath)
        {
            return this.TryInvoke(vc => {
                var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page;
                IEnumerable<Kernel.Models.DTOs.NavItem> navItems;

                // TODO what to do if current page is not found? what happens when viewing in CMS Admin?
                if (currentPage != null)
                {
                    // TODO is this the correct method, or does it get a whole subtree instead of just what we need?
                    // TODO do we need to check page types etc. here? in NavigationService?
                    navItems = vc.navigationService.GetSectionNavigation(currentPage);

                    vc.navigationService.SetActiveItem(currentPage, navItems);
                }
                else
                {
                    // TODO is setting an empty list the right thing to do?
                    navItems = new List<Kernel.Models.DTOs.NavItem>();
                }

                return vc.View("~/Views/Shared/Navigation/_LeftNav.cshtml", navItems);
            });
        }

    }
}
