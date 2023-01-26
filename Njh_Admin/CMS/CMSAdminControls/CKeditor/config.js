/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
  config.allowedContent = true; // To disable CKEditor ACF
  config.enterMode = CKEDITOR.ENTER_P;
  config.shiftEnterMode = CKEDITOR.ENTER_BR;
  config.entities_latin = false;

  var sourceName = config.useInlineMode ? "Sourcedialog" : "Source";

  config.toolbar_Standard = config.toolbar_Default = [
    [sourceName, "-"],
    ["Undo", "Redo", "-"],
    ["Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-"],
    ["Styles"],
    ["Bold", "Italic", "FontSize", "-"],
    ["NumberedList", "BulletedList", "Outdent", "Indent", "Blockquote", "-"],
    ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock", "-"],
    ["InsertLink", "Unlink", "Anchor", "-"],
    [
      "InsertImageOrMedia",
      "QuicklyInsertImage",
      "Table",
      "HorizontalRule",
      "InsertMacro",
      "-",
    ],
    ["Maximize"],
  ];

  config.toolbar_Full = [
    [sourceName, "-"],
    ["Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "Scayt", "-"],
    ["Undo", "Redo", "Find", "Replace", "RemoveFormat", "-"],
    ["Bold", "Italic", "Underline", "Strike", "Subscript", "Superscript", "-"],
    [
      "NumberedList",
      "BulletedList",
      "Outdent",
      "Indent",
      "Blockquote",
      "CreateDiv",
      "-",
    ],
    ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock", "-"],
    "/",
    ["InsertLink", "Unlink", "Anchor", "-"],
    [
      "InsertImageOrMedia",
      "QuicklyInsertImage",
      "Table",
      "HorizontalRule",
      "SpecialChar",
      "-",
    ],
    ["Styles", "Format", "Font", "FontSize"],
    ["TextColor", "BGColor", "-"],
    ["InsertMacro", "-"],
    ["Maximize", "ShowBlocks"],
  ];

  config.toolbar_Basic = [
    [
      "Bold",
      "Italic",
      "-",
      "NumberedList",
      "BulletedList",
      "-",
      "InsertLink",
      "Unlink",
    ],
  ];

  config.toolbar_BizForm = [
    ["Source", "-"],
    ["Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-"],
    ["Undo", "Redo", "Find", "Replace", "RemoveFormat", "-"],
    ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock", "-"],
    ["Bold", "Italic", "Underline", "Strike", "Subscript", "Superscript", "-"],
    ["NumberedList", "BulletedList", "Outdent", "Indent", "-"],
    ["InsertLink", "Unlink", "Anchor", "-"],
    ["InsertImageOrMedia", "Table", "HorizontalRule", "SpecialChar", "-"],
    ["Styles", "Format", "Font", "FontSize"],
    ["TextColor", "BGColor", "-"],
    ["InsertMacro", "-"],
    ["Maximize"],
  ];

  config.toolbar_Forum = [
    [
      "Bold",
      "Italic",
      "-",
      "InsertLink",
      "InsertUrl",
      "InsertImageOrMedia",
      "InsertImage",
      "InsertQuote",
      "-",
      "NumberedList",
      "BulletedList",
      "-",
      "TextColor",
      "BGColor",
    ],
  ];

  config.toolbar_Reporting = [
    ["Source", "-"],
    ["Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-"],
    ["Undo", "Redo", "Find", "Replace", "RemoveFormat", "-"],
    ["Bold", "Italic", "Underline", "Strike", "Subscript", "Superscript", "-"],
    ["NumberedList", "BulletedList", "Outdent", "Indent", "-"],
    ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock", "-"],
    ["InsertLink", "Unlink", "Anchor", "-"],
    [
      "InsertImageOrMedia",
      "QuicklyInsertImage",
      "Table",
      "HorizontalRule",
      "SpecialChar",
      "-",
    ],
    ["Styles", "Format", "Font", "FontSize"],
    ["TextColor", "BGColor", "-"],
    ["InsertMacro", "-"],
    ["Maximize"],
  ];

  config.toolbar_SimpleEdit = [
    ["Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-"],
    ["Undo", "Redo", "Find", "Replace", "RemoveFormat", "-"],
    ["Bold", "Italic", "Underline", "Strike", "Subscript", "Superscript", "-"],
    ["NumberedList", "BulletedList", "Outdent", "Indent", "-"],
    ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock", "-"],
    ["InsertLink", "Unlink", "Anchor", "-"],
    [
      "InsertImageOrMedia",
      "QuicklyInsertImage",
      "Table",
      "HorizontalRule",
      "SpecialChar",
      "-",
    ],
    ["Styles", "Format", "Font", "FontSize"],
    ["TextColor", "BGColor", "-"],
    ["Maximize"],
  ];

  config.toolbar_Invoice = [
    ["Source", "-"],
    ["Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "Scayt", "-"],
    ["Undo", "Redo", "Find", "Replace", "RemoveFormat", "-"],
    ["Bold", "Italic", "Underline", "Strike", "Subscript", "Superscript", "-"],
    [
      "NumberedList",
      "BulletedList",
      "Outdent",
      "Indent",
      "Blockquote",
      "CreateDiv",
      "-",
    ],
    ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock", "-"],
    ["InsertImageOrMedia", "Table", "HorizontalRule", "SpecialChar", "-"],
    ["Styles", "Format", "Font", "FontSize"],
    ["TextColor", "BGColor", "-"],
    ["InsertMacro", "-"],
    ["Maximize", "ShowBlocks"],
  ];

  config.toolbar_Group = [
    [
      "Bold",
      "Italic",
      "-",
      "NumberedList",
      "BulletedList",
      "-",
      "InsertLink",
      "Unlink",
    ],
  ];

  config.toolbar_Widgets = [
    [
      "Bold",
      "Italic",
      "-",
      "NumberedList",
      "BulletedList",
      "-",
      "InsertLink",
      "Unlink",
      "InsertImageOrMedia",
      "-",
    ],
    ["Format", "Font", "FontSize"],
    ["TextColor", "BGColor"],
  ];

  config.toolbar_EmailWidgets = [
    [
      "Bold",
      "Italic",
      "Underline",
      "-",
      "NumberedList",
      "BulletedList",
      "-",
      "PasteText",
      "PasteFromWord",
      "-",
      "InsertMacro",
      "-",
    ],
  ];

  config.toolbar_Consents_ShortText = [
    [
      "Source",
      "-",
      "Bold",
      "Italic",
      "-",
      "NumberedList",
      "BulletedList",
      "-",
      "InsertLink",
      "Unlink",
      "-",
      "PasteText",
      "PasteFromWord",
    ],
  ];

  config.toolbar_Consents_FullText = [
    [
      "Source",
      "-",
      "Bold",
      "Italic",
      "-",
      "NumberedList",
      "BulletedList",
      "-",
      "InsertLink",
      "Unlink",
      "-",
      "PasteText",
      "PasteFromWord",
    ],
    ["Styles", "Format", "Font", "FontSize"],
    ["TextColor", "BGColor", "-"],
  ];

  config.toolbar_Disabled = [["Maximize"]];

  config.toolbar = config.toolbar_Standard;

  config.scayt_customerid =
    "1:vhwPv1-GjUlu4-PiZbR3-lgyTz1-uLT5t-9hGBg2-rs6zY-qWz4Z3-ujfLE3-lheru4-Zzxzv-kq4";
};

/**
 *
 * Methods used to enhance the admin experience.
 *
 */
window.NJHAdmin = {
  correctAnchorSpanButtons: (target = "a > span.btn") => {
    // Get only links that have span.btn as direct children.
    const buttons = document.querySelectorAll(target);

    if (!buttons || buttons.length === 0) return;

    // ! TODO: Remove all console.logs when ready for prod.
    console.log(`Correcting ${buttons.length} buttons`);

    // Loop over the spans.
    buttons.forEach(function (span) {
      const anchor = span.parentElement;

      // Bail if there are no classes
      if (!span.classList.length) return;

      // Add all classes from the span to the anchor.
      span.classList.forEach((spanClass) => {
        anchor.classList.add(spanClass);
      });

      // Replace the span with just the text of the button.
      anchor.innerHTML = span.textContent;
    });
  },

  correctSpanAnchorButtons: (target = "span.btn > a") => {
    // Get only links that have span.btn as direct parents of anchors.
    const buttons = document.querySelectorAll(target);

    if (!buttons || buttons.length === 0) return;

    // ! TODO: Remove all console.logs when ready for prod.
    console.log(`Correcting ${buttons.length} buttons`);

    // Loop over the anchors.
    buttons.forEach(function (anchor) {
      const span = anchor.parentElement;

      // Bail if there are no classes
      if (!span.classList.length) return;

      // Add all classes from the span to the anchor.
      span.classList.forEach((spanClass) => {
        anchor.classList.add(spanClass);
      });

      // Insert the anchor before the span
      span.parentNode.insertBefore(anchor, span);

      // Delete the span from the DOM
      span.remove();
    });
  },
};

CKEDITOR.on("change", function (e) {
  console.log(e);
  window.NJHAdmin.correctAnchorSpanButtons();
  window.NJHAdmin.correctSpanAnchorButtons();
});
