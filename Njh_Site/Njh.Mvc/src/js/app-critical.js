import "../styles/app-critical.scss";

/*
 * Other 3rd Party Dependencies
 * =========================================================================== */
import * as focusTrap from "focus-trap";

/*
 * Custom Dependencies
 * =========================================================================== */
// Components
import App__appHeader from "./components/app-header";
import App__accordion from "./components/accordion";
import App__globalAlert from "./components/global-alert";
import App__logoZone from "./components/logo-zone";
// Utilities
import App__externalLinkClass from "./utilities/external-link-class";
import App__responsiveImage from "./utilities/responsive-image";
// Animation
import "./animation/fade-up";
import "./animation/slide";


/*
 * Setup the global App object
 * =========================================================================== */
let App = (window.App = {});

App.appHeader   = App__appHeader;
// Components
App.accordion   = App__accordion;
App.globalAlert = App__globalAlert;
App.logoZone    = App__logoZone;

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
};


App.mediaQueries = {
    lg: 'screen and (min-width: 992px)',
    isLargeUp: '',
};

// Setup media query and listener for large screens
const mediaQueryList__lg = window.matchMedia(App.mediaQueries.lg);
// Call listener function explicitly at run time
App.mediaQueries.isLargeUp = mediaQueryList__lg.matches ? true : false;
// Attach listener function to listen in on state changes
mediaQueryList__lg.addListener((mediaQuery__lg) => {
    // Screen is Large or greater
    if (mediaQueryList__lg.matches) {
        App.mediaQueries.isLargeUp = true;
        App.utils.closeOverlayPanels();
        App.appHeader.largeScreenNav__build(App.appHeader.smallScreenNav__destroy);

        // if (App.appSidebar.sideNav) {
        //     App.appSidebar.buildLargeScreenSideNav(App.appSidebar.destroySmallScreenSideNav);
        // }

        // Open sidebar accordions (NOTE: these are different than the sideNavAccordion items as they have different rules!)
        // document.querySelectorAll('[data-hook=appSidebar] [data-hook=accordionGroupItem]').forEach(function (accordionGroupItem) {
        //     App__accordion.open(accordionGroupItem);
        // });

    // Screen is less than Large
    } else {
        App.mediaQueries.isLargeUp = false;
        App.utils.closeOverlayPanels();
        App.appHeader.smallScreenNav__build(App.appHeader.largeScreenNav__destroy);

        // if (App.appSidebar.sideNav) {
        //     App.appSidebar.buildSmallScreenSideNav(App.appSidebar.destroyLargeScreenSideNav);
        // }

        // Close sidebar accordions
        // document.querySelectorAll('[data-hook=sideNav] [data-hook=sideNavAccordion]').forEach(function (sideNavAccordion) {
        //     App.appSidebar.closeAccordion(sideNavAccordion);
        // });
    }
});

