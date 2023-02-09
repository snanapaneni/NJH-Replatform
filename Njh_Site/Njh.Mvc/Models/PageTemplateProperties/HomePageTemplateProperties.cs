using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;

namespace Njh.Mvc.Models.PageTemplateProperties
{
    public class HomePageTemplateProperties : PageTemplatePropertiesBase
    {
        [EditingComponent(IntInputComponent.IDENTIFIER, Order = 1, Label = "Slider Play Speed", DefaultValue = 0, ExplanationText = "0 to turn off autoplay.")]
        public int SliderPlaySpeed { get; set; }

        [EditingComponent(PathSelector.IDENTIFIER, Order = 2, Label = "Slider Images Path")]
        public IEnumerable<PathSelectorItem> PagePaths { get; set; } = Enumerable.Empty<PathSelectorItem>();
    }
}
