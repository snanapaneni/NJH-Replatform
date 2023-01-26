const App__accordion = {
  accordions: [],
  accordionItems: [],
  accordionPanels: [],
  accordionTriggers: [],

  openAccordion: null,

  init: function () {
    this.accordions = document.querySelectorAll("[data-hook=accordion]");

    if (this.accordions === null || this.accordions === undefined) return;

    this.accordionPanels = document.querySelectorAll(
      "[data-hook=accordion__panel]"
    );

    this.accordionTriggers = document.querySelectorAll(
      "[data-hook=accordion__trigger]"
    );

    if (!this.accordionPanels.length || !this.accordionTriggers.length) return;

    /***
     *
     * FIRE INIT METHODS
     *
     */

    this.listenToAccordionTriggers();

    // Close as a precaution.
    this.closeAccordions();
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

  handleClose: function () {
    this.accordionTriggers.forEach((trigger) => {
      trigger.setAttribute("aria-expanded", false);
    });

    this.accordionPanels.forEach((panel) => {
      panel.setAttribute("aria-hidden", true);
    });
  },

  handleOpen: function (e) {
    e.preventDefault();

    if (this.isAccordionOpen(e)) {
      this.openAccordion = null;
      return;
    }

    e.target.setAttribute("aria-expanded", true);

    const panel = document.getElementById(
      e.target.getAttribute("aria-controls")
    );

    panel.setAttribute("aria-hidden", false);

    this.openAccordion = e.target;
  },

  /***
   * 
   * CONDITIONS
   * 
   */
  isAccordionOpen: function (e) {
    return (
      this.openAccordion !== null &&
      this.openAccordion.getAttribute("aria-controls") ===
        e.target.getAttribute("aria-controls")
    );
  },
};

export default App__accordion;
