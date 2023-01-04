

namespace Njh.Mvc.Components.Sections.ThreeColumn
{
    using Kentico.PageBuilder.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using Njh.Kernel.Services;
    using Njh.Mvc.Models.SectionsViewModels;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;
    public class ThreeColumnSectionViewComponent : SafeViewComponent<ThreeColumnSectionViewComponent>
    {
        /// <summary>
        /// The section identifier.
        /// </summary>
        public const string Identifier =
            "Njh.ThreeColumnSection";


        private readonly ISectionThemeService sectionThemeService;

        public ThreeColumnSectionViewComponent(
                     ISectionThemeService sectionThemeService,
            ILogger<ThreeColumnSectionViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {

            this.sectionThemeService = sectionThemeService ??
                throw new ArgumentNullException(nameof(sectionThemeService));
        }

        public IViewComponentResult Invoke(ComponentViewModel<ThreeColumnSectionProperties> sectionProperties)
        {
            return
                      this.TryInvoke((vc) =>
                      {
                          var secProps = sectionProperties?.Properties;
                          var model = new ThreeColumnSectionViewModel();

                          if (Guid.TryParse(secProps?.ThemeGuid, out Guid themeGuid))
                          {
                              var themeItem = this.sectionThemeService.GetThemesItemByGuid(themeGuid);

                              model.HasPadding = secProps.HasPadding;
                              model.CssClass = themeItem?.CssClass ?? string.Empty;
                              model.BackgroundColor = themeItem?.BackgroundColor ?? string.Empty;
                              model.Color = themeItem?.TextColor ?? string.Empty;
                          }

                          return vc.View("~/Views/Shared/Sections/_ThreeColumnSection.cshtml", model);
                      });
        }
    }
}
