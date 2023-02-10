using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Mvc.Models.Widgets;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.DonationCTA
{
    public class DonationCtaComponent : SafeViewComponent<DonationCtaComponent>
    {
        /// <summary>
        /// The widget identifier.
        /// </summary>
        public const string Identifier =
            "Njh.DonationCta";

        public DonationCtaComponent(ILogger<DonationCtaComponent> logger, IViewComponentErrorVisibility viewComponentErrorVisibility) : base(logger, viewComponentErrorVisibility)
        {
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
        public IViewComponentResult Invoke(ComponentViewModel<DonationCtaComponentProperties> componentProperties)
        {
            return this.TryInvoke((vc) =>
            {
                var model = new DonationCtaViewModel
                {
                    Content = componentProperties.Properties?.Content,
                    ButtonText = componentProperties.Properties?.ButtonText,
                    DonationUrl = componentProperties.Properties?.DonationUrl,
                };
                return vc.View("~/Views/Shared/Widgets/_DonationCta.cshtml", model);
            });
        }
    }
}
