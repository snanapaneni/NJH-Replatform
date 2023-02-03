// Copyright (c) NJH. All rights reserved.

#nullable enable
namespace Njh.Mvc.Components.MetaData
{
    using System;
    using Njh.Kernel.Extensions;
    using Njh.Kernel.Kentico.Models.PageTypes;
    using Njh.Mvc.Models;
    using CMS.DocumentEngine;
    using Kentico.Content.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;
    using Njh.Kernel.Services;

    /// <summary>
    /// Implements the Metadata view component.
    /// </summary>
    public partial class MetaDataViewComponent
        : SafeViewComponent<MetaDataViewComponent>
    {
        private readonly ILogger<MetaDataViewComponent> logger;

        private readonly IPageDataContextRetriever dataRetriever;
        private readonly ISettingsKeyRepository settingKeyRepository;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MetaDataViewComponent"/> class.
        /// </summary>
        /// <param name="dataRetriever">
        /// The page data retriever.
        /// </param>
        /// <param name="settingKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="viewComponentErrorVisibility">
        /// The view component error visibility.
        /// </param>
        public MetaDataViewComponent(
            IPageDataContextRetriever dataRetriever,
            ISettingsKeyRepository settingKeyRepository,
            ILogger<MetaDataViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            this.dataRetriever = dataRetriever ??
                throw new ArgumentNullException(nameof(dataRetriever));

            this.settingKeyRepository = settingKeyRepository ??
                throw new ArgumentNullException(nameof(settingKeyRepository));
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
                this.TryInvoke(vc =>
                {
                    var model = new MetaDataViewModel();

                    TreeNode? currentPage;

                    try
                    {
                        currentPage =
                            vc.dataRetriever.Retrieve<TreeNode>()?.Page;
                    }
                    catch (InvalidOperationException ex)
                    {
                        this.LogPageDataIsNotAvailable(
                            this.HttpContext.Request.Path);

                        this.LogPageDataIsNotAvailableException(ex);

                        currentPage = null;
                    }

                    if (currentPage != null)
                    {
                        model.PageUrl = DocumentURLProvider.GetAbsoluteUrl(currentPage);
                        model.OgType = this.settingKeyRepository.GetOgType();
                        model.OgSiteName = this.settingKeyRepository.GetOgSiteName();
                        model.Description =
                            currentPage.GetMetaTag("DocumentPageDescription");

                        model.Keywords =
                            currentPage.GetMetaTag("DocumentPageKeyWords");

                        model.ImageUrl =
                            string.IsNullOrWhiteSpace(currentPage.GetSocialImage()) ? this.settingKeyRepository.GetDefaulSocialImage() : currentPage.GetSocialImage();

                        model.TwitterCardImageUrl =
                            string.IsNullOrWhiteSpace(currentPage.GetSocialImage()) ? this.settingKeyRepository.GetDefaulSocialImage() : currentPage.GetSocialImage();

                        model.TwitterCard = this.settingKeyRepository.GetTwitterCard();
                        model.TwitterHandle = this.settingKeyRepository.GetTwitterHandle();

                        if (currentPage is IPageType_BasePageType basePageType)
                        {
                            model.Title =
                                basePageType.Title;

                            model.ImageAlt =
                                basePageType.PageImageAltText;
                        }
                    }

                    return vc.View(model);
                });
        }

        /// <summary>
        /// Logs the request URL when the page data is not available.
        /// </summary>
        /// <param name="requestUrl">
        /// The request url.
        /// </param>
        [LoggerMessage(
             EventId = 1,
             Level = LogLevel.Information,
             Message = "The page data is not available. Request URL: {requestUrl}")]
        partial void LogPageDataIsNotAvailable(
            string requestUrl);

        /// <summary>
        /// Logs the exception when the page data is not available.
        /// </summary>
        /// <param name="ex">
        /// The exception.
        /// </param>
        [LoggerMessage(
             EventId = 2,
             Level = LogLevel.Error,
             Message = "The page data is not available.")]
        partial void LogPageDataIsNotAvailableException(
            InvalidOperationException ex);
    }
}
#nullable restore
