using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Services;
using SimpleMvcSitemap;

namespace Njh.Mvc.Controllers
{
    /// <summary>
    /// Implements the controller that handles the sitemap.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class XmlSitemapController
        : Controller
    {
        private readonly ISitemapService sitemapService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="XmlSitemapController"/> class.
        /// </summary>
        /// <param name="sitemapService">
        /// The sitemap service.
        /// </param>
        public XmlSitemapController(
            ISitemapService sitemapService)
        {
            this.sitemapService = sitemapService ??
                                  throw new ArgumentNullException(nameof(sitemapService));
        }

        /// <summary>
        /// Returns the sitemap.
        /// </summary>
        /// <returns>
        /// The sitemap.
        /// </returns>
        [Route("googlesitemap.xml")]
        public IActionResult Index()
        {
            var model =
                this.sitemapService.GetXmlSitemapPages();

            var sitemapProvider = new SitemapProvider();

            return sitemapProvider.CreateSitemap(model);
        }
    }
}
