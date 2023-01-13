using CMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Definitions;
using Njh.Kernel.Extensions;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models.DTOs;
using Njh.Kernel.Services;
using Njh.Mvc.Models;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.Master
{
    /// <summary>
    /// View component for the site footer.
    /// </summary>
    public class FooterViewComponent
        : SafeViewComponent<FooterViewComponent>
    {
        private readonly ISettingsKeyRepository settingsKeyRepository;
        private readonly INavigationService navigationService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FooterViewComponent"/> class.
        /// </summary>
        /// <param name="siteSettingsService">
        /// The site settings service.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="viewComponentErrorVisibility">
        /// The view component error visibility.
        /// </param>
        public FooterViewComponent(
            ISettingsKeyRepository settingsKeyRepository,
            ILogger<FooterViewComponent> logger,
            INavigationService navigationService,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.settingsKeyRepository = settingsKeyRepository ??
                throw new ArgumentNullException(nameof(settingsKeyRepository));

            this.navigationService = navigationService;
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
                    var phoneNumber = settingsKeyRepository.GetGlobalPhoneNumber();
                    FooterViewModel footerDto = new ()
                    {
                        Address = settingsKeyRepository.GetAddress(),
                        AddressUrl = settingsKeyRepository.GetAddressUrl(),
                        Badges = navigationService.GetCTAItems(settingsKeyRepository.GetBadgesPath())?.Take(3) ?? new List<CTAItem>(),
                        CopyRightText = ResHelper.GetString("NJH.Footer.CopyRightText"),
                        NewsletterSignUpText = ResHelper.GetString("NJH.Footer.NewsletterSignUpText"),
                        PhoneNumber = phoneNumber,
                        PhoneNumberText = GlobalConstants.Regexs.RxPhone.IsMatch(phoneNumber) ? GlobalConstants.Regexs.RxPhone.Replace(phoneNumber, GlobalConstants.Regexs.PhoneDisplayFormat) : phoneNumber,
                        PolicyLinks = navigationService.GetNavItems<PageType_NavItem>(settingsKeyRepository.GetPolicyLinksPath()),
                        SupportedLanguages = navigationService.GetNavItems<PageType_NavItem>(settingsKeyRepository.GetSupportedLanguagesPath()),
                    };

                    return vc.View("~/Views/Shared/Master/_Footer.cshtml", footerDto);
                });
        }
    }
}
