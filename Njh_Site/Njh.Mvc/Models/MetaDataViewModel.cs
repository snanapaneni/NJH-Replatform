// Copyright (c) Toronto Region Board of Trade. All rights reserved.

namespace Njh.Mvc.Models
{
#nullable enable
    using Njh.Mvc.Components.MetaData;

    /// <summary>
    /// Implements the view model for the
    /// <see cref="MetaDataViewComponent"/> widget.
    /// </summary>
    public class MetaDataViewModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; } =
            string.Empty;
        
        /// <summary>
        /// Gets or sets the PageUrl.
        /// </summary>
        public string PageUrl { get; set; } =
            string.Empty;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; } =
            string.Empty;

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        public string Keywords { get; set; } =
            string.Empty;

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        public string ImageUrl { get; set; } =
            string.Empty;

        /// <summary>
        /// Gets or sets the image alt text.
        /// </summary>
        public string ImageAlt { get; set; } =
            string.Empty;

        /// <summary>
        /// Gets or sets the Twitter Card image URL.
        /// </summary>
        public string TwitterCardImageUrl { get; set; } =
            string.Empty;

        /// <summary>
        /// Gets or sets the Og Type.
        /// </summary>
        public string OgType { get; set; } =
            string.Empty;

        /// <summary>
        /// Gets or sets the Og Site Name.
        /// </summary>
        public string OgSiteName { get; set; } =
            string.Empty;

        /// <summary>
        /// Gets or sets the Twitter handle.
        /// </summary>
        public string TwitterHandle { get; set; } =
            string.Empty;

        /// <summary>
        /// Gets or sets the Twitter Card.
        /// </summary>
        public string TwitterCard { get; set; } =
            string.Empty;

        
    }
#nullable restore
}