namespace Njh.Mvc.Models.SectionsViewModels
{
    public class BaseSectionViewModel
    {
        public string ThemeName { get; set; } = string.Empty;
        public string CssClass { get; set; } = string.Empty;
        public bool HasPadding { get; set; } = true;

        public bool UseCssClass
        {
            get { return !string.IsNullOrWhiteSpace(this.CssClass); }

        }

        public string ThemeGuid { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// Returns the Padding CssClass.
        /// </summary>
        /// <returns>
        /// The padding CSS class.
        /// </returns>
        public string GetPaddingCssClass()
        {
            return
                HasPadding
                ? "has-padding"
                : string.Empty;
        }

        /// <summary>
        /// Returns the Theme CssClass.
        /// </summary>
        /// <returns>
        /// The theme CSS class.
        /// </returns>
        public string GetThemeCssClass()
        {
            return
                UseCssClass
                ? this.CssClass
                : "bg-neutral--100";
        }

        /// <summary>
        /// Returns the Background color when no CssClass is set.
        /// </summary>
        /// <returns>
        /// The background css variable along with the value (css syntax).
        /// </returns>
        public string GetBackgroundColor()
        {
            return
                UseCssClass || string.IsNullOrWhiteSpace(this.BackgroundColor)
                ? string.Empty
                : $"--has-background:{this.BackgroundColor};";
        }

        /// <summary>
        /// Returns the text color when no CssClass is set.
        /// </summary>
        /// <returns>
        /// The color css variable along with the value (css syntax).
        /// </returns>
        public string GetTextColor()
        {
            return
                UseCssClass || string.IsNullOrWhiteSpace(this.Color)
                ? string.Empty
                : $"--has-color:{this.Color};";
        }
        
    }
}
