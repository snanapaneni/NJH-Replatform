using CMS.DataEngine;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models.DTOs;
using Njh.Kernel.Services;
using Njh.Mvc.Models;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.RelatedDocuments
{
    public class SpecialtyPageConditionViewComponent : SafeViewComponent<SpecialtyPageConditionViewComponent>
    {
        private readonly IPageDataContextRetriever dataRetriever;
        private readonly IConditionService conditionService;
        private readonly IPageService pageService;

        /// <inheritdoc />
        public SpecialtyPageConditionViewComponent(ILogger<SpecialtyPageConditionViewComponent> logger, IViewComponentErrorVisibility viewComponentErrorVisibility, IPageDataContextRetriever dataRetriever, IConditionService conditionService, IPageService pageService)
            : base(logger, viewComponentErrorVisibility)
        {
            this.dataRetriever = dataRetriever;
            this.conditionService = conditionService;
            this.pageService = pageService;
        }

        public IViewComponentResult Invoke()
        {
            return this.TryInvoke(vc =>
            {
                var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page;
                var model = new SpecialtyPageConditionViewModel();
                if (currentPage != null)
                {
                    model.ImagePath = currentPage.GetStringValue(nameof(PageType_Specialty.ConditionsWeTreatImage), string.Empty);
                    model.ImageAltText = currentPage.GetStringValue(nameof(PageType_Specialty.ConditionsWeTreatImageAltText), string.Empty);
                    model.Url = currentPage.GetStringValue(nameof(PageType_Specialty.ConditionsWeTreatLink), string.Empty);

                    var links = this.conditionService.GetConditions(currentPage.NodeAliasPath, 2, new[] { "Title" }).ToList();
                    if (links.Any())
                    {
                        model.MainLinks = links.Select(doc => new NavItem()
                        {
                            DisplayTitle = doc.GetStringValue(nameof(PageType_Condition.Title), doc.DocumentName),
                            Link = DocumentURLProvider.GetUrl(doc),
                        });
                    }

                    model.OtherLinks = new List<NavItem>();
                    var childPages = pageService.GetChildPages(currentPage.Children.FirstItem.NodeAliasPath,
                        new WhereCondition().WhereEquals("nameof(PageType_Page.Show_On_HTLP)", 1)).Select(doc => new NavItem()
                    {
                        DisplayTitle = doc.GetStringValue(nameof(PageType_Page.Title),doc.DocumentName),
                        Link = DocumentURLProvider.GetUrl(doc),
                    });

                    model.OtherLinks.ToList().AddRange(childPages);
                }

                return vc.View("~/Views/Shared/RelatedDocuments/SpecialtyPageCondition.cshtml", model);
            });
        }
    }
}
