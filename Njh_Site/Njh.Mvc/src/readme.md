# Webpack Front-End Build System for Bootstrap 5

## GETTING STARTED
1. Use Node version `14.17.0`
2. Install dependencies with `npm i`
3. Set the `WEBPACK_DIST_PATH` in `config/config.json`. _Note this path is relative to the `config/webpack` directory._
4. Run application with one of the following npm scripts:

| Command                | Description                              |
| ---------------------- | ---------------------------------------- |
| `npm run azure`        | _Compile the static template for Azure ADB2C to consume. This will generate a file at wwwroot/azure/azure.html that has the css/js inlined and the layout's image base64'd so the template will be portable and not rely on assets hosted on a particular environment (other than the Google font assets)._ |
| `npm run dev`          | _Compile & watch for changes._ |
| `npm run dev-hashed`   | _Same as above but with hashed filenames._ |
| `npm run build`        | _*Compile minified production build.* This will also generate .gz (gzip) and .br (brotli) compressed versions of the js and css assets that can be used if the server environment supports them. Can also (optionally) tree-shake the css (more info below)_ |
| `npm run build-hashed` | _Same as above but with hashed filenames._ |

Assets are compiled into the `WEBPACK_DIST_PATH` set in `config/config.json`

## USAGE NOTES

### CSS Tree-Shaking

--------------------

"Tree-shaking" is a technique to remove unused code. PurgeCSS is a tool to accomplish this for CSS. If enabled it will look through the `.html` files in the `src/static` directory and remove all styles that are not in use. This can sometimes remove too many styles in tha case that there are css selectors or html elements that are injected dynamically (via javascript or some server side code). Since those items are not in the static html files, you'll need to do a little work in the postcss.config.js file to tell PurgeCSS to not remove them.

*If you want to disable css tree-shaking completely, comment out the `purgecss` entry in postcss.config.js.*

In postcss.config.js there is an entry for `purgecss`. The following options allow you to set special rules for safelisting specifc selectors or via regex patterns:

#### Safelist Specific Selectors
You can safelist selectors to stop PurgeCSS from removing them from your CSS. This can be accomplished by configuring the PurgeCSS safelist option and/or directly in your CSS with a special comment.

```javascript
const purgecss = new Purgecss({
    content: [], // content
    css: [], // css
    safelist: {
        standard: [/red$/],
        deep: [/blue$/],
        greedy: [/yellow$/]
    }
})
```
In the example, selectors ending with red such as .bg-red, selectors ending with blue as well as their children such as blue p or .bg-blue .child-of-bg, and selectors that have any part ending with yellow such as button.bg-yellow.other-class, will be left in the final CSS.


#### Or add safelisting flags directly in your CSS
You can safelist directly in your CSS with a special comment. 

Use `/*! purgecss ignore */` to safelist the next rule.
```css
/*! purgecss ignore */
h1 {
  color: blue;
}
```

Use `/*! purgecss ignore current */` to safelist the current rule.
```css
h1 {
  /*! purgecss ignore current */
  color: blue;
}
```

Use `/*! purgecss start ignore */` and `/*! purgecss end ignore */` to safelist a range of rules.
```css
/*! purgecss start ignore */
h1 {
  color: blue;
}

h3 {
  color: green;
}
/*! purgecss end ignore */

h4 {
  color: purple;
}

/!* purgecss start ignore */
h5 {
  color: pink;
}

h6 {
  color: lightcoral;
}
/*! purgecss end ignore */
```


You can learn more about PurgeCSS here https://github.com/FullHuman/purgecss-docs


## Creating New Features Process

1. Create new branch under `feature/NJHK13-{number}`

2. Create appropriate HTML files:

	- For templates:
		- Create new HTML file, or copy `index.html` in `./static/`
		- Add ticket number in a comment at the top of the html file.
		- Add new template object to `./static/components/_config.js`
	    ```
	    {
		    templateFilename: "template",
		    header: "app-header",
		    footer: "app-footer",
		    main_components: [
			    // add components here.
		    ],
	    },
	    ```

	- For components:
		- Create new HTML file in `./static/components`
		- Add ticket number in a comment at the top of the html file.
		- Add component to existing template, or create a new template.

3. Restart webpack
	- Note: You will need to restart webpack anytime you add new files OR adjust the config file.

4. Static files write to `../wwwroot/dist`