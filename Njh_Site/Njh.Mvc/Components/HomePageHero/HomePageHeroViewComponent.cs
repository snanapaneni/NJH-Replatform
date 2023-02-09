using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc.PageTemplates;
using Microsoft.AspNetCore.Mvc;
using Njh.Mvc.Models;
using Njh.Mvc.Models.PageTemplateProperties;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.HomePageHero
{
    public class HomePageHeroViewComponent : SafeViewComponent<HomePageHeroViewComponent>
    {
        private readonly IPageDataContextRetriever dataRetriever;
        private readonly IPageTemplatePropertiesRetriever templatePropertiesRetriever;

        public HomePageHeroViewComponent(ILogger<HomePageHeroViewComponent> logger, IViewComponentErrorVisibility viewComponentErrorVisibility, IPageDataContextRetriever dataRetriever, IPageTemplatePropertiesRetriever templatePropertiesRetriever)
            : base(logger, viewComponentErrorVisibility)
        {
            this.dataRetriever = dataRetriever;
            this.templatePropertiesRetriever = templatePropertiesRetriever;
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
                    var currentPage = vc.dataRetriever.Retrieve<TreeNode>()?.Page;
                    var pageProperties = vc.templatePropertiesRetriever.Retrieve<HomePageTemplateProperties>();

                    //HomePageHeroViewModel model = new ()
                    //{
                    //    PlaySpeed = currentPage?.GetIntegerValue("SliderPlaySpeed", 0) ?? 0,
                    //    ImageSlides = new List<object> { new object(), new object() },
                    //};

                    HomePageHeroViewModel model = new ()
                    {
                        PlaySpeed = pageProperties?.SliderPlaySpeed ?? 0,
                        ImageSlides = new List<object> { new object(), new object() },
                    };

                    return vc.View("~/Views/Shared/HomePageHero/HomePageHero.cshtml", model);
                });
        }
    }
}
