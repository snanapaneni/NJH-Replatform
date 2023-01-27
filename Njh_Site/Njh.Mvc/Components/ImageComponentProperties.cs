namespace Njh.Mvc.Components
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
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 1, Label = "Select an Image")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        public IEnumerable<MediaFilesSelectorItem> ImageSource { get; set; } = Enumerable.Empty<MediaFilesSelectorItem>();

        /// <summary>
        /// Gets or sets alt text for the image.
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "Image alt text", ExplanationText = "Accessible text; optional but recommended; max 255 chars")]
        [EditingComponentProperty(nameof(TextInputProperties.DefaultValue), "")]
        [EditingComponentProperty(nameof(TextInputProperties.Size), 255)]
        public string? ImageAltText { get; set; }

        /// <summary>
        /// Gets or sets optional image caption text.
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "Image caption", ExplanationText = "Optional visible caption; max 1000 chars")]
        [EditingComponentProperty(nameof(TextInputProperties.DefaultValue), "")]
        [EditingComponentProperty(nameof(TextInputProperties.Size), 1000)]
        public string? ImageCaption { get; set; }

        /// <summary>
        /// Gets or sets image alignment in text.
        /// </summary>
        [EditingComponent(DropDownComponent.IDENTIFIER, Order = 4, Label = "Image alignment", ExplanationText = "None, left, or right")]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), ";None\r\nleft;Left\r\nright;Right")]
        public string? Alignment { get; set; }

        /// <summary>
        /// Gets or sets optional link URL.
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 5, Label = "Image link URL", ExplanationText = "Optional image link; max 500 chars")]
        [EditingComponentProperty(nameof(TextInputProperties.DefaultValue), "")]
        [EditingComponentProperty(nameof(TextInputProperties.Size), 500)]
        public string? ImageLinkUrl { get; set; }

        /// <summary>
        /// Gets or sets optional link title text.
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 6, Label = "Image link title", ExplanationText = "Optional accessible link title; max 255 chars")]
        [EditingComponentProperty(nameof(TextInputProperties.DefaultValue), "")]
        [EditingComponentProperty(nameof(TextInputProperties.Size), 255)]
        public string? ImageLinkTitle { get; set; }
    }
}
