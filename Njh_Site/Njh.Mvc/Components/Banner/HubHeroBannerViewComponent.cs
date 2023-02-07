using CMS.MediaLibrary;
using CMS.SiteProvider;
using DocumentFormat.OpenXml.Vml;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Mvc.Components.Image;
using Njh.Mvc.Models.Widgets;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.Banner
{
    public class HubHeroBannerViewComponent : SafeViewComponent<HubHeroBannerViewComponent>
    {
        /// <summary>
        /// The widget identifier.
        /// </summary>
        public const string Identifier =
            "Njh.HubHeroBannerWidget";

        private readonly IMediaFileInfoProvider mediaFileInfo;

        public HubHeroBannerViewComponent(
            IMediaFileInfoProvider mediaFileInfo,
            ILogger<HubHeroBannerViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.mediaFileInfo = mediaFileInfo ??
                throw new ArgumentNullException(nameof(mediaFileInfo));
        }

        public IViewComponentResult Invoke(ComponentViewModel<HubHeroBannerViewComponentProperties> componentProperties)
        {
            return this.TryInvoke((vc) =>
            {
                var props = componentProperties?.Properties
                    ?? new HubHeroBannerViewComponentProperties();

                // get the image URL, width, and height from the media file object
                var imageSourceGuid = props.ImageSource.FirstOrDefault()?.FileGuid ?? Guid.Empty;
                MediaFileInfo mediaFile = this.mediaFileInfo.Get(imageSourceGuid, SiteContext.CurrentSiteID);

                string imageSourceUrl = string.Empty;
                int imageWidth = 0;
                int imageHeight = 0;

                if (mediaFile != null)
                {
                    imageSourceUrl = MediaLibraryHelper.GetDirectUrl(mediaFile);
                    imageWidth = mediaFile.FileImageWidth;
                    imageHeight = mediaFile.FileImageHeight;
                }

                // TODO do we need properties for the rich text part of the banner component?
                HubHeroBannerViewModel bannerData = new ()
                {
                    ImageSource = imageSourceUrl,
                    ImageAltText = props.ImageAltText,
                    ImageWidth = imageWidth,
                    ImageHeight = imageHeight,
                    BannerRichText = props.BannerRichText,
                };

                return vc.View(
                    "~/Views/Shared/Widgets/_HubHeroBanner.cshtml",
                    bannerData);
            });
        }
    }
}
