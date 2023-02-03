using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Njh.Mvc.Components.Sections.OneColumnFullWidth;
using Njh.Mvc.Models.SectionsViewModels;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.Sections.Tabs
{
    /// <summary>
    /// Tab Section.
    /// </summary>
    public class TabSectionViewComponent : SafeViewComponent<TabSectionViewComponent>
    {
        /// <summary>
        /// The section identifier.
        /// </summary>
        public const string Identifier = "Njh.TabSection";


        public TabSectionViewComponent(ILogger<TabSectionViewComponent> logger, IViewComponentErrorVisibility viewComponentErrorVisibility) : base(logger, viewComponentErrorVisibility)
        {
        }

        /// <summary>
        /// Render view.
        /// </summary>
        /// <param name="sectionProperties">Tab Section Properties.</param>
        /// <returns>Returns view with Tabs.</returns>
        public IViewComponentResult Invoke(ComponentViewModel<TabSectionProperties> sectionProperties)
        {
            return
                this.TryInvoke((vc) =>
                {
                    var secProps = sectionProperties?.Properties;
                    var model = new TabSectionViewModel
                    {
                        Tabs = secProps?.ListOfTabs?.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                            .ToList() ?? new List<string>(),
                    };

                    return vc.View("~/Views/Shared/Sections/_TabSection.cshtml", model);
                });
        }
    }
}
