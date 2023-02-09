const App__accordion = {
  // Object Props
  accordions: [],
  accordionItems: [],
  accordionTriggers: [],

  init: function () {
    // Register all of the props.
    this.accordions = document.querySelectorAll("[data-hook=accordion]");

    if (!this.accordions.length) return;

    this.accordionTriggers = document.querySelectorAll(
      "[data-hook=accordion__trigger]"
    );

    this.accordionItems = document.querySelectorAll(
      "[data-hook=accordion__item]"
    );

    if (!this.accordionTriggers.length || !this.accordionItems) return;

    /***
     *
     * FIRE INIT METHODS
     *
     */

    this.listenToAccordionTriggers();

    // Close as a precaution on load
    this.handleCloseAll();
  },

  /***
   *
   * LISTENERS
   *
   */
  listenToAccordionTriggers: function () {
    this.accordionTriggers.forEach((trigger) => {
      trigger.addEventListener("click", (e) => {
        e.preventDefault();

        this.handleClick(e.target);
      });
    });
  },

  /***
   *
   * HANDLERS
   *
   */

  handleClick: function (trigger) {
    const item = trigger.closest("[data-hook=accordion__item]");
    
    if (item.dataset.open === "true") {
      this.handleClose(item);
    } else {
      this.handleOpen(item);
    }

    this.handleCloseSiblings(item);
  },

  handleClose: function (item) {
    const trigger = item.querySelector("[data-hook=accordion__trigger");
    const panel = item.querySelector("[data-hook=accordion__panel]");

    item.setAttribute("data-open", false);
    panel.setAttribute("aria-hidden", true);
    trigger.setAttribute("aria-expanded", false);
  },

  handleOpen: function (item) {
    const trigger = item.querySelector("[data-hook=accordion__trigger");
    const panel = item.querySelector("[data-hook=accordion__panel]");

    item.setAttribute("data-open", true);
    panel.setAttribute("aria-hidden", false);
    trigger.setAttribute("aria-expanded", true);
  },

  handleCloseAll: function () {
    this.accordionItems.forEach((item) => {
      this.handleClose(item);
    });
  },

  handleCloseSiblings: function (item) {
    const siblings = App.utils.sibling.getAll(item);

    if (!siblings.length) return;

    siblings.forEach((item) => {
      this.handleClose(item);
    });
  },
};

export default App__accordion;
