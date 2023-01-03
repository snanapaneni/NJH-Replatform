

namespace Njh.Mvc.Components.Sections.FourColumn
{
    using Kentico.PageBuilder.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using Njh.Kernel.Models;
    using Njh.Kernel.Services;
    using Njh.Mvc.Models.SectionsViewModels;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;

    public class FourColumnSectionViewComponent : SafeViewComponent<FourColumnSectionViewComponent>
    {
        /// <summary>
        /// The section identifier.
        /// </summary>
        public const string Identifier =
            "Njh.FourColumnSection";

        private readonly ISectionThemeService sectionThemeService;

        public FourColumnSectionViewComponent(
            ISectionThemeService sectionThemeService,
            ILogger<FourColumnSectionViewComponent> logger,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {

            this.sectionThemeService = sectionThemeService ??
                throw new ArgumentNullException(nameof(sectionThemeService));
        }

        public IViewComponentResult Invoke(ComponentViewModel<FourColumnSectionProperties> sectionProperties)
        {
            return
           this.TryInvoke((vc) =>
           {
               var secProps = sectionProperties?.Properties;
               var model = new FourColumnSectionViewModel();

               if (Guid.TryParse(secProps?.ThemeGuid, out Guid themeGuid))
               {
                   var themeItem = this.sectionThemeService.GetThemesItemByGuid(themeGuid);

                   model.HasPadding = secProps.HasPadding;
                   model.CssClass = themeItem?.CssClass ?? string.Empty;
                   model.BackgroundColor = themeItem?.BackgroundColor ?? string.Empty;
                   model.Color = themeItem?.TextColor ?? string.Empty;
               }

               return vc.View("~/Views/Shared/Sections/_FourColumnSection.cshtml", model);
           });
        }
    }
}
