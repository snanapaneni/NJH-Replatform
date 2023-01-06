﻿using Kentico.PageBuilder.Web.Mvc.PageTemplates;
using Njh.Mvc.Models.PageTemplateProperties;

[assembly: RegisterPageTemplate("NJH.LandingPageTemplate",
                               "Landing page template",
                               typeof(LandingPageProperties),
                               customViewName: "~/PageTemplates/LandingPage/_LandingPageTemplate.cshtml",
                               IconClass = "icon-l-rows-2")]

[assembly: RegisterPageTemplate("NJH.SubPageTemplate",
                               "Sub page (No Nav) template",
                               typeof(SubPageTemplateProperties),
                               customViewName: "~/Views/Shared/PageTemplates/SubPageTemplate.cshtml",
                               IconClass = "icon-l-rows-2")]