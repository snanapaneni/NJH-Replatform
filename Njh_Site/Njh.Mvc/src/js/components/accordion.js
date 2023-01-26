const App__accordion = {
  
  // Object Props
  accordions: [],
  accordionItems: [],
  accordionPanels: [],
  accordionTriggers: [],

  // Used to identify the current open accordion. 
  openAccordion: null,

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

    if (!this.accordionPanels.length || !this.accordionTriggers.length || !this.accordionItems.length) return;

    /***
     *
     * FIRE INIT METHODS
     *
     */

    this.listenToAccordionTriggers();

    // Close as a precaution.
    this.handleClose();
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
    // Loop over all triggers and close them
    this.accordionTriggers.forEach((trigger) => {
      trigger.setAttribute("aria-expanded", false);
    });

    // Same for all the panels. 
    this.accordionPanels.forEach((panel) => {
      panel.setAttribute("aria-hidden", true);
    });

    this.accordionItems.forEach((trigger) => {
      trigger.setAttribute("data-open", false);
    });
  },

  handleOpen: function (e) {

    
    e.preventDefault();

    // At this point ALL accordions are closed, this will check the last to be opened and see if it's the same as we're clicking. 
    // If so, we do nothing, and then reset the openAccordion to null so that on the next click we can open it or another back up. 
    if (this.isAccordionOpen(e)) {
      this.openAccordion = null;
      return;
    }

    // Change the trigger
    e.target.setAttribute("aria-expanded", true);

    // Find the associated panel based on ID
    const panel = document.getElementById(
      e.target.getAttribute("aria-controls")
    ); 

    // Open it up.
    panel.setAttribute("aria-hidden", false);


    e.target.closest('[data-hook=accordion__item').setAttribute('data-open', true);

    // Set this as the new open Accordion.
    this.openAccordion = e.target;
  },

  /***
   * 
   * CONDITIONS
   * 
   */
  isAccordionOpen: function (e) {
    // Used in this.handleOpen
    return (
      this.openAccordion !== null &&
      this.openAccordion.getAttribute("aria-controls") ===
        e.target.getAttribute("aria-controls")
    );
  },
};

export default App__accordion;
