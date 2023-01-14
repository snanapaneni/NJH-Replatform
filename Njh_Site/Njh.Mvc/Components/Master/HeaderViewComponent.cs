using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Extensions;
using Njh.Kernel.Models.DTOs;
using Njh.Kernel.Services;
using Njh.Mvc.Models;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.Master
{
    /// <summary>
    /// Implements the Header view component.
    /// </summary>
    public class HeaderViewComponent
        : SafeViewComponent<HeaderViewComponent>
    {
        private readonly IPageService pageService;
        private readonly ISettingsKeyRepository settingsKeyRepository;
        private readonly INavigationService navigationService;
        private readonly IPageDataContextRetriever dataRetriever;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="HeaderViewComponent"/> class.
        /// </summary>
        /// <param name="siteSettingsService">
        /// The site settings service.
        /// </param>
        /// <param name="pageService">
        /// The page service.
        /// </param>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <param name="viewComponentErrorVisibility">
        /// The view component error visibility.
        /// </param>
        public HeaderViewComponent(
            IPageService pageService,
            ISettingsKeyRepository settingsKeyRepository,
            ILogger<HeaderViewComponent> logger,
            INavigationService navigationService,
            IPageDataContextRetriever dataRetriever,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            
            this.pageService = pageService ??
                throw new ArgumentNullException(nameof(pageService));

            this.settingsKeyRepository = settingsKeyRepository ??
                throw new ArgumentNullException(nameof(settingsKeyRepository));

            this.navigationService = navigationService;
            this.dataRetriever = dataRetriever;
        }

        /// <summary>
        /// Renders the view component markup.
        /// </summary>
        /// <returns>
        /// The view component result.
        /// </returns>
        public IViewComponentResult Invoke(bool isMobile = false)
        {
            return
                this.TryInvoke((vc) =>
                {

                    HeaderViewModel headerModel = new ()
                    {
                        Logo = settingsKeyRepository.GetHeaderLogo(),
                        LogoAltText = settingsKeyRepository.GetHeaderLogoAltText(),
                        GlobalSearchUrl = settingsKeyRepository.GetGlobalSearchPage(),
                        PhoneNumber = settingsKeyRepository.GetGlobalPhoneNumber(),
                        PhoneNumberText = settingsKeyRepository.GetGlobalPhoneNumberText()
                    };

                    var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page;
                    headerModel.CurrentTree = new NavItem()
                    {
                        DisplayTitle = currentPage.GetValue("Title",currentPage.DocumentName),
                        //Link = currentPage.NodeAliasPath,
                        Children = navigationService.GetSectionNavigation(currentPage).ToList(),
                    };

                    return vc.View(isMobile ? "~/Views/Shared/Master/_MobileHeader.cshtml" : "~/Views/Shared/Master/_Header.cshtml", headerModel);
                });
        }
    }
}
