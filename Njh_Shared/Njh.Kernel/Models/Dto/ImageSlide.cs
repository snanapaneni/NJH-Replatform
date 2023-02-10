namespace Njh.Kernel.Models.Dto
{
    /// <summary>
    /// Represents a single image slide, for use in e.g. Image Slider widget.
    /// </summary>
    public class ImageSlide
    {
        /// <summary>
        /// Gets or sets the Title of the image slide.
        /// </summary>
        public string? SlideTitle { get; set; }

        /// <summary>
        /// Gets or sets image source; expect a media library URL, not the selected media file object.
        /// </summary>
        public string? ImageSource { get; set; }

        /// <summary>
        /// Gets or sets mobile image source; expect a media library URL, not the selected media file object.
        /// </summary>
        public string? MobileImageSource { get; set; }

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
        /// Gets or sets image content / description rich text.
        /// </summary>
        public string? ImageDescription { get; set; }

        /// <summary>
        /// Gets or sets optional link URL.
        /// </summary>
        public string? SlideLinkUrl { get; set; }

        /// <summary>
        /// Gets or sets optional link title for accessibility.
        /// </summary>
        public string? SlideLinkTitle { get; set; }
    }
}
