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
using ReasonOne.Services;

namespace Njh.Mvc.Components.RelatedDocuments
{
    public class SpecialtyPageConditionViewComponent : SafeViewComponent<SpecialtyPageConditionViewComponent>
    {
        private readonly IPageDataContextRetriever dataRetriever;
        private readonly IRelatedDocumentService relatedDocumentService;
        private readonly IPageService pageService;
        private readonly IResourceStringService resourceStringService;
        private readonly ISettingsKeyRepository settingsKeyRepository;

        /// <inheritdoc />
        public SpecialtyPageConditionViewComponent(ILogger<SpecialtyPageConditionViewComponent> logger, IViewComponentErrorVisibility viewComponentErrorVisibility, IPageDataContextRetriever dataRetriever, IRelatedDocumentService relatedDocumentService, IPageService pageService, IResourceStringService resourceStringService, ISettingsKeyRepository settingsKeyRepository)
            : base(logger, viewComponentErrorVisibility)
        {
            this.dataRetriever = dataRetriever;
            this.relatedDocumentService = relatedDocumentService;
            this.pageService = pageService;
            this.resourceStringService = resourceStringService;
            this.settingsKeyRepository = settingsKeyRepository;
        }

        public IViewComponentResult Invoke()
        {
            return this.TryInvoke(vc =>
            {
                var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page;
                var model = new SpecialtyConditionProgramViewModel("conditionInformation")
                {
                    HeaderText = resourceStringService.GetString("NJH.SpecialtyPage.ConditionInfoHeader"),
                    MoreText = resourceStringService.GetString("NJH.SpecialtyPage.MoreButton"),
                    SearchText = settingsKeyRepository.GetValue<string>("NJHConditionSearchText"),
                    SearchUrl = settingsKeyRepository.GetValue<string>("NJHConditionSearchUrl"),
                };

                if (currentPage != null)
                {
                    model.ImagePath = currentPage.GetStringValue(nameof(PageType_Specialty.ConditionsWeTreatImage), string.Empty);
                    model.ImageAltText = currentPage.GetStringValue(nameof(PageType_Specialty.ConditionsWeTreatImageAltText), string.Empty);
                    model.Url = currentPage.GetStringValue(nameof(PageType_Specialty.ConditionsWeTreatLink), string.Empty);

                    var condition = this.relatedDocumentService.GetDocument<PageType_Condition>(currentPage.ToPageType<PageType_Specialty>().PrimaryCondition);
                    if (condition != null)
                    {
                        model.OverviewLink = new NavItem()
                        {
                            DisplayTitle = condition.GetStringValue(nameof(PageType_Condition.Title), condition.DocumentName),
                            Link = DocumentURLProvider.GetUrl(condition),
                        };
                    }

                    model.Links = new List<NavItem>();
                    if (currentPage.Children.FirstItem != null)
                    {
                        var childPages = pageService.GetChildPages(currentPage.Children.FirstItem.NodeAliasPath,
                            new WhereCondition().WhereEquals(nameof(PageType_Page.ShowOnHTLP), 1)).Select(doc =>
                            new NavItem()
                            {
                                DisplayTitle = doc.GetStringValue(nameof(PageType_Page.Title), doc.DocumentName),
                                Link = DocumentURLProvider.GetUrl(doc),
                            });
                        model.Links.ToList().AddRange(childPages);
                    }

                    var relatedConditions = relatedDocumentService.GetRelatedDocuments(currentPage, new[] { PageType_Condition.CLASS_NAME });
                    if (relatedConditions.Any())
                    {
                        model.Links.ToList().AddRange(relatedConditions);
                    }
                }

                return vc.View("~/Views/Shared/RelatedDocuments/SpecialtyConditionProgram.cshtml", model);
            });
        }
    }
}
