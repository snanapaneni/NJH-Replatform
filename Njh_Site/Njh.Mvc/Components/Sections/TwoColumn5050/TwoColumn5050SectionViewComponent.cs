namespace Njh.Mvc.Components.Sections.TwoColumn5050
{
    using Kentico.PageBuilder.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using Njh.Kernel.Services;
    using Njh.Mvc.Models.SectionsViewModels;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;
    public class TwoColumn5050SectionViewComponent : SafeViewComponent<TwoColumn5050SectionViewComponent>
    {
        /// <summary>
        /// The section identifier.
        /// </summary>
        public const string Identifier =
            "Njh.TwoColumn5050Section";


        private readonly ISectionThemeService sectionThemeService;

        public TwoColumn5050SectionViewComponent(
                     ISectionThemeService sectionThemeService,
            ILogger<TwoColumn5050SectionViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {

            this.sectionThemeService = sectionThemeService ??
                throw new ArgumentNullException(nameof(sectionThemeService));
        }

        public IViewComponentResult Invoke(ComponentViewModel<TwoColumn5050SectionProperties> sectionProperties)
        {
            return
                      this.TryInvoke((vc) =>
                      {
                          var secProps = sectionProperties?.Properties;
                          var model = new TwoColumn5050SectionViewModel();

                          if (Guid.TryParse(secProps?.ThemeGuid, out Guid themeGuid))
                          {
                              var themeItem = this.sectionThemeService.GetThemesItemByGuid(themeGuid);

                              model.HasMargin = secProps.HasMargin;
                              model.HasPadding = secProps.HasPadding;
                              model.CssClass = themeItem?.CssClass ?? string.Empty;
                              model.BackgroundColor = themeItem?.BackgroundColor ?? string.Empty;
                              model.Color = themeItem?.TextColor ?? string.Empty;
                          }

                          return vc.View("~/Views/Shared/Sections/_TwoColumn5050Section.cshtml", model);
                      });
        }
    }
}
