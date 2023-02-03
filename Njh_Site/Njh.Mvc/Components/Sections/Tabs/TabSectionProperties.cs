using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace Njh.Mvc.Components.Sections.Tabs
{
    public class TabSectionProperties : ISectionProperties
    {
        /// <summary>
        /// Gets or sets the list of tabs.
        /// </summary>
        [EditingComponent(TextAreaComponent.IDENTIFIER, Order = 1, Label = "List of Tabs", ExplanationText = "Enter the title for each tab on separate lines.")]
        [EditingComponentProperty(nameof(TextAreaProperties.Required), true)]
        public string ListOfTabs { get; set; }
    }
}
