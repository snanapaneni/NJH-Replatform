namespace Njh.Mvc.Models
{
    using Njh.Mvc.Components;

    /// <summary>
    /// Implements the view model for the <see cref="BlurbContentViewComponent"/> widget.
    /// </summary>
    public class BlurbContentViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether to show or hide the page Blurb.
        /// Read from widget properties.
        /// </summary>
        public bool ShowBlurb { get; set; }

        /// <summary>
        /// Gets or sets a value for the page's Blurb data.
        /// Read from page data.
        /// </summary>
        public string? PageBlurbData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show or hide the page Content.
        /// Read from widget properties.
        /// </summary>
        public bool ShowContent { get; set; }

        /// <summary>
        /// Gets or sets a value for the page's Content data.
        /// Read from page data.
        /// </summary>
        public string? PageContentData { get; set; }
    }
}
