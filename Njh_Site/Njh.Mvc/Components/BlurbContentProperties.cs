using Kentico.Forms.Web.Mvc;
using System.Reflection.Emit;

namespace Njh.Mvc.Components
{
    public class BlurbContentProperties : Kentico.PageBuilder.Web.Mvc.IWidgetProperties
    {
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 1, Label = "Show Blurb?")]
        [EditingComponentProperty(nameof(CheckBoxProperties.DefaultValue), true)]
        public bool ShowBlurb { get; set; } = true;

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 2, Label = "Show Content?")]
        [EditingComponentProperty(nameof(CheckBoxProperties.DefaultValue), true)]
        public bool ShowContent { get; set; } = true;
    }
}
