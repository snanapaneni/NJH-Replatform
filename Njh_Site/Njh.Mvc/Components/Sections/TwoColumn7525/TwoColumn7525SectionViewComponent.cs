

namespace Njh.Mvc.Components.Sections.TwoColumn7525
{
    using Kentico.PageBuilder.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using Njh.Kernel.Services;
    using Njh.Mvc.Models.SectionsViewModels;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;
    public class TwoColumn7525SectionViewComponent : SafeViewComponent<TwoColumn7525SectionViewComponent>
    {
        /// <summary>
        /// The section identifier.
        /// </summary>
        public const string Identifier =
            "Njh.TwoColumn7525Section";


        private readonly ISectionThemeService sectionThemeService;

        public TwoColumn7525SectionViewComponent(
                     ISectionThemeService sectionThemeService,
            ILogger<TwoColumn7525SectionViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {

            this.sectionThemeService = sectionThemeService ??
                throw new ArgumentNullException(nameof(sectionThemeService));
        }

        public IViewComponentResult Invoke(ComponentViewModel<TwoColumn7525SectionProperties> sectionProperties)
        {
            return
                      this.TryInvoke((vc) =>
                      {
                          int FullWidthColumnSize = 12;
                          var secProps = sectionProperties?.Properties;
                          var model = new TwoColumn7525SectionViewModel();

                          if (Guid.TryParse(secProps?.ThemeGuid, out Guid themeGuid))
                          {
                              var themeItem = this.sectionThemeService.GetThemesItemByGuid(themeGuid);

                              model.HasMargin = secProps.HasMargin;
                              model.HasPadding = secProps.HasPadding;
                              model.CssClass = themeItem?.CssClass ?? string.Empty;
                              model.BackgroundColor = themeItem?.BackgroundColor ?? string.Empty;
                              model.Color = themeItem?.TextColor ?? string.Empty;
                              model.FirstColumnSize = (!secProps?.SwapColumns) ?? false ? 9 : 3;
                              model.SecondColumnSize = FullWidthColumnSize - model.FirstColumnSize;
                          }

                          return vc.View("~/Views/Shared/Sections/_TwoColumn7525Section.cshtml", model);
                      });
        }
    }
}
