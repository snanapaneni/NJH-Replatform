using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace Njh.Mvc.Components.DonationCTA
{
    public class DonationCtaComponentProperties : IWidgetProperties
    {
        [EditingComponent(TextAreaComponent.IDENTIFIER, Order = 1, Label = "{$NJH.Widget.Properties.Content$}")]
        [EditingComponentProperty(nameof(TextAreaProperties.Required), true)]
        public string Content { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "{$NJH.Widget.Properties.ButtonText$}")]
        [EditingComponentProperty(nameof(TextInputProperties.Required), true)]
        public string ButtonText { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 3, Label = "{$NJH.Widget.Properties.DonationUrl$}")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Required), true)]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page)]
        public string DonationUrl { get; set; }
    }
}
