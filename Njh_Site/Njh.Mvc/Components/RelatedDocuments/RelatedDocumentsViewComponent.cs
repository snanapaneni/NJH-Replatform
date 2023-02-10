using CMS.DocumentEngine;
using CMS.Helpers;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Definitions;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Services;
using Njh.Mvc.Models;
using ReasonOne.AspNetCore.Mvc.ViewComponents;
using ReasonOne.Services;

namespace Njh.Mvc.Components.RelatedDocuments
{
    /// <summary>
    /// Related documents view component.
    /// </summary>
    public class RelatedDocumentsViewComponent : SafeViewComponent<RelatedDocumentsViewComponent>
    {
        private readonly ILogger<RelatedDocumentsViewComponent> logger;
        private readonly IViewComponentErrorVisibility viewComponentErrorVisibility;
        private readonly IRelatedDocumentService relatedDocumentService;
        private readonly IResourceStringService resourceStringService;
        private readonly IPageDataContextRetriever dataRetriever;
        private readonly ISettingsKeyRepository settingsKeyRepository;

        /// <summary>
        /// Initialize a new instance of the <see cref="RelatedDocumentsViewComponent"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="viewComponentErrorVisibility">Error Visibility.</param>
        /// <param name="relatedDocumentService">Related Document Service</param>
        public RelatedDocumentsViewComponent(ILogger<RelatedDocumentsViewComponent> logger, IViewComponentErrorVisibility viewComponentErrorVisibility, IRelatedDocumentService relatedDocumentService, IResourceStringService resourceStringService, IPageDataContextRetriever dataRetriever, ISettingsKeyRepository settingsKeyRepository)
            : base(logger, viewComponentErrorVisibility)
        {
            this.logger = logger;
            this.viewComponentErrorVisibility = viewComponentErrorVisibility;
            this.relatedDocumentService = relatedDocumentService;
            this.resourceStringService = resourceStringService;
            this.dataRetriever = dataRetriever;
            this.settingsKeyRepository = settingsKeyRepository;
        }

        /// <summary>
        /// Render view.
        /// </summary>
        /// <param name="type">Related Document Type.</param>
        /// <param name="pageTypes">targeted page types.</param>
        /// <param name="resString">ResString for empty related documents.</param>
        /// <returns>The view component results.</returns>
        public IViewComponentResult Invoke(RelatedDocumentType type, string pageTypes, string resString)
        {
            return this.TryInvoke(vc =>
            {
                var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page;

                var model = new RelatedDocumentViewModel
                {
                    RelatedDocuments = relatedDocumentService.GetRelatedDocuments(currentPage, pageTypes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)),
                    Title = resourceStringService.GetString($"NJH.RelatedDocuments.{type.ToStringRepresentation()}Title"),
                    ImageUrl = settingsKeyRepository.GetValue<string>($"NJHRelatedDocument{type.ToStringRepresentation()}Image"),
                };

                if (!model.RelatedDocuments.Any())
                {
                    model.Content = resourceStringService.GetString(resString);
                }

                return vc.View("~/Views/Shared/RelatedDocuments/RelatedDocuments.cshtml", model);
            });
        }
    }
}
