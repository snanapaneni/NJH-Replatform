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
        private readonly IImageSlideService imageSlideService;

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
            IViewComponentErrorVisibility viewComponentErrorVisibility, IImageSlideService imageSlideService)
            : base(logger, viewComponentErrorVisibility)
        {
            this.mediaFileInfo = mediaFileInfo ??
                throw new ArgumentNullException(nameof(mediaFileInfo));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));

            this.context = context ??
                throw new ArgumentNullException(nameof(context));
            this.imageSlideService = imageSlideService;
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

                // load data only if path has been set
                var cachedSlides = string.IsNullOrEmpty(slidesPath)
                    ? new List<ImageSlide>()
                    : this.imageSlideService.GetSlides(slidesPath).ToList();

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
