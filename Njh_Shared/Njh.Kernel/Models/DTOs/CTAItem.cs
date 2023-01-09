namespace Njh.Kernel.Models.DTOs
{
    using System;

    public class CTAItem
    {
        public string Title { get; set; }

        public string DisplayTitle { get; set; }

        public string Link { get; set; }

        public Guid LinkPage { get; set; }

        public string ShortDescription { get; set; }

        public string Image { get; set; }

        public string ImageAltText { get; set; }
    }
}