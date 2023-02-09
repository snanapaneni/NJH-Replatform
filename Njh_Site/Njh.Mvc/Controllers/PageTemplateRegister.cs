using Kentico.PageBuilder.Web.Mvc.PageTemplates;
using Njh.Mvc.Models.PageTemplateProperties;

[assembly: RegisterPageTemplate(
                               "NJH.LandingPageTemplate",
                               "Landing page template",
                               typeof(LandingPageProperties),
                               customViewName: "~/PageTemplates/LandingPage/_LandingPageTemplate.cshtml",
                               IconClass = "icon-l-rows-2")]

[assembly: RegisterPageTemplate(
                               "NJH.SubPageTemplate",
                               "Sub page (No Nav) template",
                               typeof(SubPageTemplateProperties),
                               customViewName: "~/Views/Shared/PageTemplates/SubPageTemplate.cshtml",
                               IconClass = "icon-l-rows-2")]

[assembly: RegisterPageTemplate(
                               "NJH.SubPageLeftNavTemplate",
                               "Sub page (Left Nav) template",
                               typeof(SubPageLeftNavTemplateProperties),
                               customViewName: "~/Views/Shared/PageTemplates/SubPageLeftNavTemplate.cshtml",
                               IconClass = "icon-l-rows-2")]

[assembly: RegisterPageTemplate("NJH.PressReleaseTemplate",
                                "Press Release Page template",
                                typeof(PressReleaseTemplateProperties),
                                customViewName: "~/Views/Shared/PageTemplates/PressReleasePageTemplate.cshtml",
                                IconClass = "icon-newspaper")]
[assembly: RegisterPageTemplate(
                               "NJH.HubPageTemplate",
                               "Hub Page template",
                               typeof(HubPageTemplateProperties),
                               customViewName: "~/Views/Shared/PageTemplates/HubPageTemplate.cshtml",
                               IconClass = "icon-l-rows-2")]
