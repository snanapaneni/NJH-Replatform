const App__accordion = {
  // Object Props
  accordions: [],
  accordionItems: [],
  accordionPanels: [],
  accordionTriggers: [],

  // Used to identify the current open accordion.
  openAccordions: new Map(),

  init: function () {
    // Register all of the props.
    this.accordions = document.querySelectorAll("[data-hook=accordion]");

    if (this.accordions === null || this.accordions === undefined) return;

    this.accordionPanels = document.querySelectorAll(
      "[data-hook=accordion__panel]"
    );

    this.accordionTriggers = document.querySelectorAll(
      "[data-hook=accordion__trigger]"
    );

    this.accordionItems = document.querySelectorAll(
      "[data-hook=accordion__item]"
    );

    if (
      !this.accordionPanels.length ||
      !this.accordionTriggers.length ||
      !this.accordionItems.length
    )
      return;

    /***
     *
     * FIRE INIT METHODS
     *
     */

    this.setUUID();

    this.listenToAccordionTriggers();

    // Close as a precaution.
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
        this.handleClose(e);
        this.handleOpen(e);
      });
    });
  },

  /***
   *
   * HANDLERS
   *
   */

  handleClose: function (e) {
    // Loop over all triggers and close them
    this.accordionTriggers.forEach((trigger) => {
      if (this.isSameAccordionGroup(trigger, e.target)) {
        trigger.setAttribute("aria-expanded", false);
      }
    });

    // Same for all the panels.
    this.accordionPanels.forEach((panel) => {
      if (this.isSameAccordionGroup(panel, e.target)) {
        panel.setAttribute("aria-hidden", true);
      }
    });

    this.accordionItems.forEach((item) => {
      if (this.isSameAccordionGroup(item, e.target)) {
        item.setAttribute("data-open", false);
      }
    });
  },

  handleCloseAll: function (e) {
    // Loop over all triggers and close them
    this.accordionTriggers.forEach((trigger) => {
      trigger.setAttribute("aria-expanded", false);
    });

    // Same for all the panels.
    this.accordionPanels.forEach((panel) => {
      panel.setAttribute("aria-hidden", true);
    });

    this.accordionItems.forEach((item) => {
      item.setAttribute("data-open", false);
    });

    this.openAccordions.clear();
  },

  handleOpen: function (e) {
    e.preventDefault();

    const targetID = e.target.getAttribute("aria-controls");

    // At this point ALL accordions are closed, this will check the last to be opened and see if it's the same as we're clicking.
    // If so, we do nothing, and then reset the openAccordion to null so that on the next click we can open it or another back up.
    if (this.isAccordionOpen(e)) {
      this.openAccordions.delete(targetID);
      return;
    }

    // Change the trigger
    e.target.setAttribute("aria-expanded", true);

    // Find the associated panel based on ID
    const panel = document.getElementById(targetID);

    // Open it up.
    panel.setAttribute("aria-hidden", false);

    e.target
      .closest("[data-hook=accordion__item")
      .setAttribute("data-open", true);

    // Set this as the new open Accordion.
    this.openAccordions.set(targetID, e.target);
  },

  /***
   *
   * CONDITIONS
   *
   */
  isAccordionOpen: function (e) {
    const targetID = e.target.getAttribute("aria-controls");

    return this.openAccordions.size && this.openAccordions.has(targetID);
  },

  isSameAccordionGroup: function (self, target) {
    
    // We'll assume they're in the same group by default.
    if (target === undefined || self === undefined) return true;

    const selfGroup = self
      .closest("[data-hook=accordion]")
      .getAttribute("data-id");
    const targetGroup = target
      .closest("[data-hook=accordion]")
      .getAttribute("data-id");

    return selfGroup === targetGroup;
  },

  /***
   *
   * SETTERS
   *
   */
  setUUID: function () {
    this.accordions.forEach((accordion) => {
      const UUID = App.utils.uuid.generate();
      accordion.setAttribute("data-id", UUID);
    });
  },
};

export default App__accordion;
