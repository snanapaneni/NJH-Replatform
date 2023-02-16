namespace Njh.Mvc.Models.Widgets
{
    using Njh.Kernel.Models.Dto;
    using Njh.Mvc.Components.Image;

    /// <summary>
    /// Implements the view model for the <see cref="ImageSliderViewComponent"/> widget.
    /// </summary>
    public class ImageSliderViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether to autoplay the slide show.
        /// </summary>
        public bool Autoplay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the play / pause controls of the slide show.
        /// Recommended value 'true'; must be 'true' if Autoplay is false.
        /// </summary>
        public bool ShowControls { get; set; } = true;

        /// <summary>
        /// Gets or sets the time to display each slide, in milliseconds.
        /// </summary>
        public int SlideDuration { get; set; }

        /// <summary>
        /// Gets or sets the list of image slides; populated from NJH Slider Image documents in the CMS Tree.
        /// </summary>
        public List<ImageSlide>? Slides { get; set; }
    }
}
