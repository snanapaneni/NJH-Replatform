using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Models.Dto;
using Njh.Kernel.Services;
using Njh.Mvc.Components.Image;
using Njh.Mvc.Models.Widgets;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.Accordion
{
    public class AccordionComponent : SafeViewComponent<AccordionComponent>
    {
        /// <summary>
        /// The widget identifier.
        /// </summary>
        public const string Identifier =
            "Njh.AccordionWidget";

        private readonly IAccordionService accordionService;

        public AccordionComponent(ILogger<AccordionComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility, IAccordionService accordionService)
            : base(logger, viewComponentErrorVisibility)
        {
            this.accordionService = accordionService;
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
        public IViewComponentResult Invoke(ComponentViewModel<AccordionComponentProperties> componentProperties)
        {
            return this.TryInvoke((vc) =>
            {
                var path = componentProperties.Properties.PagePaths.Select(p => p.NodeAliasPath).FirstOrDefault();
                var model = new AccordionViewModel
                {
                    Items = string.IsNullOrWhiteSpace(path) ? new List<AccordionItem>() : accordionService.GetItems($"{path}/%"),
                };
                return vc.View("~/Views/Shared/Widgets/_Accordion.cshtml", model);
            });
        }
    }
}
