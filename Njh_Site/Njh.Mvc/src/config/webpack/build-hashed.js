const appConfig = require("../config.json");

const fs = require("fs");
const glob = require("glob");
const path = require("path");
const zlib = require("zlib");

const CompressionPlugin = require("compression-webpack-plugin");
const CopyWebpackPlugin = require("copy-webpack-plugin");
const HtmlWebpackPartialsPlugin = require("html-webpack-partials-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const TerserPlugin = require("terser-webpack-plugin");

let plugins = [];

// Add plugins to main plugins array
plugins.push(
  new CompressionPlugin({
    algorithm: "gzip",
    filename: "[path][base].gz",
    test: /\.(js|css)$/,
  })
);

plugins.push(
  new CompressionPlugin({
    algorithm: "brotliCompress",
    filename: "[path][base].br",
    test: /\.(js|css)$/,
    compressionOptions: {
      params: {
        [zlib.constants.BROTLI_PARAM_QUALITY]: 11,
      },
    },
  })
);

plugins.push(
  new MiniCssExtractPlugin({
    // filename: "[contenthash].css",
    filename: (pathData) => {
      return '[name].[contenthash].css';
    },
    // chunkFilename: "[id].css",
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

module.exports = {
  mode: "production",

  entry: {
    //critical: path.resolve(__dirname, "../../js/app-critical.js"),
    app: path.resolve(__dirname, "../../js/app.js"),
    //react: path.resolve(__dirname, "../../js/react/app.js"),
  },

  output: {
    // filename: "[name].[contenthash].js",
    filename: (pathData) => {
      return '[name].[contenthash].js';
    },
    path: path.resolve(__dirname, appConfig.WEBPACK_DIST_PATH),
    clean: true,
  },

  optimization: {
    minimizer: [new TerserPlugin({ extractComments: false })], // Prevents creation on the .LICENSE.txt file
  },

  devtool: "source-map",

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
