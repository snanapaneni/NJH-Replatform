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
import Modal from 'bootstrap/js/dist/modal'
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
import _ from 'lodash'
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

App.appHeader                  = App__appHeader;
App.appSidebar                 = App__appSidebar;
// Components
App.accordion                  = App__accordion;
App.formBuilder                = App__formBuilder;
//App.globalAlert                = App__globalAlert;
App.newsletterSignup           = App__newsletterSignup;
App.formCountryProvinceSelects = App__formCountryProvinceSelects;
App.routing                    = App__routing;
App.video                      = App__video;

App.mediaQueries = {
    lg: 'screen and (min-width: 992px)',
    isLargeUp: '',
};

App.modal = {};
App.utils = {
    closeOverlayPanels: function () {
        // Check for and close smallScreenNavPanel
        if ( App.appHeader.element  &&  App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanel].is-open') ) {
            App.appHeader.smallScreenNavPanel__close();
        }

        // Check for and close globalSearchPanel
        if ( App.appHeader.element  &&  App.appHeader.element.querySelector('[data-hook=appHeader__globalSearchPanel][aria-hidden=false]') ) {
            App.appHeader.globalSearchPanel__close();
        }

        // Check for and close primaryNavItem
        if ( App.appHeader.element  &&  App.appHeader.element.querySelector('[data-hook=appHeader__primaryNavItem].is-open') ) {
            App.appHeader.primaryNavItemPanel__close( App.appHeader.element.querySelector('[data-hook=appHeader__primaryNavItem].is-open') );
        }

        // Check for and close portalNav
        if ( App.appHeader.element  &&  App.appHeader.element.querySelector('[data-hook=appHeader__utilityNavPortalMenu].is-open') ) {
            let utilityNavPortalMenuTrigger = App.appHeader.element.querySelector('[data-hook=appHeader__utilityNavPortalMenuTrigger][aria-expanded=true]');
            App.appHeader.togglePortalUtilityNavMenu(utilityNavPortalMenuTrigger);
        }
    },
    adjustBodyTopMargin: function () {
        // Calculate and adjust space to deal with fixed header on small screens
        let spaceBuffer = 0;
        if ( App.mediaQueries.isLargeUp ) {
        } else {
            if ( document.querySelector('[data-hook=appHeader] [data-hook=appHeader__alertBanner]') ) {
                spaceBuffer = spaceBuffer + document.querySelector('[data-hook=appHeader] [data-hook=appHeader__alertBanner]').offsetHeight
            }
            if ( document.querySelector('[data-hook=appHeader] [data-hook=appHeader__primaryBanner]') ) {
                spaceBuffer = spaceBuffer + document.querySelector('[data-hook=appHeader] [data-hook=appHeader__primaryBanner]').offsetHeight
            }
            if ( document.querySelector('[data-hook=appHeader] [data-hook=sideNav] > p') ) {
                spaceBuffer = spaceBuffer + document.querySelector('[data-hook=appHeader] [data-hook=sideNav] > p').offsetHeight
            }
        }
        // document.querySelector('body').style.marginTop = spaceBuffer + 'px';
        let appMain = document.querySelector('.app-main');
        if ( appMain ) {
            appMain.style.paddingTop = spaceBuffer + 'px';
        }
    },
    externalLinkClass: App__externalLinkClass,
    focusTrap: focusTrap,
    // pagination: App__pagination,
    responsiveImage: App__responsiveImage,
    // sibling: App__sibling,
    // urlToolkit: App__urlToolkit,
}

App.utils.pagination = App__pagination;
App.utils.sibling    = App__sibling;
App.utils.urlToolkit = App__urlToolkit;

// Global App component initialization
App.init = function () {
   // App.globalAlert.init();
    App.appHeader.init();
    App.appSidebar.init();
    // Components
    App.accordion.init();
    App.formBuilder.init();
    App.newsletterSignup.init();
    App.formCountryProvinceSelects.init();
    App.video.init();
}



// Setup media query and listener for large screens
const mediaQueryList__lg = window.matchMedia(App.mediaQueries.lg);
// Call listener function explicitly at run time
// App.mediaQueries.isLargeUp = mediaQueryList__lg.matches ? true : false;
// Attach listener function to listen in on state changes
mediaQueryList__lg.addListener((mediaQuery__lg) => {
    // Screen is Large or greater
    if (mediaQueryList__lg.matches) {
        App.mediaQueries.isLargeUp = true;
        // App.utils.closeOverlayPanels();
        // App.appHeader.largeScreenNav__build(App.appHeader.smallScreenNav__destroy);

        if (App.appSidebar.sideNav) {
            App.appSidebar.buildLargeScreenSideNav(App.appSidebar.destroySmallScreenSideNav);
        }

        // Open sidebar accordions (NOTE: these are different than the sideNavAccordion items as they have different rules!)
        document.querySelectorAll('[data-hook=appSidebar] [data-hook=accordionGroupItem]').forEach(function (accordionGroupItem) {
            App.accordion.open(accordionGroupItem);
        });

        // Screen is less than Large
    } else {
        App.mediaQueries.isLargeUp = false;
        // App.utils.closeOverlayPanels();
        // App.appHeader.smallScreenNav__build(App.appHeader.largeScreenNav__destroy);

        if (App.appSidebar.sideNav) {
            App.appSidebar.buildSmallScreenSideNav(App.appSidebar.destroyLargeScreenSideNav);
        }

        // Close sidebar accordions
        document.querySelectorAll('[data-hook=sideNav] [data-hook=sideNavAccordion]').forEach(function (sideNavAccordion) {
            App.appSidebar.closeAccordion(sideNavAccordion);
        });
    }
});


window.addEventListener('load', function (event) {

    App.init();

    App.utils.adjustBodyTopMargin();

    App.utils.externalLinkClass.add()

    reframe('iframe[src*="youtube"], iframe[src*="vimeo"]');

    shortAndSweet('[data-hook=portalProfileHeadlineFieldWrapper] textarea', {
        counterClassName: 'app-portalpage-content__counter',
    });

    /*
     * Keyboard ahortcuts
     * =========================================================================== */
    document.onkeydown = function (e) {
        e = e || window.event;
        var isEscape = false;
        if ('key' in e) {
            isEscape = e.key === 'Escape' || e.key === 'Esc';
        } else {
            isEscape = e.keyCode === 27;
        }
        if (isEscape) {
            App.utils.closeOverlayPanels();
            App.appSidebar.closeSideNavMenu();
        }
    };

    /*
     * Dismiss any panels if appOverlay is clicked
     * =========================================================================== */
    let appOverlay = document.querySelector('[data-hook=appOverlay]');
    if (appOverlay) {
        appOverlay.addEventListener('click', function (e) {
            App.utils.closeOverlayPanels();
            e.target.classList.remove('is-active');
        });
    }

    /*
     * Setup Bootstrap's Modal
     * =========================================================================== */
    let modalTriggers = document.querySelectorAll('[data-hook=modalTrigger]');

    modalTriggers.forEach((modalTrigger) => {
        let relatedModal = document.getElementById( modalTrigger.dataset.modalId );
        // If modal exists, attach event listener to trigger, setup the modal, and show it
        if ( relatedModal ) {
            modalTrigger.addEventListener('click', (e) => {
                App.modal = new Modal( relatedModal, {
                    backdrop: true
                });

                App.modal.show();

                relatedModal.addEventListener('hidden.bs.modal', function (event) {
                    App.modal = {};
                });
            });
        }
    });

});


window.addEventListener('resize', function( event) {
    App.utils.adjustBodyTopMargin();
});

