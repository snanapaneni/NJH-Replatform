namespace Njh.Mvc.Components
{
    using System;
    using CMS.DocumentEngine;
    using CMS.MediaLibrary;
    using CMS.SiteProvider;
    using Kentico.Content.Web.Mvc;
    using Kentico.PageBuilder.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Njh.Kernel.Extensions;
    using Njh.Kernel.Kentico.Models.PageTypes;
    using Njh.Kernel.Models.Dto;
    using Njh.Kernel.Services;
    using Njh.Mvc.Models;
    using Njh.Mvc.Models.Widgets;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;

    /// <summary>
    /// Image widget component displays an image from media library selection; optional alt text, caption, etc. and optionally is hyperlinked.
    /// </summary>
    public class ImageViewComponent : SafeViewComponent<ImageViewComponent>
    {
        /// <summary>
        /// The widget identifier.
        /// </summary>
        public const string Identifier =
            "Njh.ImageWidget";

        private readonly IMediaFileInfoProvider mediaFileInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageViewComponent"/> class.
        /// </summary>
        /// <param name="mediaFileInfo">Media File Info provider.</param>
        /// <param name="logger">A logger for errors.</param>
        /// <param name="viewComponentErrorVisibility">View component error visibility methods.</param>
        /// <exception cref="ArgumentNullException">Throws exception if data retriever is missing or null.</exception>
        public ImageViewComponent(
            IMediaFileInfoProvider mediaFileInfo,
            ILogger<ImageViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.mediaFileInfo = mediaFileInfo ??
                throw new ArgumentNullException(nameof(mediaFileInfo));
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
        public IViewComponentResult Invoke(ComponentViewModel<ImageComponentProperties> componentProperties)
        {
            return this.TryInvoke((vc) =>
            {
                var props = componentProperties?.Properties
                    ?? new ImageComponentProperties();

                // get the image URL, width, and height from the media file object
                var imageSourceGuid = props.ImageSource.FirstOrDefault()?.FileGuid ?? Guid.Empty;
                MediaFileInfo mediaFile = mediaFileInfo.Get(imageSourceGuid, SiteContext.CurrentSiteID);

                string imageSourceUrl = "/NJH/media/assets/TEST-IT-Crowd-1920x1080.jpg";
                int imageWidth = 0;
                int imageHeight = 0;

                if (mediaFile != null)
                {
                    imageSourceUrl = MediaLibraryHelper.GetDirectUrl(mediaFile);
                    imageWidth = mediaFile.FileImageWidth;
                    imageHeight = mediaFile.FileImageHeight;
                }

                ImageViewModel imageData = new ()
                {
                    ImageSource = imageSourceUrl,
                    ImageAltText = props.ImageAltText,
                    ImageWidth = imageWidth,
                    ImageHeight = imageHeight,
                    Alignment = props.Alignment,
                    ImageCaption = props.ImageCaption,
                    ImageLinkUrl = props.ImageLinkUrl,
                    ImageLinkTitle = props.ImageLinkTitle,
                };

                return vc.View(
                    "~/Views/Shared/Widgets/_Image.cshtml",
                    imageData);
            });
        }
    }
}
