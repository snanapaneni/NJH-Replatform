// Copyright (c) NJH. All rights reserved.

// TODO: Register the Kentico page templates (with the RegisterPageTemplate attribute) here

[assembly: Kentico.PageBuilder.Web.Mvc.PageTemplates.RegisterPageTemplate(
    "NJH.SubpageLeftNavTemplate",
    "Subpage (Left Nav) Template",
    typeof(Njh.Mvc.Models.PageTemplateProperties.SubpageLeftNavTemplateProperties),
    "~/Views/Shared/PageTemplates/SubpageLeftNavTemplate.cshtml")]