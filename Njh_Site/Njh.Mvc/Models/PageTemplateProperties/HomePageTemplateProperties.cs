using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;

namespace Njh.Mvc.Models.PageTemplateProperties
{
    public class HomePageTemplateProperties : PageTemplatePropertiesBase
    {
        [EditingComponent(IntInputComponent.IDENTIFIER, Order = 1, Label = "{$NJH.Template.Properties.SliderPlaySpeed$}", DefaultValue = 0, ExplanationText = "{$NJH.Template.Properties.SliderAutoPlayExplanation$}")]
        public int SliderPlaySpeed { get; set; }

        [EditingComponent(PathSelector.IDENTIFIER, Order = 2, Label = "{$NJH.Template.Properties.SliderImagePath$}")]
        public IEnumerable<PathSelectorItem> PagePaths { get; set; } = Enumerable.Empty<PathSelectorItem>();
    }
}
