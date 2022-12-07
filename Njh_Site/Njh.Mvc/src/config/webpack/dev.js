const appConfig = require("../config.json");
// const staticTemplatePartials = require('../static-template-partials')

const fs = require("fs");
const glob = require("glob");
const path = require("path");

const CopyWebpackPlugin = require("copy-webpack-plugin");
const ExtraWatchWebpackPlugin = require("extra-watch-webpack-plugin");
const HtmlWebpackPartialsPlugin = require("html-webpack-partials-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

let plugins = [];

/* TURN OFF STATIC GEN
const staticConfig = require("../../static/components/_config.js");
// Generate static partial plugin entries
staticConfig.templates.forEach(template => {
  // Setup template
  plugins.push(
    new HtmlWebpackPlugin({
      template: path.resolve(
        __dirname,
        "../../static/" + template.templateFilename + ".html"
      ),
      filename: template.templateFilename + ".html",
    })
  );
  // Setup partials to inject
  let templatePartials = [];
  // Add Header
  templatePartials.push({
    template_filename: template.templateFilename + ".html",
    path: path.resolve(__dirname, "../../static/" + template.header + ".html"),
    priority: "replace",
    location: "header-partial",
  });

  // Add Main content components
  template.main_components.forEach(component => {
    let componentFilepath = path.resolve(__dirname, '../../static/components/' + component + '.html')
    try {
      if ( fs.existsSync( componentFilepath ) ) {
        templatePartials.push(
          {
            template_filename: template.templateFilename + '.html',
            path: path.resolve(__dirname, '../../static/components/' + component + '.html'),
            priority: 'low',
            location: 'main-partial'
          }
        );
      }
    } catch( err ) {
      console.error( err )
    }
  });

  // Add Footer
  templatePartials.push({
    template_filename: template.templateFilename + ".html",
    path: path.resolve(__dirname, "../../static/" + template.footer + ".html"),
    priority: "replace",
    location: "footer-partial",
  });

  // Push onto main plugins array
  plugins.push(new HtmlWebpackPartialsPlugin(templatePartials));
});
*/
// Add plugins to main plugins array
plugins.push(
  new MiniCssExtractPlugin({
    filename: "[name].css",
    chunkFilename: "[id].css",
  })
);
plugins.push(
  new CopyWebpackPlugin({
    patterns: [
      {
        from: path.resolve(__dirname, "../../images"),
        to: path.resolve(__dirname, appConfig.WEBROOT + "/images"),
      },
    ],
  })
);
plugins.push(
  new ExtraWatchWebpackPlugin({
    dirs: path.resolve(__dirname, "../../static"),
  })
);

module.exports = {
  mode: "development",

  stats: {
    children: true
  },

  watch: true,

  entry: {
    //critical: path.resolve(__dirname, "../../js/app-critical.js"),
    app: path.resolve(__dirname, "../../js/app.js"),
    //react: path.resolve(__dirname, "../../js/react/app.js"),
  },

  output: {
    filename: "[name].js",
    path: path.resolve(__dirname, appConfig.WEBPACK_DIST_PATH),
    clean: true,
  },

  devtool: "inline-source-map",

  // devServer: {
  //   // contentBase: './',
  //   contentBase: __dirname,
  // },

  plugins: plugins,

  module: {
    rules: [
      // Process SASS/SCSS
      {
        test: /\.s[ac]ss$/i,
        use: [
          MiniCssExtractPlugin.loader, // Output to .css file
          "css-loader", // Convert CSS to a string
          "postcss-loader", // Post-process CSS (Autoprefix, Tree-shake, etc.)
          "sass-loader", // Compile Sass to CSS
        ],
      },

      // Process JS
      {
        test: /\.(js|jsx)$/,
        exclude: /(node_modules|bower_components)/,
        resolve: {
          extensions: [".js", ".jsx"],
        },
        use: {
          loader: "babel-loader",
          options: {
            presets: ["@babel/preset-env"],
          },
        },
      },

      // Process Images
      {
        test: /\.(png|svg|jpg|jpeg|gif)$/i,
        type: "asset/resource",
      },

      // Process Fonts
      {
        test: /\.(woff|woff2|eot|ttf|otf)$/i,
        type: "asset/resource",
      },

      // Process CSV / TSV
      {
        test: /\.(csv|tsv)$/i,
        use: ["csv-loader"],
      },

      // Process XML
      {
        test: /\.xml$/i,
        use: ["xml-loader"],
      },
    ],
  },
};
