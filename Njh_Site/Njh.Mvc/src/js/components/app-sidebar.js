const App__appSidebar = {

  element: document.body.contains(document.querySelector('[data-hook=appSidebar]')) ? document.querySelector('[data-hook=appSidebar]') : false,

  // Clone nav
  sideNav: document.body.contains(document.querySelector('[data-hook=sideNav]')) ? document.querySelector('[data-hook=sideNav]').cloneNode(true) : false,

  // Cache some elements for easier targeting
  sideNavWrapper: document.querySelector('[data-hook=sideNavWrapper]'),

  focusTrap: false,

  init: function () {
    if ( App__appSidebar.element ) {
      // Attach event listeners for sideNav Menu
      /* Click handler for: sideNav__menuTrigger
       *    Toggles side nav menu. Is a delegated event listener on appSidebar since
       *    we are rebuilding these side navs as needed for small/large screens.
       * This is only visible on small screens, on large the menu is not an accordion
       * =========================================================================== */
      // For sideNav Accordions in appHeader
      App.appHeader.element.addEventListener('click', (e) => {

        if ( !e.target.matches('[data-hook=sideNav__menuTrigger]') ) {
          return false;
        }

        let sideNav__menu = App.appHeader.element.querySelector('[data-hook=sideNav__menu]');

        if ( e.target.getAttribute('aria-expanded') == "false" ) {
          App.appSidebar.openSideNavMenu();
        } else {
          App.appSidebar.closeSideNavMenu();
        }
      });

      // Attach event listeners for accordions
      /* Click handler for: sideNavAccordion__trigger
       *    Toggles accordion. Is a delegated event listener on appSidebar since
       *    we are rebuilding these side navs as needed for small/large screens.
       * Sometimes these live in the appHeader and sometimes in the appSidebar!
       * =========================================================================== */
      // For sideNav Accordions in appHeader
      App.appHeader.element.addEventListener('click', (e) => {
        if ( !e.target.matches('[data-hook=sideNavAccordion__trigger]') ) {
          return false;
        }
        let sideNavAccordion = e.target.closest('[data-hook=sideNavAccordion]');
        if ( e.target.getAttribute('aria-expanded') == "false" ) {
          App__appSidebar.openAccordion(sideNavAccordion);
        } else {
          App__appSidebar.closeAccordion(sideNavAccordion);
        }
      });
      // For sideNav Accordions in appSidebar
      App__appSidebar.element.addEventListener('click', (e) => {
        if ( !e.target.matches('[data-hook=sideNavAccordion__trigger]') ) {
          return false;
        }
        let sideNavAccordion = e.target.closest('[data-hook=sideNavAccordion]');
        if ( e.target.getAttribute('aria-expanded') == "false" ) {
          App__appSidebar.openAccordion(sideNavAccordion);
        } else {
          App__appSidebar.closeAccordion(sideNavAccordion);
        }
      });

      if ( App__appSidebar.sideNav ) {
        if ( App.mediaQueries.isLargeUp ) {
          // console.log('isLargeUp = true');
          App__appSidebar.buildLargeScreenSideNav(App__appSidebar.destroySmallScreenSideNav);
        } else {
          // console.log('isLargeUp = false');
          App__appSidebar.buildSmallScreenSideNav(App__appSidebar.destroyLargeScreenSideNav);
        }
      }

      if ( App.mediaQueries.isLargeUp ) {
        // Open sidebar accordions (NOTE: these are different than the sideNavAccordion items as they have different rules!)
        document.querySelectorAll('[data-hook=appSidebar] [data-hook=accordionGroupItem]').forEach(function (accordionGroupItem) {
          App.accordion.open(accordionGroupItem);
        });
      }
    }
  },

  openSideNavMenu: function() {
    if ( App.appHeader.element && ! App.mediaQueries.isLargeUp ) {
      App.appHeader.element.classList.add('has-open-sidenav');
      App.appHeader.element.querySelector('[data-hook=sideNav__menuTrigger]').setAttribute('aria-expanded', 'true');
      App.appHeader.element.querySelector('[data-hook=sideNav__menu]').style.display = "block";
      App.appHeader.element.querySelector('[data-hook=sideNav__menu]').setAttribute('aria-hidden', 'false');
    }

    App.appHeader.focusTrap = App.utils.focusTrap.createFocusTrap('[data-hook=sideNav]', {
      initialFocus: "#side-nav__menu-trigger"
    });

    if ( App.appHeader.focusTrap ) {
      App.appHeader.focusTrap.activate();
    }
  },

  closeSideNavMenu: function() {

    if ( App.appHeader.element && ! App.mediaQueries.isLargeUp ) {
      App.appHeader.element.classList.remove('has-open-sidenav');
      App.appHeader.element.querySelector('[data-hook=sideNav__menuTrigger]').setAttribute('aria-expanded', 'false');
      App.appHeader.element.querySelector('[data-hook=sideNav__menu]').style.display = "none";
      App.appHeader.element.querySelector('[data-hook=sideNav__menu]').setAttribute('aria-hidden', 'true');
    }

    if ( App.appHeader.focusTrap ) {
      App.appHeader.focusTrap.deactivate();
    }
  },

  openAccordion: function ( sideNavAccordion ) {
    sideNavAccordion.querySelector('[data-hook=sideNavAccordion__trigger]').setAttribute('aria-expanded', 'true');
    sideNavAccordion.querySelector('[data-hook=sideNavAccordion__content]').setAttribute('aria-hidden', 'false');
  },

  closeAccordion: function ( sideNavAccordion ) {
    sideNavAccordion.querySelector('[data-hook=sideNavAccordion__trigger]').setAttribute('aria-expanded', 'false');
    sideNavAccordion.querySelector('[data-hook=sideNavAccordion__content]').setAttribute('aria-hidden', 'true');
  },

  buildSmallScreenSideNav: function ( callback ) {
    // console.log('App__appSidebar.buildSmallScreenSideNav()');

    let smallScreenNavExists = App.appHeader.element.querySelector('[data-hook=sideNav]');
    if ( smallScreenNavExists ) {
      return false;
    }

    // App.appHeader.element.appendChild( App__appSidebar.sideNav );
    App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenSideNavPanel]').appendChild(App__appSidebar.sideNav);

    // document.querySelector('body').style.paddingTop = "126px";

    callback();
  },

  destroySmallScreenSideNav: function () {
    // console.log('App__appSidebar.destroySmallScreenSideNav()');
    // document.querySelector('body').style.marginTop = "initial";
  },

  buildLargeScreenSideNav: function ( callback ) {
    // console.log('App__appSidebar.buildLargeScreenSideNav()');
    let largeScreenNavExists = App__appSidebar.element.querySelector('[data-hook=sideNav]');
    if ( largeScreenNavExists ) {
      return false;
    }

    // console.log('App__appSidebar.largeScreenNav__build()');
    App__appSidebar.sideNavWrapper.appendChild(App__appSidebar.sideNav);

    App__appSidebar.element.querySelector('[data-hook=sideNav__menuTrigger]').setAttribute('aria-expanded', 'true');
    App__appSidebar.element.querySelector('[data-hook=sideNav__menu]').style.display = "block";
    App__appSidebar.element.querySelector('[data-hook=sideNav__menu]').setAttribute('aria-hidden', 'false');

    callback();
  },

  destroyLargeScreenSideNav: function () {
    // console.log('App__appSidebar.destroyLargeScreenSideNav()');
    App__appSidebar.sideNavWrapper.innerHTML = "";
  },
};

export default App__appSidebar;
