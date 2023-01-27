namespace Njh.Mvc.Models.Widgets
{
    using Njh.Mvc.Components;

    /// <summary>
    /// Implements the view model for the <see cref="ImageViewComponent"/> widget.
    /// </summary>
    public class ImageViewModel
    {
        /// <summary>
        /// Gets or sets image source; expect a media library URL.
        /// </summary>
        public string? ImageSource { get; set; }

        // TODO make width & height visible properties, or get from the media library URL?

        /// <summary>
        /// Gets or sets image width.
        /// </summary>
        public int ImageWidth { get; set; }

        /// <summary>
        /// Gets or sets image height.
        /// </summary>
        public int ImageHeight { get; set; }

        /// <summary>
        /// Gets or sets alt text for the image.
        /// </summary>
        public string? ImageAltText { get; set; }

        /// <summary>
        /// Gets or sets image caption text for accessibility.
        /// </summary>
        public string? ImageCaption { get; set; }

        /// <summary>
        /// Gets or sets image alignment in text.
        /// </summary>
        public string? Alignment { get; set; }

        /// <summary>
        /// Gets or sets optional link URL.
        /// </summary>
        public string? ImageLinkUrl { get; set; }

        /// <summary>
        /// Gets or sets optional link title for accessibility.
        /// </summary>
        public string? ImageLinkTitle { get; set; }
    }
}
