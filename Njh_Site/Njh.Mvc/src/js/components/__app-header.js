const App__appHeader = {

  element: null,
  focusTrap: false,

  // Clone navs
  utilityNav: null,
  primaryNav: null,

  // portalNavTrigger: null,
  // portalNav: null,

  // Cache some elements for easier targeting
  largeScreenUtilityNavWrapper: null,
  largeScreenPrimaryNavWrapper: null,
  smallScreenNavPanel:          null,
  smallScreenUtilityNavWrapper: null,
  smallScreenPrimaryNavWrapper: null,

  smallScreenNavPanelOpenTrigger: false,
  smallScreenNavPanelCloseTrigger: false,

  init: function () {
		console.log('appHeader.init')
    App.appHeader.element = document.querySelector('[data-hook=appHeader]');

    App.appHeader.largeScreenUtilityNavWrapper = document.querySelector('[data-hook=appHeader] > [data-hook=appHeader__utilityNavWrapper]');
    App.appHeader.largeScreenPrimaryNavWrapper = document.querySelector('[data-hook=appHeader__primaryBanner] [data-hook=appHeader__primaryNavWrapper]');
    App.appHeader.smallScreenNavPanel          = document.querySelector('[data-hook=appHeader__smallScreenNavPanel]');
    App.appHeader.smallScreenUtilityNavWrapper = document.querySelector('[data-hook=appHeader__smallScreenNavPanel] [data-hook=appHeader__utilityNavWrapper]');
    App.appHeader.smallScreenPrimaryNavWrapper = document.querySelector('[data-hook=appHeader__smallScreenNavPanel] [data-hook=appHeader__primaryNavWrapper]');

    if ( document.querySelector('[data-hook=appHeader__primaryNav]')  &&  document.querySelector('[data-hook=appHeader__utilityNav]') ) {
			console.log('appHeader.init passed')
      App.appHeader.utilityNav = document.querySelector('[data-hook=appHeader__utilityNav]').cloneNode(true),
      App.appHeader.primaryNav = document.querySelector('[data-hook=appHeader__primaryNav]').cloneNode(true),

      window.addEventListener('scroll', (e) => {
          clearTimeout(App.appHeader.scrollThrottle);
          App.appHeader.scrollThrottle = setTimeout(function () {
            // throttle code inside scroll to once every 20 milliseconds
            App.appHeader.toggleHeaderIsScrolledClass();
          }, 20);
        },
        false
      );

      // let portalNavTrigger = document.querySelector('[data-hook=appHeader__utilityNavPortalMenuTrigger]');
      // if ( portalNavTrigger ) {
      //   App.appHeader.portalNavTrigger = portalNavTrigger.cloneNode(true);
      // }

      // let portalNav = document.querySelector('[data-hook=appHeader__utilityNavPortalMenu]');
      // if ( portalNav ) {
      //   App.appHeader.portalNav = portalNav.cloneNode(true);
      // }

      /* Setup click handlers
       * These are setup as delegated event listeners on appHeader since we are
       * rebuilding these navs as needed for small/large screens.
       * =========================================================================== */

			 /* Click handler for: appHeader__globalSearchPanelTrigger
       * Toggles "I want to..." panel
       * =========================================================================== */
      App.appHeader.element.addEventListener('click', (e) => {
        if ( !e.target.matches('[data-hook=appHeader__wantTo]') ) {

					// Check for and close wantTo
					if ( App.appHeader.element && App.appHeader.element.querySelector( "[data-hook=appHeader__wantToWrapper].is-open" ) ) {
						App.appHeader.wantTo__close();
					}

          return false;
        }
        e.preventDefault();

        let appHeader__wantToWrapper = App.appHeader.element.querySelector('[data-hook=appHeader__wantToWrapper]');
        if ( appHeader__wantToWrapper.getAttribute('aria-hidden') == 'true' ) {
          App.appHeader.wantTo__open();
        } else {
          App.appHeader.wantTo__close();
        }
      });

      /* Click handler for: appHeader__globalSearchPanelTrigger
       * Toggles global search panel
       * =========================================================================== */
      App.appHeader.element.addEventListener('click', (e) => {
        if ( !e.target.matches('[data-hook=appHeader__globalSearchPanelTrigger], [data-hook=appHeader__smallScreenNavPanelSearchButton]') ) {
          return false;
        }
        e.preventDefault();

        let appHeader__globalSearchPanel = App.appHeader.element.querySelector('[data-hook=appHeader__globalSearchPanel]');
        if ( appHeader__globalSearchPanel.getAttribute('aria-hidden') == 'true' ) {
          App.appHeader.globalSearchPanel__open();
        } else {
          App.appHeader.globalSearchPanel__close();
        }
      });

      /* Click handler for: appHeader__globalSearchPanelCloseButton
       * Toggles global search panel
       * =========================================================================== */
      App.appHeader.element.addEventListener('click', (e) => {
        if ( !e.target.matches('[data-hook=appHeader__globalSearchPanelCloseButton]') ) {
          return false;
        }
        e.preventDefault();

        App.appHeader.globalSearchPanel__close();
      });

      /* Submit handler for: appHeader__globalSearchHeaderForm
       * REdirects to proper search results url
       * =========================================================================== */
      App.appHeader.element.addEventListener('submit', (e) => {
        if ( !e.target.matches('[data-hook=appHeader__globalSearchHeaderForm]') ) {
          return false;
        }
        e.preventDefault();

        App.appHeader.globalSearch__submit();
      });

      /* Click handler for: appHeader__primaryNavItemPanelTrigger
       * Toggles Portal Dashboard utility menu
       * =========================================================================== */
      App.appHeader.element.addEventListener('click', (e) => {
        if ( !e.target.matches('[data-hook=appHeader__utilityNavPortalMenuTrigger]') ) {
          return false;
        }
        e.preventDefault();

        App.appHeader.togglePortalUtilityNavMenu( e.target );
      });

      /* Click handler for: appHeader__primaryNavItemPanelTrigger
       * Toggles Subnav panel
       * =========================================================================== */
      App.appHeader.element.addEventListener('click', (e) => {
				console.log('click detected');
        if ( !e.target.matches('[data-hook=appHeader__primaryNavItemPanelTrigger]') ) {
					console.log('click failed');
					if (App.appHeader.element && App.appHeader.element.querySelector("[data-hook=appHeader__primaryNavItem].is-open")) {
      			App.appHeader.primaryNavItemPanel__close(App.appHeader.element.querySelector("[data-hook=appHeader__primaryNavItem].is-open"));
    			}
          return false;
        }
        e.preventDefault();

        let primaryNavItem = e.target.parentElement;
        if ( primaryNavItem.classList.contains('is-open') ) {
          App.appHeader.primaryNavItemPanel__close(primaryNavItem);
        } else {
          App.appHeader.primaryNavItemPanel__open(primaryNavItem);
        }

        //App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelSearchButton]').style.display = "none";
        //App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelBackButton]').style.display = "inline";
      });

      /* Click handler for: appHeader__smallScreenNavPanelBackButton
       * =========================================================================== */
      App.appHeader.element.addEventListener('click', (e) => {
        if ( !e.target.matches('[data-hook=appHeader__smallScreenNavPanelBackButton]') ) {
          return false;
        }
        e.preventDefault();

        let primaryNavItem = App.appHeader.element.querySelector('[data-hook=appHeader__primaryNavItem].is-open');
        App.appHeader.primaryNavItemPanel__close(primaryNavItem);
        App.appHeader.globalSearchPanel__close();

        App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelSearchButton]').style.display = "inline";
        App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelBackButton]').style.display = "none";
      });

      /* Click handler for: appHeader__smallScreenNavPanelSearchButton
       * =========================================================================== */
      App.appHeader.element.addEventListener('click', (e) => {
        if ( !e.target.matches('[data-hook=appHeader__smallScreenNavPanelSearchButton]') ) {
          return false;
        }
        e.preventDefault();
      });

      /* Click handler for: appHeader__smallScreenNavPanelTrigger
       * =========================================================================== */
      // App.appHeader.element.querySelectorAll('[data-hook=appHeader__smallScreenNavPanelTrigger]').forEach(function (smallScreenNavPanelTrigger) {
      //   smallScreenNavPanelTrigger.addEventListener('click', (e) => {
      //     e.preventDefault();
      //     let smallScreenNavPanelTrigger = App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelTrigger]');

      //     if (smallScreenNavPanelTrigger.classList.contains('is-active')) {
      //       App.appHeader.smallScreenNavPanel__close();
      //     } else {
      //       App.appHeader.smallScreenNavPanel__open();
      //     }
      //   });
      // });

      // if ( App.mediaQueries.isLargeUp ) {
      //   App.appHeader.largeScreenNav__build( App.appHeader.smallScreenNav__destroy );
      // } else {
      //   App.appHeader.smallScreenNav__build( App.appHeader.largeScreenNav__destroy );
      // }

    }

  },

  togglePortalUtilityNavMenu: function ( utilityNavPortalMenuTrigger ) {
    let utilityNavPortalMenu = document.querySelector('[data-hook=appHeader__utilityNavPortalMenu]');

    utilityNavPortalMenuTrigger.classList.toggle('has-icon-chevron-down');
    utilityNavPortalMenuTrigger.classList.toggle('has-icon-chevron-up');

    utilityNavPortalMenu.classList.toggle('is-open');

    if ( utilityNavPortalMenu.classList.contains('is-open') ) {
      document.querySelector('[data-hook=appOverlay]').classList.add('is-active');

      utilityNavPortalMenuTrigger.setAttribute('aria-expanded', 'true');
      utilityNavPortalMenuTrigger.setAttribute('aria-label', 'Close Portal Nav Menu');
      utilityNavPortalMenu.setAttribute('aria-hidden', 'false');
      App.appHeader.focusTrap = App.utils.focusTrap.createFocusTrap(utilityNavPortalMenu.parentNode, {
        allowOutsideClick: true,
      });
      App.appHeader.focusTrap.activate();
    } else {
      document.querySelector('[data-hook=appOverlay]').classList.remove('is-active');

      utilityNavPortalMenuTrigger.setAttribute('aria-expanded', 'false');
      utilityNavPortalMenuTrigger.setAttribute('aria-label', 'Open Portal Nav Menu');
      utilityNavPortalMenu.setAttribute('aria-hidden', 'true');
      if ( App.appHeader.focusTrap ) {
        App.appHeader.focusTrap.deactivate();
      }
    }
  },

  toggleHeaderIsScrolledClass: function () {
    let scrollTop = window.pageYOffset || (document.documentElement || document.body.parentNode || document.body).scrollTop;
    if ( scrollTop > 10 ) {
      App.appHeader.element.classList.add('is-scrolled');
    } else {
      App.appHeader.element.classList.remove('is-scrolled');
    }
  },

  largeScreenNav__build: function ( callback ) {
    // console.log('App.appHeader.largeScreenNav__build()');
    if ( !App.appHeader.utilityNav  &&  !App.appHeader.primaryNav ) {
      return false;
    }

    document.querySelector('body').classList.remove('remove-scrollbar');

    let largeScreenNavExists = App.appHeader.largeScreenPrimaryNavWrapper.querySelector('[data-hook=appHeader__primaryNav]');
    if ( largeScreenNavExists ) {
      return false;
    }

    App.appHeader.largeScreenUtilityNavWrapper.appendChild( App.appHeader.utilityNav );
    App.appHeader.largeScreenPrimaryNavWrapper.appendChild( App.appHeader.primaryNav );

    callback();
  },

  largeScreenNav__destroy: function () {
    // console.log('App.appHeader.largeScreenNav__destroy()');

    App.appHeader.largeScreenUtilityNavWrapper.innerHTML = "";
    App.appHeader.largeScreenPrimaryNavWrapper.innerHTML = "";
  },

  smallScreenNav__build: function (callback) {
    // console.log('App.appHeader.smallScreenNav__build()');
    if ( !App.appHeader.utilityNav  &&  !App.appHeader.primaryNav ) {
      return false;
    }

    let smallScreenNavExists = App.appHeader.smallScreenPrimaryNavWrapper.querySelector('[data-hook=appHeader__primaryNav]');
    if ( smallScreenNavExists ) {
    }

    App.appHeader.smallScreenUtilityNavWrapper.appendChild( App.appHeader.utilityNav );
    App.appHeader.smallScreenPrimaryNavWrapper.appendChild( App.appHeader.primaryNav );

    // if ( App.appHeader.portalNavTrigger ) {
    //   App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenPortalMenuWrapper]').appendChild(App.appHeader.portalNavTrigger);
    // }

    // if ( App.appHeader.portalNav ) {
    //   App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenPortalMenuWrapper]').appendChild(App.appHeader.portalNav);
    // }

    let selectors = [];

    // PREVENT focus on...
    selectors = [
      '[data-hook=appHeader__smallScreenNavPanel] button',
      '[data-hook=appHeader__smallScreenNavPanel] a',

      '[data-hook=appHeader__smallScreenSideNavPanel] button',
      '[data-hook=appHeader__smallScreenSideNavPanel] a',
    ];
    App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.setAttribute('tabindex', '-1'));

    // ALLOW focus on...
    selectors = [
      '[data-hook=appHeader__logo]',
      '[data-hook=appHeader__smallScreenNavPanelOpenTrigger]',
      '[data-hook=sideNav] > p > a',
      '[data-hook=sideNav__menuTrigger]',
    ];
    App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.removeAttribute('tabindex'));

    /* Click handler for: appHeader__smallScreenNavPanelOpenTrigger
     * =========================================================================== */
    App.appHeader.smallScreenNavPanelOpenTrigger = App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelOpenTrigger]');
    if (App.appHeader.smallScreenNavPanelOpenTrigger) {
      App.appHeader.smallScreenNavPanelOpenTrigger.addEventListener('click', (e) => {
          e.preventDefault();
          App.appHeader.smallScreenNavPanel__open();
        }
      );

      App.appHeader.smallScreenNavPanelOpenTrigger.style.opacity = 1;

    }

    /* Click handler for: appHeader__smallScreenNavPanelCloseTrigger
     * =========================================================================== */
    App.appHeader.smallScreenNavPanelCloseTrigger = App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelCloseTrigger]');
    if (App.appHeader.smallScreenNavPanelCloseTrigger) {
      App.appHeader.smallScreenNavPanelCloseTrigger.addEventListener('click', (e) => {
          e.preventDefault();
          App.appHeader.smallScreenNavPanel__close();
        }
      );
    }

    callback();
  },

  smallScreenNav__destroy: function () {
    // console.log('App.appHeader.smallScreenNav__destroy');
    App.appHeader.smallScreenUtilityNavWrapper.innerHTML = "";
    App.appHeader.smallScreenPrimaryNavWrapper.innerHTML = "";
    App.appHeader.smallScreenNavPanel.classList.remove('is-open');
    App.appHeader.smallScreenNavPanelOpenTrigger = false;
    App.appHeader.smallScreenNavPanelCloseTrigger = false;

    App.appHeader.element.querySelectorAll('[data-hook=appHeader__primaryNavItemPanel] a').forEach((item) => item.removeAttribute('tabindex'));
  },

  smallScreenNavPanel__initFocusables: function () {
    let selectors = [];

    // PREVENT focus on...
    selectors = [
      '[data-hook=appHeader] a',
      '[data-hook=appHeader] button',
    ];
    App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.setAttribute('tabindex', '-1'));

    // ALLOW focus on...
    selectors = [
      '[data-hook=appHeader__smallScreenNavPanelCloseTrigger]',
      '[data-hook=appHeader__smallScreenNavPanelSearchButton]',
      '[data-hook=appHeader__primaryNavItemPanelTrigger]',
      '[data-hook=appHeader__utilityNav] a',
      '[data-hook=appHeader__primaryNavCta] a',
    ];
    App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.removeAttribute('tabindex'));
  },

  smallScreenNavPanel__open: function () {
    // console.log('App.appHeader.smallScreenNavPanel__open()');

    App.appHeader.smallScreenNavPanel__initFocusables();

    // Adjust open trigger
    App.appHeader.smallScreenNavPanelOpenTrigger.style.opacity = 0;
    // App.appHeader.smallScreenNavPanelOpenTrigger.setAttribute('aria-expanded', 'true');

    // Adjust close trigger
    // App.appHeader.smallScreenNavPanelCloseTrigger.setAttribute('aria-expanded', 'true');
    App.appHeader.smallScreenNavPanelCloseTrigger.focus();

    App.appHeader.smallScreenNavPanel.classList.add('is-open');
    App.appHeader.element.querySelectorAll('[data-hook=appHeader__primaryNavItem]').forEach((item) => item.classList.remove('is-open'));

    if ( App.mediaQueries.isLargeUp ) {
    } else {
      document.querySelector('body').classList.add('remove-scrollbar');
    }

    // Hide OneTrust cookie button/banner
    let oneTrust = document.getElementById('onetrust-consent-sdk');
    if ( oneTrust ) {
      oneTrust.style.display = 'none';
    }

    App.appHeader.focusTrap = App.utils.focusTrap.createFocusTrap('[data-hook=appHeader]');
    App.appHeader.focusTrap.activate();
  },

  smallScreenNavPanel__close: function () {
    // console.log('App.appHeader.smallScreenNavPanel__close()');

    let selectors = [];

    // PREVENT focus on...
    selectors = [
      '[data-hook=appHeader__smallScreenNavPanelCloseTrigger]',
      '[data-hook=appHeader__smallScreenNavPanelSearchButton]',
      '[data-hook=appHeader__primaryNavItemPanelTrigger]',
      '[data-hook=appHeader__utilityNav] a',
      '[data-hook=appHeader__primaryNavCta] a',
    ];
    App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.setAttribute('tabindex', '-1'));

    // ALLOW focus on...
    selectors = [
      '[data-hook=appHeader__logo]',
      '[data-hook=appHeader__smallScreenNavPanelOpenTrigger]',
      '[data-hook=sideNav] > p > a',
      '[data-hook=sideNav__menuTrigger]',
    ];
    App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.removeAttribute('tabindex'));

    App.appHeader.element.querySelectorAll('[data-hook=appHeader__primaryNavItem]').forEach((item) => item.classList.remove('is-open'));

    if ( App.mediaQueries.isLargeUp ) {
      // ...
    } else {
      document.querySelector('body').classList.remove('remove-scrollbar');

      // Make sure Global Search is closed
      App.appHeader.globalSearchPanel__close();

      // Close the panel
      App.appHeader.smallScreenNavPanel.classList.remove('is-open');

      // Adjust open trigger
      App.appHeader.smallScreenNavPanelOpenTrigger.style.opacity = 100;
      // App.appHeader.smallScreenNavPanelOpenTrigger.setAttribute('aria-expanded', 'false')
      App.appHeader.smallScreenNavPanelOpenTrigger.focus();

      // Adjust close trigger
      // App.appHeader.smallScreenNavPanelCloseTrigger.setAttribute('aria-expanded', 'false');
    }

    // Restore focusability on other items
    App.appHeader.element.querySelector('[data-hook=appHeader__logo]').removeAttribute('tabindex');
    App.appHeader.element.querySelectorAll('[data-hook=appHeader__alertBanner] a').forEach((item) => item.removeAttribute('tabindex'));

    // Show OneTrust cookie button/banner
    let oneTrust = document.getElementById('onetrust-consent-sdk');
    if ( oneTrust ) {
      oneTrust.style.display = 'initial';
    }

    if ( App.appHeader.focusTrap ) {
      App.appHeader.focusTrap.deactivate();
    }
  },

  primaryNavItemPanel__open: function ( primaryNavItem ) {
    console.log('App.appHeader.primaryNavItemPanel__open()');

    // Check for and close other open panel
    let openPanel = App.appHeader.element.querySelector('[data-hook=appHeader__primaryNavItem].is-open');
    if ( openPanel ) {
      openPanel.classList.remove('is-open');
    }

    let primaryNavItemPanelTrigger = primaryNavItem.querySelector('[data-hook=appHeader__primaryNavItemPanelTrigger]');
    primaryNavItemPanelTrigger.setAttribute('aria-expanded', 'true');

    let primaryNavItemPanel = primaryNavItem.querySelector('[data-hook=appHeader__primaryNavItemPanel]');
    primaryNavItemPanel.setAttribute('aria-hidden', 'false');

    primaryNavItem.classList.add('is-open');

    let selectors = [];

    // PREVENT focus on...
    selectors = [
      '[data-hook=appHeader] a',
      '[data-hook=appHeader] button',
    ];
    App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.setAttribute('tabindex', '-1'));

    
      document.querySelector('[data-hook=appOverlay]').classList.add('is-active');

      // ALLOW focus on this item and it's sub elements
      primaryNavItemPanelTrigger.removeAttribute('tabindex');
      primaryNavItem.querySelectorAll('[data-hook=appHeader__primaryNavItemPanel] a').forEach((item) => item.removeAttribute('tabindex'));

      App.appHeader.focusTrap = App.utils.focusTrap.createFocusTrap('[data-hook=appHeader]', {
          allowOutsideClick: true,
        }
      );
      App.appHeader.focusTrap.activate();
    
  },

  primaryNavItemPanel__close: function ( primaryNavItem ) {
    // console.log('App.appHeader.primaryNavItemPanel__close()');

    if ( primaryNavItem ) {
      let primaryNavItemPanelTrigger = primaryNavItem.querySelector('[data-hook=appHeader__primaryNavItemPanelTrigger]');
      let primaryNavItemPanel = primaryNavItem.querySelector('[data-hook=appHeader__primaryNavItemPanel]');

      primaryNavItem.classList.remove('is-open');

      // PREVENT focus on this item's sub elements
      primaryNavItem.querySelectorAll('[data-hook=appHeader__primaryNavItemPanel] a').forEach((item) => item.setAttribute('tabindex', '-1'));

      primaryNavItemPanelTrigger.setAttribute('aria-expanded', 'false');
      primaryNavItemPanel.setAttribute('aria-hidden', 'true');
    }

    let selectors = [];

    //if ( App.mediaQueries.isLargeUp ) {
      selectors = [
        '[data-hook=appHeader] a',
        '[data-hook=appHeader] button',
      ];
      App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.removeAttribute('tabindex'));

      document.querySelector('[data-hook=appOverlay]').classList.remove('is-active');
    // } else {
    //   App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelHeader]').classList.remove('is-back-to-main');

    //   // PREVENT focus on...
    //   selectors = [
    //     '[data-hook=appHeader] a',
    //     '[data-hook=appHeader] button',
    //   ];
    //   App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.setAttribute('tabindex', '-1'));

    //   // Allow focus on...
    //   App.appHeader.smallScreenNavPanel__initFocusables();
    // }

    if ( App.appHeader.focusTrap ) {
      App.appHeader.focusTrap.deactivate();
    }
  },

  globalSearchPanel__open: function () {
    // console.log('App.appHeader.globalSearchPanel__open()');

    let globalSearchPanel = document.querySelector('[data-hook=appHeader] [data-hook=appHeader__globalSearchPanel]');

    globalSearchPanel.setAttribute('aria-hidden', false);
    globalSearchPanel.querySelector('input[type=text]').focus();

    App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelSearchButton]').style.display = "none";
    App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelBackButton]').style.display = "inline";

    App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelHeader]').classList.add('is-back-to-main');

    let selectors = [];

    // PREVENT focus on...
    selectors = [
      '[data-hook=appHeader] a',
      '[data-hook=appHeader] button',
    ];
    App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.setAttribute('tabindex', '-1'));

    if ( App.mediaQueries.isLargeUp ) {
      document.querySelector('[data-hook=appOverlay]').classList.add('is-active');

      // ALLOW focus on...
      selectors = [
        '[data-hook=appHeader__globalSearchPanelCloseButton]',
        '[data-hook=appHeader] form *',
      ];
      App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.removeAttribute('tabindex'));
    } else {
      // ALLOW focus on...
      selectors = [
        '[data-hook=appHeader__smallScreenNavPanelCloseTrigger]',
        '[data-hook=appHeader__smallScreenNavPanelBackButton]',
        '[data-hook=appHeader] form *',
      ];
      App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.removeAttribute('tabindex'));
    }

    App.appHeader.focusTrap = App.utils.focusTrap.createFocusTrap('[data-hook=appHeader]', {
        initialFocus: App.appHeader.element.querySelector('[data-hook=globalSearch__input]'),
        allowOutsideClick: true,
      }
    );

    App.appHeader.focusTrap.activate();
  },

  globalSearchPanel__close: function () {
    // console.log('App.appHeader.globalSearchPanel__close()');

    let globalSearchPanel = document.querySelector('[data-hook=appHeader] [data-hook=appHeader__globalSearchPanel]');
    globalSearchPanel.setAttribute('aria-hidden', true);

    App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelSearchButton]').style.display = "inline";
    App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelBackButton]').style.display = "none";

    App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelHeader]').classList.remove('is-back-to-main');

    let selectors = [];

    if ( App.mediaQueries.isLargeUp ) {
      // ALLOW focus on...
      selectors = [
        '[data-hook=appHeader] a',
        '[data-hook=appHeader] button',
      ];
      App.appHeader.element.querySelectorAll(selectors.join()).forEach((item) => item.removeAttribute('tabindex'));

      document.querySelector('[data-hook=appOverlay]').classList.remove('is-active');

      if ( App.appHeader.focusTrap ) {
        App.appHeader.focusTrap.deactivate({
          onDeactivate: App.appHeader.element.querySelector('[data-hook=appHeader__globalSearchPanelTrigger]').focus(),
        });
      }
    } else {
      if (App.appHeader.focusTrap) {
        App.appHeader.focusTrap.deactivate({
          onDeactivate: App.appHeader.element.querySelector('[data-hook=appHeader__smallScreenNavPanelSearchButton]').focus(),
        });
      }
    }
  },

  globalSearch__submit: function () {
    // console.log('App.appHeader.globalSearch__submit()');
    let globalSearchPanel = document.querySelector('[data-hook=appHeader] [data-hook=appHeader__globalSearchPanel]');
    const landingUrl = globalSearchPanel.getAttribute('data-landing-url');
    const paramValue = globalSearchPanel.querySelector('[data-hook=globalSearch__input]').value;

    if ( paramValue === "" ) {
      return false;
    } else {
      window.location.href = `${landingUrl}?keyword=${paramValue}`;
    }
  },

	wantTo__open: function () {
		let appHeader__wantToWrapper = document.querySelector('[data-hook=appHeader] [data-hook=appHeader__wantToWrapper]');
		appHeader__wantToWrapper.setAttribute('aria-hidden', false);
		appHeader__wantToWrapper.classList.add('is-open');
	},

	wantTo__close: function () {
		let appHeader__wantToWrapper = document.querySelector('[data-hook=appHeader] [data-hook=appHeader__wantToWrapper]');
		appHeader__wantToWrapper.setAttribute('aria-hidden', true);
		appHeader__wantToWrapper.classList.remove('is-open');
	}
}

export default App__appHeader;
