using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using System.Reflection.Emit;

namespace Njh.Mvc.Components.AdditionalLinks
{
    public class AdditionalLinksComponentProperties : IWidgetProperties
    {
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Title")]
        [EditingComponentProperty(nameof(TextInputProperties.Required), true)]
        public string Title { get; set; }

        [EditingComponent(PathSelector.IDENTIFIER, Order = 2, Label = "Path")]
        [EditingComponentProperty(nameof(PathSelectorProperties.Required), true)]
        public IEnumerable<PathSelectorItem> PagePaths { get; set; } = Enumerable.Empty<PathSelectorItem>();
    }
}
