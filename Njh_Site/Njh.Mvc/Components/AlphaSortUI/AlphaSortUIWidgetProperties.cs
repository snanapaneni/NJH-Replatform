namespace Njh.Mvc.Components.AlphaSortUI
{
    using Kentico.Components.Web.Mvc.FormComponents;
    using Kentico.Forms.Web.Mvc;
    using Kentico.PageBuilder.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class AlphaSortUIWidgetProperties : IWidgetProperties
    {
        /// <summary>
        /// Gets or sets the pages path.
        /// </summary>
        [EditingComponent(
            PathSelector.IDENTIFIER,
            Order = 1,
            Label = "Path to Items folder")]
        public IList<PathSelectorItem> PageItemsFolderPath { get; set; } =
            new List<PathSelectorItem>();

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 2, Label = "Include all children")]
        [EditingComponentProperty(nameof(CheckBoxProperties.DefaultValue), false)]
        public bool IncludeAllChildren { get; set; } = false;

        [EditingComponent(
            ObjectSelector.IDENTIFIER,
            Order = 3,
            Label = "{$NJH.Widget.Properties.PageTypeSelector$}",
            ExplanationText = "{$NJH.Widget.Properties.PageTypeSelector.Explanation$}")]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.ObjectType), "cms.documenttype")]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.OrderBy), new string[] { "ClassDisplayName ASC" })]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.MaxItemsLimit), 10)]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.IdentifyObjectByGuid), false)]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.Required), false)]
        [Required(AllowEmptyStrings = true)]
        public IEnumerable<ObjectSelectorItem> ItemsPageTypes { get; set; } = Enumerable.Empty<ObjectSelectorItem>();

        [EditingComponent(
            ObjectSelector.IDENTIFIER,
            Order = 4,
            Label = "{$NJH.Widget.Properties.CategorySelector$}",
            ExplanationText = "{$NJH.Widget.Properties.CategorySelector.Explanation$}")]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.ObjectType), "cms.category")]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.OrderBy), new string[] { "CategoryNamePath ASC" })]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.MaxItemsLimit), 5)]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.IdentifyObjectByGuid), true)]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.Required), false)]
        [Required(AllowEmptyStrings = true)]
        public IEnumerable<ObjectSelectorItem> ItemsCategories { get; set; } = Enumerable.Empty<ObjectSelectorItem>();
    }
}
