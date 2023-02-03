namespace Njh.Kernel.Kentico.Models.PageTypes
{
    using CMS.DocumentEngine;
    using System.Collections.Generic;

    public interface IPageType_BasePageType
    {
        int NodeID { get; }

        string PageName { get; set; }

        string Title { get; set; }

        string PageBlurb { get; set; }

        string Summary { get; set; }

        string PageImage { get; set; }

        string PageImageAltText { get; set; }

        public bool Hide_URL { get; set; }

    }
}
