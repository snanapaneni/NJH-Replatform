using Kentico.PageBuilder.Web.Mvc;
using Njh.Mvc.Components;
using Njh.Mvc.Components.AdditionalLinks;
using Njh.Mvc.Components.Accordion;
using Njh.Mvc.Components.Banner;
using Njh.Mvc.Components.Image;
using Njh.Mvc.Components.Sections.FourColumn;
using Njh.Mvc.Components.Sections.OneColumn;
using Njh.Mvc.Components.Sections.OneColumnFullWidth;
using Njh.Mvc.Components.Sections.Tabs;
using Njh.Mvc.Components.Sections.ThreeColumn;
using Njh.Mvc.Components.Sections.TwoColumn5050;
using Njh.Mvc.Components.Sections.TwoColumn6733;
using Njh.Mvc.Components.Sections.TwoColumn7525;
using Njh.Mvc.Models.Constants;

/*
 * Sections
 */
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

[assembly: RegisterSection(
    TabSectionViewComponent.Identifier,
    typeof(TabSectionViewComponent),
    "Tab Section",
    typeof(TabSectionProperties),
    IconClass = IconConstants.Tab)]

/*
 * Widgets
 */

[assembly: RegisterWidget(
    BlurbContentViewComponent.Identifier,
    typeof(BlurbContentViewComponent),
    "NJH Blurb Content Widget",
    typeof(BlurbContentProperties),
    AllowCache = true,
    IconClass = IconConstants.HeaderText)]

[assembly: RegisterWidget(
    ImageViewComponent.Identifier,
    typeof(ImageViewComponent),
    "NJH Image Widget",
    typeof(ImageComponentProperties),
    AllowCache = true,
    IconClass = IconConstants.Picture)]

[assembly: RegisterWidget(
    ImageSliderViewComponent.Identifier,
    typeof(ImageSliderViewComponent),
    "NJH Image Slider Widget",
    typeof(ImageSliderComponentProperties),
    AllowCache = true,
    IconClass = IconConstants.Slides)]

[assembly: RegisterWidget(
    AccordionComponent.Identifier,
    typeof(AccordionComponent),
    "NJH Accordion Widget",
    typeof(AccordionComponentProperties),
    AllowCache = true,
    IconClass = IconConstants.Accordion)]

[assembly: RegisterWidget(
    HubHeroBannerViewComponent.Identifier,
    typeof(HubHeroBannerViewComponent),
    "NJH Hub Hero Banner Widget",
    typeof(HubHeroBannerViewComponentProperties),
    AllowCache = true,
    IconClass = IconConstants.ImageBanner)]

[assembly: RegisterWidget(
    AdditionalLinksComponent.Identifier,
    typeof(AdditionalLinksComponent),
    "NJH Additional Links Widget",
    typeof(AdditionalLinksComponentProperties),
    AllowCache = true,
    IconClass = IconConstants.Picture)]
