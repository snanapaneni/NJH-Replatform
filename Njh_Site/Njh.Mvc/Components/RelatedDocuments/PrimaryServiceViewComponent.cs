using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Extensions;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Mvc.Models;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.RelatedDocuments
{
    public class PrimaryServiceViewComponent : SafeViewComponent<PrimaryServiceViewComponent>
    {
        private readonly IPageDataContextRetriever dataRetriever;

        /// <inheritdoc />
        public PrimaryServiceViewComponent(ILogger<PrimaryServiceViewComponent> logger, IViewComponentErrorVisibility viewComponentErrorVisibility, IPageDataContextRetriever dataRetriever)
            : base(logger, viewComponentErrorVisibility)
        {
            this.dataRetriever = dataRetriever;
        }

        public IViewComponentResult Invoke()
        {
            return this.TryInvoke(vc =>
            {
                var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page;
                var model = new PrimaryServiceViewModel();
                model.ImagePath = currentPage.GetStringValue(nameof(PageType_Specialty.ConditionsWeTreatImage), string.Empty);
                model.ImageAltText = currentPage.GetStringValue(nameof(PageType_Specialty.ConditionsWeTreatImageAltText), string.Empty);
                model.Url = currentPage.GetStringValue(nameof(PageType_Specialty.ConditionsWeTreatLink), string.Empty);

                return vc.View("~/Views/Shared/RelatedDocuments/PrimaryService.cshtml", model);
            });
        }
    }
}
