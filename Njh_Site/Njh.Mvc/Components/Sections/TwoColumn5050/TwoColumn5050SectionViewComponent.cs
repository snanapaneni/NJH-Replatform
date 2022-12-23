using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Models;
using Njh.Kernel.Services;
using Njh.Mvc.Models.SectionsViewModels;
using Njh.Kernel.Services;
namespace Njh.Mvc.Components.Sections.TwoColumn5050
{
    public class TwoColumn5050SectionViewComponent : ViewComponent
    {
        /// <summary>
        /// The section identifier.
        /// </summary>
        public const string Identifier =
            "Njh.TwoColumn5050Section";

        private readonly ICacheService cacheService;
        private readonly ContextConfig context;
        private readonly ISectionThemeService sectionThemeService;

        public TwoColumn5050SectionViewComponent(
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
        
        public IViewComponentResult Invoke(ComponentViewModel<TwoColumn5050SectionProperties> sectionProperties)
        {
            var prop = sectionProperties?.Properties;
            var model = new TwoColumn5050SectionViewModel();

            if (Guid.TryParse(prop.ThemeGuid, out var themeGuid))
            {

                var themeItem = sectionThemeService.GetThemesItemByGuid(themeGuid);
                // Cache the item coming from the table

                model.HasPadding = prop.HasPadding;
                model.CssClass = themeItem?.CssClass;
                model.BackgroundColor = themeItem?.BackgroundColor;
                model.Color = themeItem?.TextColor;
            }
            return View("~/Views/Shared/Sections/_TwoColumn5050Section.cshtml", model);
        }
    }
}
