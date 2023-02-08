namespace Njh.Mvc.Components.Banner
{
    using Kentico.Components.Web.Mvc.FormComponents;
    using Kentico.Forms.Web.Mvc;
    using Kentico.PageBuilder.Web.Mvc;
    using System.Reflection.Emit;

    /// <summary>
    /// Properties for the Hub Hero Banner widget.
    /// </summary>
    public class HubHeroBannerViewComponentProperties : IWidgetProperties
    {
        /// <summary>
        /// Gets or sets the image source as a media file selection.
        /// </summary>
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 1, Label = "{$NJH.ImageSourceSelector.Properties.Label$}", ExplanationText = "{$NJH.HubHeroBanner.ImageSource.Explanation$}")]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.Required), true)]
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
        /// Gets or sets banner rich text.
        /// </summary>
        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 3, Label = "{$NJH.HubHeroBanner.BannerRichText.Label$}", ExplanationText = "{$NJH.HubHeroBanner.BannerRichText.Explanation$}")]
        // NOTE: this default value doesn't show in RTE so leave it empty
        [EditingComponentProperty(nameof(RichTextComponentProperties.DefaultValue), "")]
        [EditingComponentProperty(nameof(RichTextComponentProperties.Required), true)]
        public string? BannerRichText { get; set; }
    }
}
