using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc.PageTemplates;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Models.Dto;
using Njh.Kernel.Services;
using Njh.Mvc.Components.Image;
using Njh.Mvc.Models;
using Njh.Mvc.Models.PageTemplateProperties;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.HomePageHero
{
    public class HomePageHeroViewComponent : SafeViewComponent<HomePageHeroViewComponent>
    {
        private readonly IPageDataContextRetriever dataRetriever;
        private readonly IPageTemplatePropertiesRetriever templatePropertiesRetriever;
        private readonly IImageSlideService imageSlideService;

        public HomePageHeroViewComponent(ILogger<HomePageHeroViewComponent> logger, IViewComponentErrorVisibility viewComponentErrorVisibility, IPageDataContextRetriever dataRetriever, IPageTemplatePropertiesRetriever templatePropertiesRetriever, IImageSlideService imageSlideService)
            : base(logger, viewComponentErrorVisibility)
        {
            this.dataRetriever = dataRetriever;
            this.templatePropertiesRetriever = templatePropertiesRetriever;
            this.imageSlideService = imageSlideService;
        }

        /// <summary>
        /// Renders the view component.
        /// </summary>
        /// <returns>
        /// The view component result.
        /// </returns>
        public IViewComponentResult Invoke()
        {
            return
                this.TryInvoke((vc) =>
                {
                    var pageProperties = vc.templatePropertiesRetriever.Retrieve<HomePageTemplateProperties>();
                    var path = pageProperties?.PagePaths.Select(p => p.NodeAliasPath).FirstOrDefault();

                    HomePageHeroViewModel model = new ()
                    {
                        PlaySpeed = pageProperties?.SliderPlaySpeed ?? 0,
                        ImageSlides = string.IsNullOrWhiteSpace(path) ? new List<ImageSlide>() : imageSlideService.GetSlides(path),
                    };

                    return vc.View("~/Views/Shared/HomePageHero/HomePageHero.cshtml", model);
                });
        }
    }
}
