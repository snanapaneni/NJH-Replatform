using Kentico.PageBuilder.Web.Mvc;
using Njh.Mvc.Components;
using Njh.Mvc.Components.Sections.FourColumn;
using Njh.Mvc.Components.Sections.OneColumn;
using Njh.Mvc.Components.Sections.OneColumnFullWidth;
using Njh.Mvc.Components.Sections.ThreeColumn;
using Njh.Mvc.Components.Sections.TwoColumn5050;
using Njh.Mvc.Components.Sections.TwoColumn6733;
using Njh.Mvc.Components.Sections.TwoColumn7525;
using Njh.Mvc.Models.Constants;

[assembly: RegisterSection(
    OneColumnSectionViewComponent.Identifier,
    typeof(OneColumnSectionViewComponent),
    "One Column Section",
    typeof(OneColumnSectionProperties),
    IconClass = IconConstants.OneColumn)]

[assembly: RegisterSection(
    OneColumnFullWidthSectionViewComponent.Identifier,
    typeof(OneColumnFullWidthSectionViewComponent),
    "One Column Full Width Section",
    typeof(OneColumnFullWidthSectionProperties),
    IconClass = IconConstants.OneColumn)]

[assembly: RegisterSection(
    TwoColumn5050SectionViewComponent.Identifier,
    typeof(TwoColumn5050SectionViewComponent),
    "Two Column 50/50 Section",
    typeof(TwoColumn5050SectionProperties),
    IconClass = IconConstants.TwoColumn)]

[assembly: RegisterSection(
    TwoColumn7525SectionViewComponent.Identifier,
    typeof(TwoColumn7525SectionViewComponent),
    "Two Column 75/25 Section",
    typeof(TwoColumn7525SectionProperties),
    IconClass = IconConstants.TwoColumn7525)]

[assembly: RegisterSection(
    TwoColumn6733SectionViewComponent.Identifier,
    typeof(TwoColumn6733SectionViewComponent),
    "Two Column 67/33 Section",
    typeof(TwoColumn6733SectionProperties),
    IconClass = IconConstants.TwoColumn6733)]

[assembly: RegisterSection(
    ThreeColumnSectionViewComponent.Identifier,
    typeof(ThreeColumnSectionViewComponent),
    "Three Column Section",
    typeof(ThreeColumnSectionProperties),
    IconClass = IconConstants.ThreeColumn)]

[assembly: RegisterSection(
    FourColumnSectionViewComponent.Identifier,
    typeof(FourColumnSectionViewComponent),
    "Four Column Section",
    typeof(FourColumnSectionProperties),
    IconClass = IconConstants.FourColumn)]

[assembly: RegisterWidget(
    BlurbContentViewComponent.Identifier,
    typeof(BlurbContentViewComponent),
    "NJH Blurb Content Widget",
    typeof(BlurbContentProperties),
    AllowCache = true,
    IconClass = IconConstants.OneColumn)]
