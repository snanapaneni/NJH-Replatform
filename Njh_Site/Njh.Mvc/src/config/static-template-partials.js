const path = require("path");
const fs = require("fs");

const HtmlWebpackPartialsPlugin = require("html-webpack-partials-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");

let staticConfig = require("../static/_config.js");

const staticPlugins = [];

staticConfig.templates.forEach((template) => {
  // Setup template
  staticPlugins.push(
    new HtmlWebpackPlugin({
      template: path.resolve(
        __dirname,
        "../static/" + template.templateFilename + ".html"
      ),
      filename: template.templateFilename + ".html",
    })
  );
  // Setup partials to inject
  let templatePartials = [];
  // Add Header
  templatePartials.push({
    template_filename: template.templateFilename + ".html",
    path: path.resolve(__dirname, "../static/" + template.header + ".html"),
    priority: "replace",
    location: "header-partial",
  });

  if (template.main_before !== undefined && template.main_before.length > 0) {
    template.main_before.forEach((component) => {
      let componentFilepath = path.resolve(
        __dirname,
        "../static/components/" + component + ".html"
      );
      try {
        if (fs.existsSync(componentFilepath)) {
          templatePartials.push({
            template_filename: template.templateFilename + ".html",
            path: path.resolve(
              __dirname,
              "../static/components/" + component + ".html"
            ),
            priority: "high",
            location: "main-before-partial",
            // options: template.options !== undefined ? template.options : "",
          });
        }
      } catch (err) {
        console.error(err);
      }
    });
  }

  // Add Main content components
  template.main_components.forEach((component) => {
    let componentFilepath = path.resolve(
      __dirname,
      "../static/components/" + component + ".html"
    );
    try {
      if (fs.existsSync(componentFilepath)) {
        templatePartials.push({
          template_filename: template.templateFilename + ".html",
          path: path.resolve(
            __dirname,
            "../static/components/" + component + ".html"
          ),
          priority: "low",
          location: template.main_insert,
          // options: template.options !== undefined ? template.options : "",
        });
      }
    } catch (err) {
      console.error(err);
    }
  });

  if (template.main_after !== undefined && template.main_after.length > 0) {
    template.main_after.forEach((component) => {
      let componentFilepath = path.resolve(
        __dirname,
        "../static/components/" + component + ".html"
      );
      try {
        if (fs.existsSync(componentFilepath)) {
          templatePartials.push({
            template_filename: template.templateFilename + ".html",
            path: path.resolve(
              __dirname,
              "../static/components/" + component + ".html"
            ),
            priority: "low",
            location: "main-after-partial",
            // options: template.options !== undefined ? template.options : "",
          });
        }
      } catch (err) {
        console.error(err);
      }
    });
  }

  // Add Footer
  templatePartials.push({
    template_filename: template.templateFilename + ".html",
    path: path.resolve(__dirname, "../static/" + template.footer + ".html"),
    priority: "replace",
    location: "footer-partial",
  });

  staticPlugins.push(new HtmlWebpackPartialsPlugin(templatePartials));
});

exports.staticPlugins = staticPlugins;
