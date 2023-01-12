namespace Njh.Mvc.ViewComponents.Navigation
{
    using System;
    using Njh.Kernel.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;
    using Njh.Mvc.Models;
    using Njh.Kernel.Extensions;
    using ReasonOne.Services;

    /// <summary>
    /// Implements the Primary Navigation view component.
    /// </summary>
    public class SearchBoxViewComponent
        : SafeViewComponent<SearchBoxViewComponent>
    {
        private readonly ISearchQuickLinksService searchQuickLinksService;
        private readonly ISettingsKeyRepository settingsKeyRepository;
        private readonly IResourceStringService resourceStringService;
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SearchBoxViewComponent"/> class.
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
        public SearchBoxViewComponent(
            ILogger<SearchBoxViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility,
            ISearchQuickLinksService searchQuickLinksService,
            ISettingsKeyRepository settingsKeyRepository,
            IResourceStringService resourceStringService)
            : base(logger, viewComponentErrorVisibility)
        {
            this.searchQuickLinksService = searchQuickLinksService ??
                                     throw new ArgumentNullException(nameof(searchQuickLinksService));

            this.settingsKeyRepository = settingsKeyRepository ??
                         throw new ArgumentNullException(nameof(settingsKeyRepository));

            this.resourceStringService = resourceStringService ??
               throw new ArgumentNullException(nameof(resourceStringService));

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
                    SearchBoxViewModel model = new()
                    {
                        QuickLinksDropdownPlaceholder = resourceStringService.GetString("NJH.Search.QuickLinksDropdownPlaceholder"),
                        SearchResultsPageUrl = settingsKeyRepository.GetGlobalSearchPage(),
                        SearchBoxLabel = resourceStringService.GetString("NJH.Search.SearchBoxLabel"),
                        SearchBoxPlaceholderText = resourceStringService.GetString("NJH.Search.SearchBoxPlaceholderText"),
                        SearchBoxValidationErrorMessage = resourceStringService.GetString("NJH.Search.SearchBoxValidationErrorMessage"),
                        QuickLinks = searchQuickLinksService.GetSearchQuickSimpleLinks().ToList(),
                    };

                    return vc.View("~/Views/Shared/Navigation/_SearchBox.cshtml", model);
                });
        }
    }
}
