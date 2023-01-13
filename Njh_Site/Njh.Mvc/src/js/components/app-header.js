// import Collapse from "bootstrap";
const App__appHeader = {
  element: null,
  focusTrap: null,

  smallScreenNavPanel: false,
  smallScreenNavPanelTrigger: false,
  smallScreenNavPanelDrawer: false,
  smallScreenNavPanelTriggerClose: null,
  smallScreenNavPanelTriggerOpen: null,

  appHeader__wantToWrapper: null,

  appHeader__search: null,
  appHeader__searchInput: null,
  appHeader__searchForm: null,

  init: function () {
    this.element = document.querySelector("[data-hook=appHeader]");

    // Init Panel controls
    this.smallScreenNavPanel = document.querySelector(
      "[data-hook=appHeader__smallScreenNavPanel]"
    );
    this.smallScreenNavPanelDrawer = document.querySelector(
      "[data-hook=appHeader__smallScreenNavPanelDrawer]"
    );
    this.smallScreenNavPanelTrigger = document.querySelector(
      "[data-hook=appHeader__smallScreenNavPanelTrigger]"
    );

    // Init Want To controls.
    this.appHeader__wantToWrapper = document.querySelector(
      "[data-hook=appHeader] [data-hook=appHeader__wantToWrapper]"
    );

    // Init Search controls
    this.appHeader__search = document.querySelector(
      "[data-hook=appHeader__search]"
    );
    this.appHeader__searchInput = document.querySelector(
      "[data-hook=appHeader__searchInput]"
    );
    this.appHeader__searchForm = document.querySelector(
      "[data-hook=appHeader__searchForm]"
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

      /* Click handler for: appHeader__wantTo
       * Toggles "I want to..." panel
       * =========================================================================== */
      document.addEventListener("click", (e) => {
        // Safety retun incase the wrapper didn't get set.
        if (this.appHeader__wantToWrapper === null) {
          return;
        }

        if (!e.target.matches("[data-hook=appHeader__wantTo]")) {
          // Check for and close wantTo
          if (
            this.element &&
            this.appHeader__wantToWrapper.classList.contains("is-open")
          ) {
            this.wantTo__close();
          }

          return false;
        }
        e.preventDefault();

        if (
          this.appHeader__wantToWrapper.getAttribute("aria-hidden") == "true"
        ) {
          this.wantTo__open();
        } else {
          this.wantTo__close();
        }
      });

      /* Click handler for: appHeader__primaryNavItemPanelTrigger
       * Toggles Subnav panel
       * =========================================================================== */
      document.addEventListener("click", (e) => {
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

      /* Listen to collapsing search container */
      /* ================================================================= */
      this.appHeader__search.addEventListener("shown.bs.collapse", (event) => {
        this.search__init_focus();
      });

      this.appHeader__search.addEventListener("hidden.bs.collapse", (event) => {
        this.search__reset_focus();
      });

      document.addEventListener("click", (e) => {
        // Is the target click a child of search?
        if (!this.appHeader__search.contains(e.target)) {
          this.search__close();

          return false;
        }
      });

      this.appHeader__searchForm.addEventListener("submit", (event) => {
        event.preventDefault();

        this.search__submit();
      });
    }
  },

  // Primary Nav Methods
  //=====================================================
  primaryNavItemPanel__open: function (primaryNavItem) {
    this.search__close();

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

    if (this.focusTrap !== null) {
      this.focusTrap.deactivate();
    }
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
  },

  smallScreenNavPanel__resetFocusables: function () {
    let selectors = [];

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
    this.search__close();

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

    if (this.focusTrap !== null) {
      this.focusTrap.deactivate();
    }
  },

  // Want To
  //=====================================================
  wantTo__open: function () {
    this.appHeader__wantToWrapper.setAttribute("aria-hidden", false);
    this.appHeader__wantToWrapper.classList.add("is-open");
  },

  wantTo__close: function () {
    this.appHeader__wantToWrapper.setAttribute("aria-hidden", true);
    this.appHeader__wantToWrapper.classList.remove("is-open");
  },

  search__init_focus: function () {
    // Close competing panels
    this.smallScreenNavPanel__close();
    this.primaryNavItemPanel__close();

    this.element.classList.add("has-search-open");
    this.focusTrap = App.utils.focusTrap.createFocusTrap(
      "[data-hook=appHeader]"
    );
    this.appHeader__searchInput.focus({
      focusVisible: true,
    });
    this.focusTrap.activate();
  },

  search__reset_focus: function () {
    if (this.focusTrap === null) return;

    this.element.classList.remove("has-search-open");
    // this.element.focus();
    this.focusTrap.deactivate();
  },

  search__close: function () {
    if (!this.appHeader__search.classList.contains("show")) return;

    const searchCollapse = new App.Collapse(this.appHeader__search);
    searchCollapse.hide();
  },

  search__submit: function () {
    if (!this.appHeader__searchInput.validity.valid) return;

    console.log(window.location);
    const searchUrl = this.appHeader__searchForm.dataset.searchUrl;
    const searchParams = `?searchtext=${this.appHeader__searchInput.value}&searchmode=allwords`;

    window.location.href = searchUrl + searchParams;
  },
};

export default App__appHeader;
