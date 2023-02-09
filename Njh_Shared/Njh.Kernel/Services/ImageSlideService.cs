using CMS.DocumentEngine;
using CMS.Helpers;
using Njh.Kernel.Constants;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models;
using Njh.Kernel.Models.Dto;

namespace Njh.Kernel.Services
{
    public class ImageSlideService : ServiceBase, IImageSlideService
    {
        private readonly ICacheService cacheService;
        private readonly ContextConfig context;

        public ImageSlideService(ICacheService cacheService, ContextConfig context)
        {
            this.cacheService = cacheService;
            this.context = context;
        }

        public IEnumerable<ImageSlide> GetSlides(string path)
        {
            var slidePageType = PageType_SliderImage.CLASS_NAME;

            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByPathByType,
                    "ImageSlider",
                    path,
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
                        .Path(path, PathTypeEnum.Children)
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

                            var mobileImageSrc = slideDoc.GetStringValue(nameof(PageType_SliderImage.MobileSlideImage), string.Empty);
                            var mobileImageSrcUrl = string.Empty;
                            if (!string.IsNullOrWhiteSpace(mobileImageSrc))
                            {
                                mobileImageSrcUrl = URLHelper.ResolveUrl(mobileImageSrcUrl);
                            }

                            var linkUrl = slideDoc.GetStringValue("LinkUrl", string.Empty);
                            if (!string.IsNullOrEmpty(linkUrl))
                            {
                                linkUrl = URLHelper.ResolveUrl(linkUrl);
                            }

                            imageSlides.Add(new ImageSlide()
                            {
                                SlideTitle = slideDoc.GetStringValue("Title", string.Empty),
                                ImageSource = imageSourceUrl,
                                MobileImageSource = mobileImageSrcUrl,
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

            return cachedSlides;
        }
    }
}
