import "../styles/app.scss";

/*
 * NOTE!
 * app-critical.js must be loaded in the browser before this file
 * =========================================================================== */

/*
 * Bootstrap Components
 * Only uncomment what you need to reduce js payload size
 * =========================================================================== */
// import Alert from 'bootstrap/js/dist/alert'
// import Button from 'bootstrap/js/dist/button'
// import Carousel from 'bootstrap/js/dist/carousel'
// import Collapse from 'bootstrap/js/dist/collapse'
// import Dropdown from 'bootstrap/js/dist/dropdown'
import Modal from "bootstrap/js/dist/modal";
// import Modal from 'bootstrap/js/dist/offcanvas'
// import Popover from 'bootstrap/js/dist/popover'
// import Scrollspy from 'bootstrap/js/dist/scrollspy'
// import Tab from 'bootstrap/js/dist/tab'
// import Toast from 'bootstrap/js/dist/toast'
// import Tooltip from 'bootstrap/js/dist/tooltip'

/*
 * Other 3rd Party Dependencies
 * =========================================================================== */
// import accessibleAutocomplete from 'accessible-autocomplete';
import * as focusTrap from "focus-trap";
import _ from "lodash";
import reframe from "reframe.js";
import shortAndSweet from "short-and-sweet/dist/short-and-sweet.module.js";

/*
 * Custom Dependencies
 * =========================================================================== */
// Components
import App__appHeader from "./components/app-header";
import App__appSidebar from "./components/app-sidebar";
import App__accordion from "./components/accordion";
import App__formBuilder from "./components/form-builder";
//import App__globalAlert from "./components/global-alert";
import App__newsletterSignup from "./components/newsletter-signup";
import App__formCountryProvinceSelects from "./components/form-country-province-selects";
import App__routing from "./components/routing";
import App__video from "./components/video";
// Utilities
import App__externalLinkClass from "./utilities/external-link-class";
import App__pagination from "./utilities/pagination";
import App__responsiveImage from "./utilities/responsive-image";
import App__sibling from "./utilities/sibling";
import App__urlToolkit from "./utilities/url-toolkit";
// Animation
// import "./animation/fade-up";
// import "./animation/slide";

/*
 * Setup the global App object
 * =========================================================================== */
let App = (window.App = {});

App.appHeader = App__appHeader;
App.appSidebar = App__appSidebar;
// Components
//App.accordion                  = App__accordion;
//App.formBuilder                = App__formBuilder;
//App.globalAlert                = App__globalAlert;
//App.newsletterSignup           = App__newsletterSignup;
//App.formCountryProvinceSelects = App__formCountryProvinceSelects;
//App.routing                    = App__routing;
//App.video                      = App__video;

App.mediaQueries = {
  is: function (breakpoint, callback) {
    if (breakpoint.matches) {
      callback();
    }
  },
  breakpoints: new Map(),

  isXS: false,
  isSM: false,
  isMD: false,
  isLG: false,
  isXL: false,
  isXXL: false,

  lg: "screen and (min-width: 992px)",
  isLargeUp: "",

  init: function () {
    //console.log("breakpoints, and go.");
    // Set all breakpoints.
    this.breakpoints.set("xs", {
      screen: "screen and (min-width: 0px)",
      condition: "isXS",
    });
    this.breakpoints.set("sm", {
      screen: "screen and (min-width: 576px)",
      condition: "isSM",
    });
    this.breakpoints.set("md", {
      screen: "screen and (min-width: 768px)",
      condition: "isMD",
    });
    this.breakpoints.set("lg", {
      screen: "screen and (min-width: 992px)",
      condition: "isLG",
    });
    this.breakpoints.set("xl", {
      screen: "screen and (min-width: 1200px)",
      condition: "isXL",
    });
    this.breakpoints.set("xxl", {
      screen: "screen and (min-width: 1400px)",
      condition: "isXXL",
    });

    this.breakpoints.forEach((value, key) => {
      const matchMedia = window.matchMedia(value.screen);
      //console.log(`Init Processing ${key}`);
      this.processLogic(matchMedia, key, value);

      matchMedia.addEventListener("change", () => {
        //console.log(`Listener Processing ${key}`);
        this.processLogic(matchMedia, key, value);
      });
    });
  },

  processLogic: function (matchMedia, key, value) {
    if (!matchMedia.matches) {
      //console.log(`${key}: fail`);
      this.resetCondition(value.condition);
    } else {
      //console.log(`${key}: pass`);
      this.matchesCondition(value.condition);
      this.processScreen(key);
    }

    console.log(this);
  },

  resetCondition: function (condition) {
    this[condition] = false;
  },
  matchesCondition: function (condition) {
    this[condition] = true;
  },

  processScreen: function (breakpoint) {
    switch (breakpoint) {
      case "xs":
        break;

      case "sm":
        break;

      case "md":
        // App.appHeader.smallScreenNav__build(
        //   App.appHeader.largeScreenNav__destroy
        // );
        break;

      case "lg":
        // App.appHeader.largeScreenNav__build(
        //   App.appHeader.smallScreenNav__destroy
        // );
        break;

      case "xl":
        break;

      case "xxl":
        break;

      default:
        break;
    }
  },
};

App.modal = {};
App.utils = {
  closeOverlayPanels: function () {
    // Check for and close smallScreenNavPanel
    if (
      App.appHeader.element &&
      App.appHeader.element.querySelector(
        "[data-hook=appHeader__smallScreenNavPanel].is-open"
      )
    ) {
      App.appHeader.smallScreenNavPanel__close();
    }

    // Check for and close globalSearchPanel
    if (
      App.appHeader.element &&
      App.appHeader.element.querySelector(
        "[data-hook=appHeader__globalSearchPanel][aria-hidden=false]"
      )
    ) {
      App.appHeader.globalSearchPanel__close();
    }

    // Check for and close primaryNavItem
    if (
      App.appHeader.element &&
      App.appHeader.element.querySelector(
        "[data-hook=appHeader__primaryNavItem].is-open"
      )
    ) {
      App.appHeader.primaryNavItemPanel__close(
        App.appHeader.element.querySelector(
          "[data-hook=appHeader__primaryNavItem].is-open"
        )
      );
    }

    // Check for and close portalNav
    if (
      App.appHeader.element &&
      App.appHeader.element.querySelector(
        "[data-hook=appHeader__utilityNavPortalMenu].is-open"
      )
    ) {
      let utilityNavPortalMenuTrigger = App.appHeader.element.querySelector(
        "[data-hook=appHeader__utilityNavPortalMenuTrigger][aria-expanded=true]"
      );
      App.appHeader.togglePortalUtilityNavMenu(utilityNavPortalMenuTrigger);
    }

    // Check for and close wantTo
    if (
      App.appHeader.element &&
      App.appHeader.element.querySelector(
        "[data-hook=appHeader__wantToWrapper].is-open"
      )
    ) {
      App.appHeader.wantTo__close();
    }
  },
  adjustBodyTopMargin: function () {
    // Calculate and adjust space to deal with fixed header on small screens
    let spaceBuffer = 0;
    if (App.mediaQueries.isLargeUp) {
    } else {
      if (
        document.querySelector(
          "[data-hook=appHeader] [data-hook=appHeader__alertBanner]"
        )
      ) {
        spaceBuffer =
          spaceBuffer +
          document.querySelector(
            "[data-hook=appHeader] [data-hook=appHeader__alertBanner]"
          ).offsetHeight;
      }
      if (
        document.querySelector(
          "[data-hook=appHeader] [data-hook=appHeader__primaryBanner]"
        )
      ) {
        spaceBuffer =
          spaceBuffer +
          document.querySelector(
            "[data-hook=appHeader] [data-hook=appHeader__primaryBanner]"
          ).offsetHeight;
      }
      if (
        document.querySelector("[data-hook=appHeader] [data-hook=sideNav] > p")
      ) {
        spaceBuffer =
          spaceBuffer +
          document.querySelector(
            "[data-hook=appHeader] [data-hook=sideNav] > p"
          ).offsetHeight;
      }
    }
    // document.querySelector('body').style.marginTop = spaceBuffer + 'px';
    let appMain = document.querySelector(".app-main");
    if (appMain) {
      appMain.style.paddingTop = spaceBuffer + "px";
    }
  },
  externalLinkClass: App__externalLinkClass,
  focusTrap: focusTrap,
  // pagination: App__pagination,
  responsiveImage: App__responsiveImage,
  // sibling: App__sibling,
  // urlToolkit: App__urlToolkit,
};

App.utils.pagination = App__pagination;
App.utils.sibling = App__sibling;
App.utils.urlToolkit = App__urlToolkit;

// Global App component initialization
App.init = function () {
  console.log("App Init");
  // App.mediaQueries.init();
  // App.globalAlert.init();
  App.appHeader.init();

  // App.appSidebar.init();
  // Components
  //App.accordion.init();
  //App.formBuilder.init();
  //App.newsletterSignup.init();
  //App.formCountryProvinceSelects.init();
  //App.video.init();
};

/**
// Setup media query and listener for large screens
const mediaQueryList__lg = window.matchMedia(App.mediaQueries.lg);
// Call listener function explicitly at run time
// App.mediaQueries.isLargeUp = mediaQueryList__lg.matches ? true : false;
// Attach listener function to listen in on state changes
App.mediaQueryList__lg.addEventListener("change", (mediaQuery__lg) => {
  console.log("watching media query");
  // Screen is Large or greater
  if (mediaQueryList__lg.matches) {
    // Screen is less than Large
  } else {
  }
});

**/

window.addEventListener("load", function (event) {
  App.init();

  App.utils.adjustBodyTopMargin();

  App.utils.externalLinkClass.add();

  reframe('iframe[src*="youtube"], iframe[src*="vimeo"]');

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
      App.utils.closeOverlayPanels();
      // App.appSidebar.closeSideNavMenu();
    }
  };

  /*
   * Dismiss any panels if appOverlay is clicked
   * =========================================================================== */
  let appOverlay = document.querySelector("[data-hook=appOverlay]");
  if (appOverlay) {
    appOverlay.addEventListener("click", function (e) {
      App.utils.closeOverlayPanels();
      e.target.classList.remove("is-active");
    });
  }

  /*
   * Setup Bootstrap's Modal
   * =========================================================================== */
  let modalTriggers = document.querySelectorAll("[data-hook=modalTrigger]");

  modalTriggers.forEach((modalTrigger) => {
    let relatedModal = document.getElementById(modalTrigger.dataset.modalId);
    // If modal exists, attach event listener to trigger, setup the modal, and show it
    if (relatedModal) {
      modalTrigger.addEventListener("click", (e) => {
        App.modal = new Modal(relatedModal, {
          backdrop: true,
        });

        App.modal.show();

        relatedModal.addEventListener("hidden.bs.modal", function (event) {
          App.modal = {};
        });
      });
    }
  });
});

window.addEventListener("resize", function (event) {
  App.utils.adjustBodyTopMargin();
});
