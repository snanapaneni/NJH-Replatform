const App__appHeader = {
  element: null,
  focusTrap: false,

  smallScreenNavPanel: false,
  smallScreenNavPanelTrigger: false,
  smallScreenNavPanelDrawer: false,
  smallScreenNavPanelTriggerClose: null,
  smallScreenNavPanelTriggerOpen: null,

  init: function () {
    console.log("appHeader.init");
    this.element = document.querySelector("[data-hook=appHeader]");

    this.smallScreenNavPanel = document.querySelector(
      "[data-hook=appHeader__smallScreenNavPanel]"
    );
    this.smallScreenNavPanelDrawer = document.querySelector(
      "[data-hook=appHeader__smallScreenNavPanelDrawer]"
    );
    this.smallScreenNavPanelTrigger = document.querySelector(
      "[data-hook=appHeader__smallScreenNavPanelTrigger]"
    );

    if (
      this.smallScreenNavPanel &&
      this.smallScreenNavPanelTrigger &&
      this.smallScreenNavPanelDrawer
    ) {
      this.smallScreenNavPanelTriggerClose =
        this.smallScreenNavPanelTrigger.querySelector(
          "[data-hook=appHeader__smallScreenNavPanelTriggerClose]"
        );
      this.smallScreenNavPanelTriggerOpen =
        this.smallScreenNavPanelTrigger.querySelector(
          "[data-hook=appHeader__smallScreenNavPanelTriggerOpen]"
        );

      /* Setup click handlers
       * These are setup as delegated event listeners on appHeader since we are
       * rebuilding these navs as needed for small/large screens.
       * =========================================================================== */

      /* Click handler for: appHeader__globalSearchPanelTrigger
       * Toggles "I want to..." panel
       * =========================================================================== */
      document.addEventListener("click", (e) => {
        if (!e.target.matches("[data-hook=appHeader__wantTo]")) {
          // Check for and close wantTo
          if (
            this.element &&
            this.element.querySelector(
              "[data-hook=appHeader__wantToWrapper].is-open"
            )
          ) {
            this.wantTo__close();
          }

          return false;
        }
        e.preventDefault();

        let appHeader__wantToWrapper = this.element.querySelector(
          "[data-hook=appHeader__wantToWrapper]"
        );
        if (appHeader__wantToWrapper.getAttribute("aria-hidden") == "true") {
          this.wantTo__open();
        } else {
          this.wantTo__close();
        }
      });

      /* Click handler for: appHeader__globalSearchPanelTrigger
       * Toggles global search panel
       * =========================================================================== */
      document.addEventListener("click", (e) => {
        if (
          !e.target.matches(
            "[data-hook=appHeader__globalSearchPanelTrigger], [data-hook=appHeader__smallScreenNavPanelSearchButton]"
          )
        ) {
          return false;
        }
        e.preventDefault();

        let appHeader__globalSearchPanel = this.element.querySelector(
          "[data-hook=appHeader__globalSearchPanel]"
        );
        if (
          appHeader__globalSearchPanel.getAttribute("aria-hidden") == "true"
        ) {
          this.globalSearchPanel__open();
        } else {
          this.globalSearchPanel__close();
        }
      });

      /* Click handler for: appHeader__globalSearchPanelCloseButton
       * Toggles global search panel
       * =========================================================================== */
      document.addEventListener("click", (e) => {
        if (
          !e.target.matches(
            "[data-hook=appHeader__globalSearchPanelCloseButton]"
          )
        ) {
          return false;
        }
        e.preventDefault();

        this.globalSearchPanel__close();
      });

      /* Submit handler for: appHeader__globalSearchHeaderForm
       * REdirects to proper search results url
       * =========================================================================== */
      document.addEventListener("submit", (e) => {
        if (
          !e.target.matches("[data-hook=appHeader__globalSearchHeaderForm]")
        ) {
          return false;
        }
        e.preventDefault();

        this.globalSearch__submit();
      });

      /* Click handler for: appHeader__primaryNavItemPanelTrigger
       * Toggles Subnav panel
       * =========================================================================== */
      this.element.addEventListener("click", (e) => {
        if (
          !e.target.matches("[data-hook=appHeader__primaryNavItemPanelTrigger]")
        ) {
          if (
            this.element &&
            this.element.querySelector(
              "[data-hook=appHeader__primaryNavItem].is-open"
            )
          ) {
            this.primaryNavItemPanel__close(
              this.element.querySelector(
                "[data-hook=appHeader__primaryNavItem].is-open"
              )
            );
          }
          return false;
        }
        e.preventDefault();

        let primaryNavItem = e.target.parentElement;
        if (primaryNavItem.classList.contains("is-open")) {
          this.primaryNavItemPanel__close(primaryNavItem);
        } else {
          this.primaryNavItemPanel__open(primaryNavItem);
        }

        //this.element.querySelector('[data-hook=appHeader__smallScreenNavPanelSearchButton]').style.display = "none";
        //this.element.querySelector('[data-hook=appHeader__smallScreenNavPanelBackButton]').style.display = "inline";
      });

      /* Click handler for: appHeader__smallScreenNavPanelTrigger
       * =========================================================================== */
      this.smallScreenNavPanelTrigger.addEventListener("click", (e) => {
        e.preventDefault();
        // let smallScreenNavPanelTrigger = this.element.querySelector('[data-hook=appHeader__smallScreenNavPanelTrigger]');

        if (this.smallScreenNavPanel.classList.contains("is-open")) {
          this.smallScreenNavPanel__close();
        } else {
          this.smallScreenNavPanel__open();
        }
      });
    }
  },

  // Primary Nav Methods
  //=====================================================
  primaryNavItemPanel__open: function (primaryNavItem) {
    console.log("this.primaryNavItemPanel__open()");

    // Check for and close other open panel
    let openPanel = this.element.querySelector(
      "[data-hook=appHeader__primaryNavItem].is-open"
    );
    if (openPanel) {
      openPanel.classList.remove("is-open");
    }

    let primaryNavItemPanelTrigger = primaryNavItem.querySelector(
      "[data-hook=appHeader__primaryNavItemPanelTrigger]"
    );
    primaryNavItemPanelTrigger.setAttribute("aria-expanded", "true");

    let primaryNavItemPanel = primaryNavItem.querySelector(
      "[data-hook=appHeader__primaryNavItemPanel]"
    );
    primaryNavItemPanel.setAttribute("aria-hidden", "false");

    primaryNavItem.classList.add("is-open");

    let parentNavElement = primaryNavItem.parentElement.closest("nav");
    parentNavElement.classList.add("has-open-panel");

    console.log(parentNavElement);

    let selectors = [];

    // PREVENT focus on...
    selectors = ["[data-hook=appHeader] a", "[data-hook=appHeader] button"];
    this.element
      .querySelectorAll(selectors.join())
      .forEach((item) => item.setAttribute("tabindex", "-1"));

    // ALLOW focus on this item and it's sub elements
    primaryNavItemPanelTrigger.removeAttribute("tabindex");
    primaryNavItem
      .querySelectorAll("[data-hook=appHeader__primaryNavItemPanel] a")
      .forEach((item) => item.removeAttribute("tabindex"));

    this.focusTrap = App.utils.focusTrap.createFocusTrap(
      "[data-hook=appHeader]",
      {
        allowOutsideClick: true,
      }
    );
    this.focusTrap.activate();
  },

  primaryNavItemPanel__close: function (primaryNavItem) {
    // console.log('this.primaryNavItemPanel__close()');

    if (primaryNavItem) {
      let primaryNavItemPanelTrigger = primaryNavItem.querySelector(
        "[data-hook=appHeader__primaryNavItemPanelTrigger]"
      );
      let primaryNavItemPanel = primaryNavItem.querySelector(
        "[data-hook=appHeader__primaryNavItemPanel]"
      );

      primaryNavItem.classList.remove("is-open");

      // PREVENT focus on this item's sub elements
      primaryNavItem
        .querySelectorAll("[data-hook=appHeader__primaryNavItemPanel] a")
        .forEach((item) => item.setAttribute("tabindex", "-1"));

      primaryNavItemPanelTrigger.setAttribute("aria-expanded", "false");
      primaryNavItemPanel.setAttribute("aria-hidden", "true");

      let parentNavElement = primaryNavItem.parentElement.closest("nav");
      parentNavElement.classList.remove("has-open-panel");
    }

    let selectors = [];

    selectors = ["[data-hook=appHeader] a", "[data-hook=appHeader] button"];
    this.element
      .querySelectorAll(selectors.join())
      .forEach((item) => item.removeAttribute("tabindex"));

    document
      .querySelector("[data-hook=appOverlay]")
      .classList.remove("is-active");

    this.focusTrap.deactivate();
  },

  // Mobile Methods
  //=====================================================
  smallScreenNavPanel__initFocusables: function () {
    let selectors = [];

    // PREVENT focus on...
    selectors = [
      "[data-hook=appHeader__logo] > 	a",
      "[data-hook=appHeader__smallScreenNavPanelPhoneNumber]",
      "[data-hook=appHeader__smallScreenNavPanelSearchButton]",
    ];
    this.element
      .querySelectorAll(selectors.join())
      .forEach((item) => item.setAttribute("tabindex", "-1"));

    // ALLOW focus on...
    // selectors = [
    // ];
    // this.element.querySelectorAll(selectors.join()).forEach((item) => item.removeAttribute('tabindex'));
  },

  smallScreenNavPanel__resetFocusables: function () {
    let selectors = [];

    // PREVENT focus on...
    // selectors = [
    // ];
    // this.element.querySelectorAll(selectors.join()).forEach((item) => item.setAttribute('tabindex', '-1'));

    // ALLOW focus on...
    selectors = [
      "[data-hook=appHeader__logo] > a",
      "[data-hook=appHeader__smallScreenNavPanelPhoneNumber]",
      "[data-hook=appHeader__smallScreenNavPanelSearchButton]",
      "[data-hook=appHeader__smallScreenNavPanelTrigger]",
      "[data-hook=appHeader__primaryNavItemPanelTrigger]",
    ];
    this.element
      .querySelectorAll(selectors.join())
      .forEach((item) => item.removeAttribute("tabindex"));
  },

  smallScreenNavPanel__open: function () {
    console.log("this.smallScreenNavPanel__open()");

    this.smallScreenNavPanel__initFocusables();

    // Adjust open trigger
    this.smallScreenNavPanelTrigger.classList.add("is-active");
    this.smallScreenNavPanelTrigger.setAttribute("aria-expanded", "true");

    // Icon visibility swap
    this.smallScreenNavPanelTriggerClose.classList.add("d-inline-block");
    this.smallScreenNavPanelTriggerClose.classList.remove("d-none");
    this.smallScreenNavPanelTriggerOpen.classList.add("d-none");
    this.smallScreenNavPanelTriggerOpen.classList.remove("d-inline-block");

    // Adjust the panel and drawer
    this.smallScreenNavPanel.classList.add("is-open");
    this.smallScreenNavPanelDrawer.setAttribute("aria-hidden", "false");

    // Reset all top level nav items
    this.element
      .querySelectorAll("[data-hook=appHeader__primaryNavItem]")
      .forEach((item) => item.classList.remove("is-open"));

    document.querySelector("body").classList.add("remove-scrollbar");

    this.focusTrap = App.utils.focusTrap.createFocusTrap(
      "[data-hook=appHeader]"
    );
    this.focusTrap.activate();
  },

  smallScreenNavPanel__close: function () {
    console.log("this.smallScreenNavPanel__close()");

    this.smallScreenNavPanel__resetFocusables();

    // Adjust open trigger
    this.smallScreenNavPanelTrigger.classList.remove("is-active");
    this.smallScreenNavPanelTrigger.setAttribute("aria-expanded", "false");

    // Icon visibility swap
    this.smallScreenNavPanelTriggerClose.classList.remove("d-inline-block");
    this.smallScreenNavPanelTriggerClose.classList.add("d-none");
    this.smallScreenNavPanelTriggerOpen.classList.remove("d-none");
    this.smallScreenNavPanelTriggerOpen.classList.add("d-inline-block");

    // Adjust the panel and drawer
    this.smallScreenNavPanel.classList.remove("is-open");
    this.smallScreenNavPanelDrawer.setAttribute("aria-hidden", "true");

    // Reset all top level nav items
    this.element
      .querySelectorAll("[data-hook=appHeader__primaryNavItem]")
      .forEach((item) => item.classList.remove("is-open"));

    document.querySelector("body").classList.remove("remove-scrollbar");

    // Close the panel
    this.smallScreenNavPanel.classList.remove("is-open");

    this.focusTrap.deactivate();
  },

  // Want To (May move to App.search)
  //=====================================================
  wantTo__open: function () {
    let appHeader__wantToWrapper = document.querySelector(
      "[data-hook=appHeader] [data-hook=appHeader__wantToWrapper]"
    );
    appHeader__wantToWrapper.setAttribute("aria-hidden", false);
    appHeader__wantToWrapper.classList.add("is-open");
  },

  wantTo__close: function () {
    let appHeader__wantToWrapper = document.querySelector(
      "[data-hook=appHeader] [data-hook=appHeader__wantToWrapper]"
    );
    appHeader__wantToWrapper.setAttribute("aria-hidden", true);
    appHeader__wantToWrapper.classList.remove("is-open");
  },
};

export default App__appHeader;
