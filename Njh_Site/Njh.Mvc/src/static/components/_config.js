exports.templates = [
  {
    templateFilename: "index",
    header: "app-header",
    footer: "app-footer",
    main_insert: "main",
    main_components: [],
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
  },
  {
    templateFilename: "subpage__left-rail",
    header: "app-header",
    footer: "app-footer",
    main_insert: "article",
    main_components: ["subpage-header", "subpage-content", "subpage-aside"],
  },
  {
    templateFilename: "subpage__no-rail",
    header: "app-header",
    footer: "app-footer",
    main_insert: "article",
    main_components: ["subpage-header", "subpage-content"],
  },
];
