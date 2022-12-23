using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Models;
using Njh.Kernel.Services;
using Njh.Mvc.Models.SectionsViewModels;
using Njh.Kernel.Services;
namespace Njh.Mvc.Components.Sections.TwoColumn6733
{
    public class TwoColumn6733SectionViewComponent : ViewComponent
    {
        /// <summary>
        /// The section identifier.
        /// </summary>
        public const string Identifier =
            "Njh.TwoColumn6733Section";

        private readonly ICacheService cacheService;
        private readonly ContextConfig context;
        private readonly ISectionThemeService sectionThemeService;

        public TwoColumn6733SectionViewComponent(
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
        
        public IViewComponentResult Invoke(ComponentViewModel<TwoColumn6733SectionProperties> sectionProperties)
        {
            var prop = sectionProperties?.Properties;
            var model = new TwoColumn6733SectionViewModel();

            if (Guid.TryParse(prop.ThemeGuid, out var themeGuid))
            {

                var themeItem = sectionThemeService.GetThemesItemByGuid(themeGuid);
                // Cache the item coming from the table

                model.HasPadding = prop.HasPadding;
                model.CssClass = themeItem?.CssClass;
                model.BackgroundColor = themeItem?.BackgroundColor;
                model.Color = themeItem?.TextColor;
            }
            return View("~/Views/Shared/Sections/_TwoColumn6733Section.cshtml", model);
        }
    }
}
