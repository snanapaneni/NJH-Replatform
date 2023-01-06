﻿

namespace Njh.Mvc.Models.SectionProperties
{
    using Kentico.Forms.Web.Mvc;
    using Kentico.PageBuilder.Web.Mvc;
    using Njh.Mvc.Models.FormComponents;

    public class SectionPropertiesBase : ISectionProperties
    {
        /// <summary>
        /// Gets or sets the Theme.
        /// </summary>

        [EditingComponent(CustomTableItemSelector.IDENTIFIER, Order = 1, Label = "Theme")]
        [EditingComponentProperty(nameof(CustomTableItemSelectorProperties.ClassName), "Njh.CustomTable_SectionColorThemes")]
        [EditingComponentProperty(nameof(CustomTableItemSelectorProperties.DisplayColumn), "Name")]
        [EditingComponentProperty(nameof(CustomTableItemSelectorProperties.FieldToSave), "ItemGUID")]
        [EditingComponentProperty(nameof(CustomTableItemSelectorProperties.Multiple), false)]
        public string ThemeGuid { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 2, Label = "Has Padding")]
        [EditingComponentProperty(nameof(CheckBoxProperties.DefaultValue), true)]
        public bool HasPadding { get; set; } = true;

    }
}
