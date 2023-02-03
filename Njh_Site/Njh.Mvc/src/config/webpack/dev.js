const appConfig = require("../config.json");
const staticPlugins = require("../static-template-partials");

const glob = require("glob");
const path = require("path");

const CopyWebpackPlugin = require("copy-webpack-plugin");
const ExtraWatchWebpackPlugin = require("extra-watch-webpack-plugin");

const MiniCssExtractPlugin = require("mini-css-extract-plugin");

let plugins = [];

// Generate static partial plugin entries

if (staticPlugins.staticPlugins) {
  plugins.push(...staticPlugins.staticPlugins);
}

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
        to: path.resolve(__dirname, appConfig.WEBPACK_DIST_PATH + "/images"),
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
    children: true,
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

  devServer: {
    static: {
      directory: path.join(__dirname, appConfig.WEBROOT),
    },
    compress: true,
    port: 9000,
  },

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
