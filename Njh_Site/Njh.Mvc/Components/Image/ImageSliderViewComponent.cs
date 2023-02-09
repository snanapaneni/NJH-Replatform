using CMS.DocumentEngine;
using CMS.DocumentEngine.Internal;
using CMS.Helpers;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using DocumentFormat.OpenXml.Vml;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Constants;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models;
using Njh.Kernel.Models.Dto;
using Njh.Kernel.Services;
using Njh.Mvc.Models.Widgets;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.Image
{
    /// <summary>
    /// NJH Image Slider widget displays slides from a selected path to NJH Image Slide documents in the CMS Tree.
    /// The widget is configurable for whether it autoplays and shows play/pause controls, and for duration of each slide.
    /// </summary>
    public class ImageSliderViewComponent : SafeViewComponent<ImageSliderViewComponent>
    {
        /// <summary>
        /// The widget identifier.
        /// </summary>
        public const string Identifier =
            "Njh.ImageSliderWidget";

        // Use mediaFileInfo to get information about the Image selection in each Slide
        private readonly IMediaFileInfoProvider mediaFileInfo;
        private readonly ICacheService cacheService;
        private readonly ContextConfig context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSliderViewComponent"/> class.
        /// </summary>
        /// <param name="mediaFileInfo">Media File Info provider.</param>
        /// <param name="cacheService">The cache service.</param>
        /// <param name="context">The context config.</param>
        /// <param name="logger">A logger for errors.</param>
        /// <param name="viewComponentErrorVisibility">View component error visibility methods.</param>
        /// <exception cref="ArgumentNullException">Throws exception if media file info provider is missing or null.</exception>
        public ImageSliderViewComponent(
            IMediaFileInfoProvider mediaFileInfo,
            ICacheService cacheService,
            ContextConfig context,
            ILogger<ImageSliderViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.mediaFileInfo = mediaFileInfo ??
                throw new ArgumentNullException(nameof(mediaFileInfo));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));

            this.context = context ??
                throw new ArgumentNullException(nameof(context));
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
        public IViewComponentResult Invoke(ComponentViewModel<ImageSliderComponentProperties> componentProperties)
        {
            return this.TryInvoke((vc) =>
            {
                var props = componentProperties?.Properties
                    ?? new ImageSliderComponentProperties();

                // get selected path to the NJH Slider Images
                var slidesPath = props.SlidePaths.FirstOrDefault()?.NodeAliasPath;
                var slidePageType = PageType_SliderImage.CLASS_NAME;

                var cacheParameters = new CacheParameters
                {
                    CacheKey = string.Format(
                        DataCacheKeys.DataSetByPathByType,
                        "ImageSlider",
                        slidesPath,
                        slidePageType),
                    IsCultureSpecific = true,
                    CultureCode = this.context?.CultureName,
                    IsSiteSpecific = true,
                    SiteName = this.context?.Site?.SiteName,
                };

                var cachedSlides = this.cacheService.Get(
                    cp =>
                    {
                        var slideDocuments = DocumentHelper.GetDocuments()
                            .OnCurrentSite()
                            .CombineWithDefaultCulture()
                            .Published()
                            .WithCoupledColumns()
                            .LatestVersion()
                            .Path(slidesPath, PathTypeEnum.Children)
                            .Type(slidePageType)
                            .OrderBy("NodeOrder")
                            .ToList();

                        var imageSlides = new List<ImageSlide>();

                        foreach (var slideDoc in slideDocuments)
                        {
                            // NOTE: media selection in NJH Slider Image page type stores image path, not a GUID
                            var imageSourcePath = slideDoc.GetStringValue("SlideImage", string.Empty);
                            if (!string.IsNullOrEmpty(imageSourcePath))
                            {
                                var imageSourceUrl = URLHelper.ResolveUrl(imageSourcePath);
                                // TODO is there any way to get width and height of the media image selection?
                                var imageWidth = 0;
                                var imageHeight = 0;

                                var linkUrl = slideDoc.GetStringValue("LinkUrl", string.Empty);
                                if (!string.IsNullOrEmpty(linkUrl))
                                {
                                    linkUrl = URLHelper.ResolveUrl(linkUrl);
                                }

                                imageSlides.Add(new ImageSlide()
                                {
                                    SlideTitle = slideDoc.GetStringValue("Title", string.Empty),
                                    ImageSource = imageSourceUrl,
                                    ImageWidth = imageWidth,
                                    ImageHeight = imageHeight,
                                    ImageAltText = slideDoc.GetStringValue("ImageAltText", string.Empty),
                                    ImageDescription = slideDoc.GetStringValue("Description", string.Empty),
                                    SlideLinkUrl = linkUrl,
                                    SlideLinkTitle = slideDoc.GetStringValue("LinkTitle", string.Empty),
                                });
                            }
                        }

                        // set dependency: all documents in slider should bust cache
                        cp.CacheDependencies = slideDocuments.Select(item =>
                                this.cacheService.GetCacheKey(
                                string.Format(DummyCacheKeys.PageSiteNodeAlias, this.context?.Site?.SiteName, item.NodeAliasPath),
                                cacheParameters.CultureCode,
                                cacheParameters.SiteName))
                                .ToList();

                        return imageSlides;
                    }, cacheParameters);

                // read SlideDuration from widget config; default 3000 milliseconds, minimum 1000.
                int slideDuration = 3000;
                if (int.TryParse(props.SlideDuration, out slideDuration))
                {
                    slideDuration = Math.Max(slideDuration, 1000);
                }

                ImageSliderViewModel slideModel = new ()
                {
                    Slides = cachedSlides,
                    Autoplay = props.Autoplay,
                    ShowControls = props.ShowControls,
                    SlideDuration = slideDuration,
                };

                return vc.View(
                    "~/Views/Shared/Widgets/_ImageSlider.cshtml",
                    slideModel);
            });
        }
    }
}
