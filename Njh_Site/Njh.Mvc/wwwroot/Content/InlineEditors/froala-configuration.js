(function (pageBuilder) {
    var richTextEditor = (pageBuilder.richTextEditor = pageBuilder.richTextEditor || {});
    var plugins = (richTextEditor.plugins = richTextEditor.plugins || []);
    var configurations = (richTextEditor.configurations = richTextEditor.configurations || {});

    /*
     * Plugin: insertHTML_2ColumnPlugin
     */
    var insertHTML_2ColumnPlugin = function (FroalaEditor) {
        FroalaEditor.DefineIcon("twoColIcon", {
            NAME: "twoColumn",
            PATH: "M21.75 0H2.25A2.25 2.25 0 0 0 0 2.25v16.5A2.25 2.25 0 0 0 2.25 21h19.5A2.25 2.25 0 0 0 24 18.75V2.25A2.25 2.25 0 0 0 21.75 0Zm-10.5 19.5h-9a.75.75 0 0 1-.75-.75V3h9.75v16.5Zm11.25-.75a.75.75 0 0 1-.75.75h-9V3h9.75v15.75Z",
        });
        FroalaEditor.RegisterCommand("insertHTML_2Column", {
            title: "Insert 2 Column Area",
            icon: "twoColIcon",
            focus: true,
            undo: true,
            refreshAfterCallback: true,
            callback: function () {
                this.html.insert(
                    '<div class="row"><div class="col col-12 col-lg-6"><p>Column content...</p></div><div class="col col-12 col-lg-6"><p>Column content...</p></div></div><p><br></p>'
                );
                this.undo.saveStep();
            },
        });
    };
    // plugins.push(insertHTML_2ColumnPlugin);

    /*
     * Plugin: insertHTML_3ColumnPlugin
     */
    var insertHTML_3ColumnPlugin = function (FroalaEditor) {
        FroalaEditor.DefineIcon("threeColIcon", {
            NAME: "threeColumn",
            PATH: "M21.75 0H2.25A2.25 2.25 0 0 0 0 2.25v16.5A2.25 2.25 0 0 0 2.25 21h19.5A2.25 2.25 0 0 0 24 18.75V2.25A2.25 2.25 0 0 0 21.75 0ZM7.594 19.5H1.969c-.26 0-.469-.336-.469-.75V3h6.094v16.5Zm14.906-.75c0 .414-.21.75-.469.75h-5.625V3H22.5v15.75ZM9.187 3h5.626v16.5H9.186V3Z",
        });
        FroalaEditor.RegisterCommand("insertHTML_3Column", {
            title: "Insert 3 Column Area",
            icon: "threeColIcon",
            focus: true,
            undo: true,
            refreshAfterCallback: true,
            callback: function () {
                this.html.insert(
                    '<div class="row"><div class="col col-12 col-lg-4"><p>Column content...</p></div><div class="col col-12 col-lg-4""><p>Column content...</p></div><div class="col col-12 col-lg-4"><p>Column content...</p></div></div><p><br></p>'
                );
                this.undo.saveStep();
            },
        });
    };
    //  plugins.push(insertHTML_3ColumnPlugin);


    /*
 * Plugin: insertButtonBlue
 */
    var insertHTML_ButtonBluePlugin = function (FroalaEditor) {
        FroalaEditor.DefineIcon("buttonsDropDown", { NAME: 'cog', SVG_KEY: 'cogs' });
        FroalaEditor.RegisterCommand("insertHTML_Button", {
            title: "Insert Button",
            icon: "buttonsDropDown",
            type: "dropdown",
            focus: true,
            undo: true,
            refreshAfterCallback: true,
            options: {
                'btn-neutral--900': 'Black Button',
                'btn-neutral--900-outline': 'Black Button - outline',
                'btn-neutral--300': 'Grey button',
                'btn-neutral--300-outline': 'Grey button - outline',
                'btn-neutral--100': 'White Button',
                'btn-neutral--100-outline': 'White Button - outline',
                'btn-blue--700': 'Blue button',
                'btn-blue--700-outline': 'Blue button - Outline',
                'btn-orange--900': 'Orange button',
                'btn-orange--900-outline': 'Orange button - Outline',
                'btn-purple--500': 'Purple button',
                'btn-purple--500-outline': 'Purple button - Outline',
            },
            callback: function (cmd, val) {
                this.html.insert(
                    '<button class="' + val + '">' + this.clean.html(this.html.getSelected(), ['p'], [], false) + '</button>'
                );
                this.undo.saveStep();
            },
        });
    };
    plugins.push(insertHTML_ButtonBluePlugin);
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
                "|",
                "insertHTML_Button",
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
    };


})(window.kentico.pageBuilder);
