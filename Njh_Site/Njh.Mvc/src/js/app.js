import "../styles/app.scss";

let App = (window.App = {});

/*
 * Other 3rd Party Dependencies
 * =========================================================================== */
// import accessibleAutocomplete from 'accessible-autocomplete';
import * as bootstrap from "bootstrap";
import * as focusTrap from "focus-trap";
import _ from "lodash";
import Swiper from "swiper/bundle";

App.bootstrap = bootstrap;
App.Swiper = Swiper;

/**
 * Custom Dependencies
 * =========================================================================== */
// Components
import App__appHeader from "./components/app-header";
import App__tabs from "./components/tabs";

import App__imageSlider from "./components/image-slider";

// Utilities
import App__linkClasses from "./utilities/link-classes";
import App__formValidation from "./utilities/form-validation";
import App__sibling from "./utilities/sibling";
import App__tables from "./utilities/tables";
import App__timers from "./utilities/timers";
import App__urlToolkit from "./utilities/url-toolkit";
import App__UUID from "./utilities/uuid";


/*
 * Setup the global App object
 * =========================================================================== */

App.appHeader = App__appHeader;

// Components
App.tabs = App__tabs;
App.imageSlider = App__imageSlider;


App.utils = {
  linkClasses: App__linkClasses,
  focusTrap: focusTrap,
  formValidation: App__formValidation,
  sibling: App__sibling,
  urlToolkit: App__urlToolkit,
  tables: App__tables,
  uuid: App__UUID,
};

// Global App component initialization
App.init = function () {
  console.log("App Init");
  App.appHeader.init();

  // Components
  App.tabs.init();

  App.imageSlider.init();
};

  // Utilities
  App.utils.linkClasses.init();
  App.utils.tables.makeResponsive();
  App.utils.formValidation.init();

};

window.addEventListener("load", function (event) {
  App.init();

  /*
   * Keyboard shortcuts
   * =========================================================================== */
  document.onkeydown = function (e) {
    e = e || window.event;
    var isEscape = false;
    if ("key" in e) {
      isEscape = e.key === "Escape" || e.key === "Esc";
    } else {
      isEscape = e.keyCode === 27;
    }
    if (isEscape) {
      App.appHeader.closeOverlayPanels();
    }
  };

});
