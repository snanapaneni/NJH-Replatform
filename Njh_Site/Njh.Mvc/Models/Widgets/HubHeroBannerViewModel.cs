namespace Njh.Mvc.Models.Widgets
{
    using Njh.Mvc.Components.Banner;

    /// <summary>
    /// Implements the view model for the <see cref="HubHeroBannerViewComponent"/> widget.
    /// </summary>
    public class HubHeroBannerViewModel
    {
        /// <summary>
        /// Gets or sets image source; expect a media library URL, not the selected media file object.
        /// </summary>
        public string? ImageSource { get; set; }

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
        /// Gets or sets banner rich text.
        /// </summary>
        public string? BannerRichText { get; set; }
    }
}
