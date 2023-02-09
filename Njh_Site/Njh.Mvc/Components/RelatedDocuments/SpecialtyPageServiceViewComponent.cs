using CMS.DataEngine;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Extensions;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models.DTOs;
using Njh.Kernel.Services;
using Njh.Mvc.Models;
using ReasonOne.AspNetCore.Mvc.ViewComponents;
using ReasonOne.KenticoXperience.Services;
using ReasonOne.Services;

namespace Njh.Mvc.Components.RelatedDocuments
{
    public class SpecialtyPageServiceViewComponent : SafeViewComponent<SpecialtyPageServiceViewComponent>
    {
        private readonly IPageDataContextRetriever dataRetriever;
        private readonly IPageService pageService;
        private readonly IRelatedDocumentService relatedDocumentService;
        private readonly IResourceStringService resourceStringService;
        private readonly ISettingsKeyRepository settingsKeyRepository;

        /// <inheritdoc />
        public SpecialtyPageServiceViewComponent(ILogger<SpecialtyPageServiceViewComponent> logger, IViewComponentErrorVisibility viewComponentErrorVisibility, IPageDataContextRetriever dataRetriever, IPageService pageService, IResourceStringService resourceStringService, ISettingsKeyRepository settingsKeyRepository, IRelatedDocumentService relatedDocumentService)
            : base(logger, viewComponentErrorVisibility)
        {
            this.dataRetriever = dataRetriever;
            this.pageService = pageService;
            this.resourceStringService = resourceStringService;
            this.settingsKeyRepository = settingsKeyRepository;
            this.relatedDocumentService = relatedDocumentService;
        }

        public IViewComponentResult Invoke()
        {
            return this.TryInvoke(vc =>
            {
                var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page;
                var model = new SpecialtyConditionProgramViewModel("programsServices")
                {
                    HeaderText = resourceStringService.GetString("NJH.SpecialtyPage.ProgramsServicesHeader"),
                    MoreText = resourceStringService.GetString("NJH.SpecialtyPage.MoreButton"),
                    SearchText = settingsKeyRepository.GetValue<string>("NJHProgramsServicesSearchText"),
                    SearchUrl = settingsKeyRepository.GetValue<string>("NJHProgramsServicesSearchUrl"),
                };

                if (currentPage != null)
                {
                    model.ImagePath = currentPage.GetStringValue(nameof(PageType_Specialty.ProgramsAndServicesImage), string.Empty);
                    model.ImageAltText = currentPage.GetStringValue(nameof(PageType_Specialty.ProgramsAndServicesImageAltText), string.Empty);
                    model.Url = currentPage.GetStringValue(nameof(PageType_Specialty.ProgramsAndServicesLink), string.Empty);

                    var service = this.relatedDocumentService.GetDocument<PageType_Specialty>(currentPage.ToPageType<PageType_Specialty>().PrimaryProgram);
                    if (service != null)
                    {
                        model.OverviewLink = new NavItem()
                        {
                            DisplayTitle = service.GetStringValue(nameof(PageType_Condition.Title), service.DocumentName),
                            Link = DocumentURLProvider.GetUrl(service),
                        };
                    }

                    model.Links = new List<NavItem>();
                    var relatedServices = relatedDocumentService.GetRelatedDocuments(currentPage, new[] { PageType_Specialty.CLASS_NAME });
                    if (relatedServices.Any())
                    {
                        model.Links.AddRange(relatedServices);
                    }
                }

                return vc.View("~/Views/Shared/RelatedDocuments/SpecialtyConditionProgram.cshtml", model);
            });
        }
    }
}
