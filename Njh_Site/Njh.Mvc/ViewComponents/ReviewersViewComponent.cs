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
    using Njh.Kernel.Models.Dto;

    /// <summary>
    /// Implements the utility navigation view component.
    /// </summary>
    public class ReviewersViewComponent
        : SafeViewComponent<ReviewersViewComponent>
    {
        private readonly IReviewerService reviewerService;
        private readonly IPageDataContextRetriever dataRetriever;
        private readonly ISettingsKeyRepository settingsKeyRepository;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ReviewersViewComponent"/> class.
        /// </summary>
        /// <param name="reviewerService">
        /// The reviewer service.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="viewComponentErrorVisibility">
        /// The view component error visibility.
        /// </param>
        public ReviewersViewComponent(
            IReviewerService reviewerService,
            IPageDataContextRetriever dataRetriever,
            ILogger<ReviewersViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.reviewerService = reviewerService ??
                throw new ArgumentNullException(nameof(reviewerService));
            
            this.dataRetriever = dataRetriever ??
                throw new ArgumentNullException(nameof(dataRetriever));
          
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
                var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page;

                Reviewers reviewers = new Reviewers();

                if (currentPage != null)
                {
                    reviewers = vc.reviewerService.GetReviewersByNodeGuid(currentPage.NodeGUID);
                }

                

                return vc.View("~/Views/Shared/_Reviewers.cshtml",
                    reviewers);
            });
        }
    }
}
