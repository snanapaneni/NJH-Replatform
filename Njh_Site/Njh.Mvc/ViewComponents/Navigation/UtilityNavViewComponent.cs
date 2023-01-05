namespace Njh.Mvc.ViewComponents.Navigation
{
    using System;
    using Njh.Kernel.Services;
    using CMS.DocumentEngine;
    using Kentico.Content.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;

    /// <summary>
    /// Implements the utility navigation view component.
    /// </summary>
    public class UtilityNavViewComponent
        : SafeViewComponent<UtilityNavViewComponent>
    {
        private readonly INavigationService navigationService;

        private readonly IPageDataContextRetriever dataRetriever;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UtilityNavViewComponent"/> class.
        /// </summary>
        /// <param name="navigationService">
        /// The navigation service.
        /// </param>
        /// <param name="dataRetriever">
        /// The data retriever.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="viewComponentErrorVisibility">
        /// The view component error visibility.
        /// </param>
        public UtilityNavViewComponent(
            INavigationService navigationService,
            IPageDataContextRetriever dataRetriever,
            ILogger<UtilityNavViewComponent> logger,
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
                var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page;

                var navItems = vc.navigationService.GetUtilityNav();

                if (currentPage != null)
                {
                    vc.navigationService.SetActiveItem(currentPage, navItems);
                }

                return vc.View(
                    "~/Views/Shared/Navigation/_UtilityNav.cshtml",
                    navItems);
            });
        }
    }
}
