using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace Njh.Mvc.Components.Accordion
{
    public class AccordionComponentProperties : IWidgetProperties
    {
        [EditingComponent(PathSelector.IDENTIFIER, Order = 1, Label = "Path")]
        [EditingComponentProperty(nameof(PathSelectorProperties.Required), true)]
        public IEnumerable<PathSelectorItem> PagePaths { get; set; } = Enumerable.Empty<PathSelectorItem>();
    }
}
