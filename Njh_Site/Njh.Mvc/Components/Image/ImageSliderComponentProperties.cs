using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace Njh.Mvc.Components.Image
{
    /// <summary>
    /// Properties for the NJH Image Slider widget.
    /// </summary>
    public class ImageSliderComponentProperties : IWidgetProperties
    {
        /// <summary>
        /// Gets or sets the path to the slides in the CMS Tree.
        /// </summary>
        [EditingComponent(PathSelector.IDENTIFIER, Order = 1, Label = "{$NJH.PathSelector.Properties.Label$}", ExplanationText = "{$NJH.PathSelector.Properties.Explanation$}")]
        [EditingComponentProperty(nameof(PathSelector.MaxPagesLimit), 1)]
        public IEnumerable<PathSelectorItem> SlidePaths { get; set; } = Enumerable.Empty<PathSelectorItem>();

        /// <summary>
        /// Gets or sets a value indicating whether to autoplay the slide show.
        /// </summary>
        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 2, Label = "{$NJH.ImageSlider.Autoplay.Label$}")]
        [EditingComponentProperty(nameof(CheckBoxProperties.DefaultValue), true)]
        public bool Autoplay { get; set; } = true;

        /// <summary>
        /// Gets or sets the time to display each slide, in milliseconds.
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "{$NJH.ImageSlider.Duration.Label$}", ExplanationText = "{$NJH.ImageSlider.Duration.Explanation$}")]
        // [EditingComponentProperty(nameof(TextInputProperties.DataType), nameof(Int32))]
        [EditingComponentProperty(nameof(TextInputProperties.DefaultValue), "3000")]
        [EditingComponentProperty(nameof(TextInputProperties.Size), 10)]
        [EditingComponentProperty(nameof(TextInputProperties.Required), true)]
        [VisibilityCondition(nameof(Autoplay), ComparisonTypeEnum.IsEqualTo, true, StringComparison = StringComparison.OrdinalIgnoreCase)]
        public string? SlideDuration { get; set; } = "3000";
    }
}
