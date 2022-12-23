using Kentico.PageBuilder.Web.Mvc;
using Njh.Mvc.Components.Sections.FourColumn;
using Njh.Mvc.Components.Sections.OneColumn;
using Njh.Mvc.Components.Sections.OneColumnFullWidth;
using Njh.Mvc.Components.Sections.ThreeColumn;
using Njh.Mvc.Components.Sections.TwoColumn5050;
using Njh.Mvc.Components.Sections.TwoColumn6733;
using Njh.Mvc.Components.Sections.TwoColumn7525;

[assembly: RegisterSection(
    OneColumnSectionViewComponent.Identifier,
    typeof(OneColumnSectionViewComponent),
    "One Column Section",
    typeof(OneColumnSectionProperties))]

[assembly: RegisterSection(
    OneColumnFullWidthSectionViewComponent.Identifier,
    typeof(OneColumnFullWidthSectionViewComponent),
    "One Column Full Width Section",
    typeof(OneColumnFullWidthSectionProperties))]

[assembly: RegisterSection(
    TwoColumn5050SectionViewComponent.Identifier,
    typeof(TwoColumn5050SectionViewComponent),
    "Two Column 50/50 Section",
    typeof(TwoColumn5050SectionProperties))]

[assembly: RegisterSection(
    TwoColumn7525SectionViewComponent.Identifier,
    typeof(TwoColumn7525SectionViewComponent),
    "Two Column 75/25 Section",
    typeof(TwoColumn7525SectionProperties))]

[assembly: RegisterSection(
    TwoColumn6733SectionViewComponent.Identifier,
    typeof(TwoColumn6733SectionViewComponent),
    "Two Column 67/33 Section",
    typeof(TwoColumn6733SectionProperties))]


[assembly: RegisterSection(
    ThreeColumnSectionViewComponent.Identifier,
    typeof(ThreeColumnSectionViewComponent),
    "Three Column Section",
    typeof(ThreeColumnSectionProperties))]

[assembly: RegisterSection(
    FourColumnSectionViewComponent.Identifier,
    typeof(FourColumnSectionViewComponent),
    "Four Column Section",
    typeof(FourColumnSectionProperties))]
