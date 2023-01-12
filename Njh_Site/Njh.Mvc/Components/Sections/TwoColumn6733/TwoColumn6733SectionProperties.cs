using Kentico.Forms.Web.Mvc;
using Njh.Mvc.Models.SectionProperties;

namespace Njh.Mvc.Components.Sections.TwoColumn6733
{
    public class TwoColumn6733SectionProperties : SectionPropertiesBase
    {
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 3, Label = "Swap Columns")]
        [EditingComponentProperty(nameof(CheckBoxProperties.DefaultValue), false)]
        public bool SwapColumns { get; set; } = false;
    }
}
