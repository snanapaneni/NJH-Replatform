exports.templates = [
  {
    templateFilename: "index",
    header: "app-header",
    footer: "app-footer",
    main_before: [""],
    main_after: [""],
    main_insert: "article",
    main_components: ["accordion"],
    main_components: ["image-slider"],
  },
  {
    templateFilename: "home",
    header: "app-header",
    footer: "app-footer",
    main_before: [""],
    main_after: [""],
    main_insert: "article",
    main_components: ["home-hero", "alert-banner", "widget-zone", "accordion"],
  },
  {
    templateFilename: "elements",
    header: "app-header",
    footer: "app-footer",
    main_before: [""],
    main_insert: "main",
    main_components: [
      ,
      "elements--typography",
      "elements--table",
      "elements--buttons",
      "elements--backgrounds",
      "elements--colors",
    ],
    main_after: [""],
  },
  {
    templateFilename: "subpage__left-rail",
    header: "app-header",
    footer: "app-footer",
    main_before: ["breadcrumbs", "alert-banner"],
    main_insert: "article",
    main_components: ["subpage-header", "subpage-aside", "subpage-content"],
    main_after: [""],
  },
  {
    templateFilename: "subpage__no-rail",
    header: "app-header",
    footer: "app-footer",
    main_before: ["breadcrumbs", "alert-banner"],
    main_insert: "article",
    main_components: ["subpage-header", "subpage-content"],
    main_after: [""],
  },
  {
    templateFilename: "hub",
    header: "app-header",
    footer: "app-footer",
    main_before: [""],
    main_insert: "component-partial",
    main_components: ["tabs", "additional-links"],
    main_after: [""],
  },
  {
    templateFilename: "hub",
    header: "app-header",
    footer: "app-footer",
    main_before: [""],
    main_insert: "",
    main_components: [""],
    main_after: [""],
  },
  {
    templateFilename: "widget-zones",
    header: "app-header",
    footer: "app-footer",
    main_before: [""],
    main_insert: "article",
    main_components: ["subpage-header", "widget-zone"],
    main_after: [""],
  },

  {
    templateFilename: "search",
    header: "app-header",
    footer: "app-footer",
    main_before: [""],
    main_insert: "article",
    main_components: [],
    main_after: [""],
  },

  {
    templateFilename: "physician",
    header: "app-header",
    footer: "app-footer",
    main_before: [""],
    main_after: [""],
    main_insert: "article",
    main_components: ["subpage-header", "subpage-aside", "physician-info-box"],
  },
  {
    templateFilename: "press-release",
    header: "app-header",
    footer: "app-footer",
    main_before: [""],
    main_after: [""],
    main_insert: "article",
    main_components: ["subpage-aside", "press-release-content"],
  },
  {
    templateFilename: "specialty-main",
    header: "app-header",
    footer: "app-footer",
    main_before: [""],
    main_insert: "article",
    main_components: ["subpage-header", "subpage-aside", "speciality-content"],
    main_after: [""],
  },
];
