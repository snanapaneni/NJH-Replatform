using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models.DTOs;
using Njh.Kernel.Services;
using Njh.Mvc.Components.Image;
using Njh.Mvc.Models.Widgets;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.AdditionalLinks
{
    public class AdditionalLinksComponent : SafeViewComponent<AdditionalLinksComponent>
    {
        /// <summary>
        /// The widget identifier.
        /// </summary>
        public const string Identifier =
            "Njh.AdditionalLinksWidget";

        private readonly INavigationService navigationService;

        public AdditionalLinksComponent(ILogger<AdditionalLinksComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility, INavigationService navigationService)
            : base(logger, viewComponentErrorVisibility)
        {
            this.navigationService = navigationService;
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
        public IViewComponentResult Invoke(ComponentViewModel<AdditionalLinksComponentProperties> componentProperties)
        {
            return this.TryInvoke((vc) =>
            {
                var path = componentProperties.Properties.PagePaths.Select(p => p.NodeAliasPath).FirstOrDefault();
                var model = new AdditionalLinksViewModel()
                {
                    Title = componentProperties.Properties.Title,
                    Items = string.IsNullOrWhiteSpace(path) ? new List<NavItem>() : navigationService.GetNavItems<PageType_NavItem>(path),
                };

                return vc.View("~/Views/Shared/Widgets/_AdditionalLinks.cshtml", model);
            });
        }
    }
}
