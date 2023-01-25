namespace Njh.Mvc.Components
{
    using System;
    using CMS.DocumentEngine;
    using Kentico.Content.Web.Mvc;
    using Kentico.PageBuilder.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Njh.Kernel.Extensions;
    using Njh.Kernel.Kentico.Models.PageTypes;
    using Njh.Kernel.Models.Dto;
    using Njh.Kernel.Services;
    using Njh.Mvc.Models;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;

    public class BlurbContentViewComponent 
        : SafeViewComponent<BlurbContentViewComponent>
    {
        /// <summary>
        /// The widget identifier.
        /// </summary>
        public const string Identifier =
            "Njh.BlurbContentWidget";

        private readonly IPageDataContextRetriever dataRetriever;


        public BlurbContentViewComponent(
            IPageDataContextRetriever dataRetriever, 
            ILogger<BlurbContentViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.dataRetriever = dataRetriever ??
                throw new ArgumentNullException(nameof(dataRetriever));
        }

        /// <summary>
        /// Renders the view component markup.
        /// </summary>
        /// <param name="componentProperties">
        /// The properties set on the widget instance in the CMS.
        /// </param>
        /// <returns>
        /// The view component result.
        /// </returns>
        public IViewComponentResult Invoke(ComponentViewModel<BlurbContentProperties> componentProperties)
        {
            return this.TryInvoke((vc) =>
            {
                var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page.ToPageType<PageType_Page>();

                var props = componentProperties?.Properties
                    ?? new BlurbContentProperties();

                BlurbContentViewModel blurbContentData = new ()
                {
                    ShowBlurb = props.ShowBlurb,
                    PageBlurbData = currentPage?.PageBlurb,
                    ShowContent = props.ShowContent,
                    PageContentData = currentPage?.PageContent,
                };

                return vc.View(
                    "~/Views/Shared/_BlurbContent.cshtml",
                    blurbContentData);
            });
        }
    }
}
