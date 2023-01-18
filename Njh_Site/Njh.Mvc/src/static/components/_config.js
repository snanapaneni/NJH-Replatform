exports.templates = [
  {
    templateFilename: "index",
    header: "app-header",
    footer: "app-footer",
    main_insert: "main",
    main_components: [],
    options: {
      h1: "Index",
    },
  },
  {
    templateFilename: "elements",
    header: "app-header",
    footer: "app-footer",
    main_insert: "main",
    main_components: [
      "elements--typography",
      "elements--table",
      "elements--buttons",
      "elements--backgrounds",
      "elements--colors",
    ],
    options: {
      h1: "Elements",
    },
  },
  {
    templateFilename: "subpage__left-rail",
    header: "app-header",
    footer: "app-footer",
    main_insert: "article",
    main_components: ["subpage-header", "subpage-aside", "subpage-content"],
    options: {
      h1: "Subpage Left Rail",
    },
  },
  {
    templateFilename: "subpage__no-rail",
    header: "app-header",
    footer: "app-footer",
    main_insert: "article",
    main_components: ["subpage-header", "subpage-content"],
    options: {
      h1: "Subpage No Rail",
    },
  },
  {
    templateFilename: "widget-zones",
    header: "app-header",
    footer: "app-footer",
    main_insert: "article",
    main_components: ["subpage-header", "widget-zone"],
    options: {
      h1: "Widget Zone Examples",
    },
  },

  {
    templateFilename: "search",
    header: "app-header",
    footer: "app-footer",
    main_insert: "article",
    main_components: [],
  },
   {
    templateFilename: "condition",
    header: "app-header",
    footer: "app-footer",
    main_insert: "article",
    main_components: ["subpage-header", "subpage-aside", "condition-content"],
    options: {
      h1: "Subpage Left Rail",
    },
  },
];
