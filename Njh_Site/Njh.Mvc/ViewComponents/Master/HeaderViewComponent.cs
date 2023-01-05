using Njh.Kernel.Extensions;

namespace Njh.Mvc.ViewComponents.Master
{
    using System;
    using System.Linq;
    using Kernel.Services;
    using Models;
    using CMS.DocumentEngine;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;

    /// <summary>
    /// Implements the Header view component.
    /// </summary>
    public class HeaderViewComponent
        : SafeViewComponent<HeaderViewComponent>
    {
        private readonly IPageService pageService;
        private readonly ISettingsKeyRepository settingsKeyRepository;

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
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            
            this.pageService = pageService ??
                throw new ArgumentNullException(nameof(pageService));

            this.settingsKeyRepository = settingsKeyRepository ??
                throw new ArgumentNullException(nameof(settingsKeyRepository));
        }

        /// <summary>
        /// Renders the view component markup.
        /// </summary>
        /// <returns>
        /// The view component result.
        /// </returns>
        public IViewComponentResult Invoke()
        {
            return
                this.TryInvoke((vc) =>
                {
                    

                    var documentGuids = new Guid[]
                    {
                        settingsKeyRepository.GetMakeAnAppointmentPage(),
                        settingsKeyRepository.GetGlobalSearchPage(),
                        settingsKeyRepository.GetGlobalDonatePage()
                    };

                    // Uses a single query to get the 3 pages
                    var pageLinks = vc.pageService
                        .GetDocuments(
                            true,
                            documentGuids)
                        .Select(p => (p.NodeGUID, new Uri(DocumentURLProvider.GetAbsoluteUrl(p))));

                    HeaderDto headerModel = new ()
                    {
                        Logo = settingsKeyRepository.GetHeaderLogo(),
                        LogoAltText = settingsKeyRepository.GetHeaderLogoAltText(),
                        MakeAnAppointmentUri = pageLinks.Where(p => p.NodeGUID == settingsKeyRepository.GetMakeAnAppointmentPage()).Select(p => p.Item2).FirstOrDefault(),
                        MakeAnAppointmentText = settingsKeyRepository.GetMakeAnAppointmentText(),
                        GlobalSearchUrl = pageLinks.Where(p => p.NodeGUID == settingsKeyRepository.GetGlobalSearchPage()).Select(p => p.Item2).FirstOrDefault(),
                        DonateUri = pageLinks.Where(p => p.NodeGUID == settingsKeyRepository.GetGlobalDonatePage()).Select(p => p.Item2).FirstOrDefault(),
                        DonateText = settingsKeyRepository.GetGlobalDonateText(),
                        PhoneNumber = settingsKeyRepository.GetGlobalPhoneNumber(),
                        PhoneNumberText = settingsKeyRepository.GetGlobalPhoneNumberText()
                    };



                    return vc.View("~/Views/Shared/Master/_Header.cshtml", headerModel);
                });
        }
    }
}
