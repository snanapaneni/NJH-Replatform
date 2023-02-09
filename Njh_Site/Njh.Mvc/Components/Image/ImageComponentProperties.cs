namespace Njh.Mvc.Components.Image
{
    using Kentico.Components.Web.Mvc.FormComponents;
    using Kentico.Forms.Web.Mvc;
    using Kentico.PageBuilder.Web.Mvc;
    using Njh.Mvc.Models.FormComponents;

    /// <summary>
    /// Properties for the ImageComponent widget.
    /// </summary>
    public class ImageComponentProperties : IWidgetProperties
    {
        /// <summary>
        /// Gets or sets the image source as a media file selection.
        /// </summary>
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 1, Label = "{$NJH.ImageSourceSelector.Properties.Label$}")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        public IEnumerable<MediaFilesSelectorItem> ImageSource { get; set; } = Enumerable.Empty<MediaFilesSelectorItem>();

        /// <summary>
        /// Gets or sets alt text for the image.
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "{$NJH.Image.AltText.Label$}", ExplanationText = "{$NJH.Image.AltText.Explanation$}")]
        [EditingComponentProperty(nameof(TextInputProperties.DefaultValue), "")]
        [EditingComponentProperty(nameof(TextInputProperties.Size), 255)]
        [EditingComponentProperty(nameof(TextInputProperties.Required), true)]
        public string? ImageAltText { get; set; }

        /// <summary>
        /// Gets or sets optional image caption text.
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "{$NJH.Image.Caption.Label$}", ExplanationText = "{$NJH.Image.Caption.Explanation$}")]
        [EditingComponentProperty(nameof(TextInputProperties.DefaultValue), "")]
        [EditingComponentProperty(nameof(TextInputProperties.Size), 1000)]
        public string? ImageCaption { get; set; }

        /// <summary>
        /// Gets or sets image alignment in text.
        /// </summary>
        [EditingComponent(DropDownComponent.IDENTIFIER, Order = 4, Label = "{$NJH.Image.Alignment.Label$}", ExplanationText = "{$NJH.Image.Alignment.Explanation$}")]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), "{$NJH.Image.Alignment.DataSource$}")]
        public string? Alignment { get; set; }

        /// <summary>
        /// Gets or sets optional link URL.
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 5, Label = "{$NJH.Image.LinkUrl.Label$}", ExplanationText = "{$NJH.Image.LinkUrl.Explanation$}")]
        [EditingComponentProperty(nameof(TextInputProperties.DefaultValue), "")]
        [EditingComponentProperty(nameof(TextInputProperties.Size), 500)]
        public string? ImageLinkUrl { get; set; }

        /// <summary>
        /// Gets or sets optional link title text.
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 6, Label = "{$NJH.Image.LinkTitle.Label$}", ExplanationText = "{$NJH.Image.LinkTitle.Explanation$}")]
        [EditingComponentProperty(nameof(TextInputProperties.DefaultValue), "")]
        [EditingComponentProperty(nameof(TextInputProperties.Size), 255)]
        public string? ImageLinkTitle { get; set; }
    }
}
