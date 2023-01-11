using Njh.Kernel.Extensions;
using Njh.Mvc.Models;

namespace Njh.Mvc.ViewComponents.Navigation
{
    using System;
    using Njh.Kernel.Services;
    using CMS.DocumentEngine;
    using Kentico.Content.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;
    using Njh.Kernel.Models.DTOs;

    /// <summary>
    /// Implements the left navigation view component.
    /// </summary>
    public class LeftNavViewComponent
        : SafeViewComponent<LeftNavViewComponent>
    {
        private readonly INavigationService navigationService;

        private readonly IPageDataContextRetriever dataRetriever;
        private readonly ISettingsKeyRepository settingsKeyRepository;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="LeftNavViewComponent"/> class.
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
        public LeftNavViewComponent(
            INavigationService navigationService,
            IPageDataContextRetriever dataRetriever,
            ILogger<LeftNavViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility,
            ISettingsKeyRepository settingsKeyRepository)
            : base(logger, viewComponentErrorVisibility)
        {
            this.navigationService = navigationService ??
                throw new ArgumentNullException(nameof(navigationService));

            this.dataRetriever = dataRetriever ??
                throw new ArgumentNullException(nameof(dataRetriever));

            this.settingsKeyRepository = settingsKeyRepository;
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
                    var parentPath = currentPage.Parent.NodeAliasPath;
                    navItems = vc.navigationService.GetSubTreeByPath(parentPath);
                    vc.navigationService.SetActiveItem(currentPage, navItems);
                }
                else
                {
                    navItems = new List<NavItem>();
                }

                return vc.View("~/Views/Shared/Navigation/_LeftNav.cshtml", navItems);
            });
        }
    }
}
