using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Models;
using Njh.Kernel.Services;
using Njh.Mvc.Models.SectionsViewModels;
using Njh.Kernel.Services;
namespace Njh.Mvc.Components.Sections.OneColumn
{
    public class OneColumnSectionViewComponent : ViewComponent
    {
        /// <summary>
        /// The section identifier.
        /// </summary>
        public const string Identifier =
            "Njh.OneColumnSection";

        private readonly ICacheService cacheService;
        private readonly ContextConfig context;
        private readonly ISectionThemeService sectionThemeService;

        public OneColumnSectionViewComponent(
            ICacheService cacheService,
            ContextConfig context,
            ISectionThemeService sectionThemeService)
        {
            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));

            this.context = context ??
                throw new ArgumentNullException(nameof(context));

            this.sectionThemeService = sectionThemeService ??
                throw new ArgumentNullException(nameof(context));
        }
        
        public IViewComponentResult Invoke(ComponentViewModel<OneColumnSectionProperties> sectionProperties)
        {
            var prop = sectionProperties?.Properties;
            var model = new OneColumnSectionViewModel();

            if (Guid.TryParse(prop.ThemeGuid, out var themeGuid))
            {

                var themeItem = sectionThemeService.GetThemesItemByGuid(themeGuid);
                // Cache the item coming from the table

                model.HasPadding = prop.HasPadding;
                model.CssClass = themeItem?.CssClass;
                model.BackgroundColor = themeItem?.BackgroundColor;
                model.Color = themeItem?.TextColor;
            }
            return View("~/Views/Shared/Sections/_OneColumnSection.cshtml", model);
        }
    }
}
