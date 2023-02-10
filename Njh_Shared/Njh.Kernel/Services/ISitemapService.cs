using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Services
{
    using SimpleMvcSitemap;

    /// <summary>
    /// Defines a xml sitemap service.
    /// </summary>
    public interface ISitemapService
    {
        /// <summary>
        /// Returns the sitemap.
        /// </summary>
        /// <returns>The sitemap.</returns>
        SitemapModel GetXmlSitemapPages();
    }
}
