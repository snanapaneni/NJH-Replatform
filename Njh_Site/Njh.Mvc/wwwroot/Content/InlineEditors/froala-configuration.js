(function (pageBuilder) {
    var richTextEditor = (pageBuilder.richTextEditor = pageBuilder.richTextEditor || {});
    var plugins = (richTextEditor.plugins = richTextEditor.plugins || []);
    var configurations = (richTextEditor.configurations = richTextEditor.configurations || {});


    /*
     * Toolbar Configuration
     */
    configurations["default"] = {
        iframeStyleFiles: [
            'https://fonts.googleapis.com/css2?family=Eczar:wght@400;500;600;700;900&family=Open+Sans:ital,wght@0,400;0,600;0,700;0,800;1,400;1,700&display=swap',
            '/Content/InlineEditors/froala-editor-styles.css',
        ],
        toolbarInline: false,
        toolbarVisibleWithoutSelection: true,
        paragraphFormatSelection: true,
        listAdvancedTypes: false,
        quickInsertTags: [],
        pastePlain: true,
        paragraphFormat: {
            N: "Normal",
            H1: "Heading 1",
            H2: "Heading 2",
            H3: "Heading 3",
            H4: "Heading 4",
            H5: "Heading 5",
            H6: "Heading 6",
            PRE: "Code",
        },
        linkEditButtons: [
            "linkOpen",
            "linkEdit",
            "linkRemove"
        ],
        toolbarButtons: [
            ["undo",
                "redo"],
            ["|"],
            ["paragraphFormat",
                "fontSize",
                "bold",
                "italic",
                "strikeThrough",
                "align"],
            ["|"],
            ["formatUL",
                "formatOL",
                "insertLink",
                "insertImage",
                "|",
                "inlineClass",
                "|",
                "insertHTML_2Column",
                "insertHTML_3Column",
                "|",
                "insertTable",
                "quote",
                "insertHR",
                "specialCharacters"],
            ["|"],
            ["clearFormatting",
                "html"]
        ],
        inlineClasses: {
            'btn btn-neutral--900': 'Black Button',
            'btn btn-neutral--900-outline': 'Black Button - outline',
            'btn btn-neutral--300': 'Grey button',
            'btn btn-neutral--300-outline': 'Grey button - outline',
            'btn btn-neutral--100': 'White Button',
            'btn btn-neutral--100-outline': 'White Button - outline',
            'btn btn-blue--700': 'Blue button',
            'btn btn-blue--700-outline': 'Blue button - Outline',
            'btn btn-orange--900': 'Orange button',
            'btn btn-orange--900-outline': 'Orange button - Outline',
            'btn btn-purple--500': 'Purple button',
            'btn btn-purple--500-outline': 'Purple button - Outline',
        }
    };


})(window.kentico.pageBuilder);
