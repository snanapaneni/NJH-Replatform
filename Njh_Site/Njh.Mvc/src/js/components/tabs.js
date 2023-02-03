const App__tabs = {

  // Object props
  tabComponents: null,
  tabTriggers: null,
  tabLists: null,
  tabPanels: null,

  init: function () {
    
    // Register the parent element.
    this.tabComponents = document.querySelectorAll("[data-hook=tabs]");

    if (this.tabComponents === null || this.tabComponents === undefined) return;

    // Register Tabs
    this.tabTriggers = document.querySelectorAll(
      "[data-hook=tabs__tabTrigger]"
    );

    // Register Tab List
    this.tabLists = document.querySelectorAll("[data-hook=tabs__tabList]");

    // Register Tab Panels
    this.tabPanels = document.querySelectorAll("[data-hook=tabs__panel]");

    // Bail if any are not registered.
    if (
      !this.tabTriggers.length ||
      !this.tabPanels.length ||
      !this.tabLists.length
    )
      return;

    /**
     *
     * Run through init methods.
     *
     */

    this.setTabUUID();

    this.handleTabsOnLoad();

    this.listenToTabTriggers();

    this.listenToKeyboard();
  },

  /***
   *
   * LISTENER METHODS
   *
   *
   */

  // Set up our listeners to detect clicks on the tabs
  listenToTabTriggers: function () {

    this.tabTriggers.forEach((tab, index) => {
      tab.addEventListener("click", (e) => {
        if (e.target.getAttribute("aria-controls") === undefined) return;

        this.handleTabState(e.target);
        this.handleHistory(e.target);
      });
    });
  },

  // Setup a listener to adjust the active states of the tabs.
  listenToKeyboard: function () {
    // console.log('keyboard');
    this.tabLists.forEach((tablist) => {
      tablist.addEventListener("keydown", (e) => {
        // console.log(e);
        this.handleArrowTraversal(e);
      });
    });
  },

  /***
   *
   * HANDLER METHODS
   *
   */

  handleArrowTraversal: function (e) {
    
    // If we're not on a tab, then bail.
    if (!e.target.classList.contains("tabs__trigger")) return;

    const currentIndex = parseInt(e.target.dataset.index);
    let moveToIndex = parseInt(currentIndex);
    const numberOfTabs = App.utils.sibling.getAll(e.target).length; // How many Tabs do we have?
    let flagged = false; // This will flip if one of the keys we've designated it pressed.

    /**
     *
     * HANDLE KEYS
     *
     */

    switch (e.key) {
      // RIGHT or DOWN
      case "Right": // IE/Edge specific value
      case "ArrowRight":
      case "Down": // IE/Edge specific value
      case "ArrowDown":
        flagged = true;

        // At the end go back to the beggining or increment.
        moveToIndex = numberOfTabs === currentIndex ? 0 : moveToIndex + 1;
        break;

      // LEFT or UP
      case "Left": // IE/Edge specific value
      case "ArrowLeft":
      case "Up": // IE/Edge specific value
      case "ArrowUp":
        flagged = true;

        // At the beginning go to the end or decrement.
        moveToIndex = currentIndex === 0 ? numberOfTabs : moveToIndex - 1;
        break;

      // HOME or ESC
      case "Home":
      case "Esc": // IE/Edge specific value
        // Do something for "enter" or "return" key press.
        flagged = true;
        moveToIndex = 0;
        break;

      // END
      case "End":
        // Do something for "esc" key press.

        flagged = true;
        moveToIndex = numberOfTabs;
        break;

      // NOT THE CORRECT KEY
      default:
        return;
    }

    // A key we're listening to has been pressed.
    if (flagged) {
      e.preventDefault();
      e.stopPropagation();
    }

    const parent = e.target.parentNode;
    const selectedSibling = parent.querySelector(
      `[data-index="${moveToIndex}"]`
    );

    if (selectedSibling === undefined) return;

    this.handleTabState(selectedSibling);
    this.handleHistory(selectedSibling);
    selectedSibling.focus();
  },

  // Control the state of the tabs based on the clicks...
  handleTabState: function (target) {

    // Manage the Panels
    this.tabPanels.forEach((panel, index) => {
      // the targets panel.
      if (panel.id === target.getAttribute("aria-controls")) {
        panel.setAttribute("aria-hidden", false);
        panel.setAttribute("tabIndex", 0);

        // Everyone else
      } else if (target.dataset.tabUuid === panel.dataset.tabUuid) {
        panel.setAttribute("aria-hidden", true);
        panel.setAttribute("tabIndex", -1);
      }
    });

    // Manage the tabs
    this.tabTriggers.forEach((tab, index) => {
      if (target.dataset)
        if (
          tab.getAttribute("aria-controls") ===
          target.getAttribute("aria-controls")
        ) {
          // Matches target
          tab.setAttribute("aria-selected", true);
          tab.setAttribute("tabIndex", 0);

          // Everyone else.
        } else if (target.dataset.tabUuid === tab.dataset.tabUuid) {
          tab.setAttribute("aria-selected", false);
          tab.setAttribute("tabIndex", -1);
        }
    });
  },

  // Change the history state.
  handleHistory: function (target) {
    // console.log("handleHistory");
    const tabID = target.getAttribute("aria-controls");
    const tabName = target.textContent;

    if (tabName === undefined || tabID === undefined) return;

    const historyPathAndParam = `${window.location.protocol}//${window.location.host}${window.location.pathname}?tab=${tabID}`;
    window.history.pushState(
      { path: historyPathAndParam },
      tabName,
      historyPathAndParam
    );
  },

  // Set the active tabs passed to us from the url.
  handleTabsOnLoad: function () {
    // console.log("setTabsOnLoad");
    // Set the first tab of each component as the active one.
    const openTabs = new Map();
    this.tabComponents.forEach((tabComponent) => {
      openTabs.set(
        tabComponent.dataset.uuid,
        tabComponent.querySelector("[data-hook=tabs__tabTrigger]:first-child")
      );
    });

    // If there's a query active, overwrite the initial openTabs.
    const query = App.utils.urlToolkit.parseQueryString();

    if (query !== undefined && query.tab !== undefined) {
      const tabTarget = document.querySelector(
        `[data-hook=tabs__tabTrigger][aria-controls=${query.tab}]`
      );

      if (tabTarget === undefined) return;

      openTabs.set(tabTarget.dataset.tabUuid, tabTarget);
    }

    // Set each of the open tabs.
    openTabs.forEach((val, key) => {
      this.handleTabState(val);
    });
  },

  /***
   *
   * SETTER METHODS
   *
   */

  setTabUUID: function () {
    // console.log("setTabUUID");

    this.tabComponents.forEach((tabComponent) => {
      tabComponent.setAttribute("data-uuid", App.utils.uuid.generate());
    });

    let tabIndex = 0;
    let tabUUID = "";
    this.tabTriggers.forEach((tab, index) => {
      // Set up the UUID to ensure we're
      const currentTabUUID = tab
        .closest("[data-hook=tabs")
        .getAttribute("data-uuid");

      // Set/Manage the indexes
      if (index === 0) {
        tabUUID = currentTabUUID;
      }
      if (tabUUID === currentTabUUID && index !== 0) {
        tabIndex++;
      } else {
        tabUUID = currentTabUUID;
        tabIndex = 0;
      }

      tab.setAttribute("data-index", tabIndex);
      tab.setAttribute("data-tab-uuid", currentTabUUID);
    });

    this.tabPanels.forEach((panel) => {
      // Set up the UUID to ensure we're
      const tabUUID = panel
        .closest("[data-hook=tabs")
        .getAttribute("data-uuid");
      panel.setAttribute("data-tab-uuid", tabUUID);
    });
  },
};

export default App__tabs;
