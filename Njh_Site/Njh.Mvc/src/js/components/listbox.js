const App__listbox = {
  
  /**
   * 
   * PROPS (ELEMENTS)
   * 
   */
  
  listboxes: [],
  listbox__triggers: [],

  init: function () {
    /**
     *
     * REGISTER ELEMENTS
     *
     */
    this.listboxes = document.querySelectorAll("[data-hook=listbox]");

    this.listbox__triggers = document.querySelectorAll(
      "[data-hook=listbox__trigger]"
    );

    if (!this.listboxes.length || !this.listbox__triggers) return;

    /***
     *
     * RUN INIT METHODS
     *
     */

    this.handleCloseAll();

    this.listenToTriggers();

    this.listenToLinks();
  },

  /***
   *
   * LISTENERS
   *
   */

  listenToTriggers: function () {
    this.listbox__triggers.forEach((trigger) => {
      trigger.addEventListener("click", (e) => {
        e.preventDefault();

        this.handleClick(e.target);
      });
    });
  },

  listenToLinks: function () {
    // Close the box is a link in the list is closed.
    // This is just incase on of the links is on the same page.
    this.listboxes.forEach((listbox) => {
      const links = listbox.querySelectorAll("a");

      if (links.length) {
        links.forEach((link) => {
          link.addEventListener("click", (e) => {
            this.handleCloseAll();
          });
        });
      }
    });
  },

  /***
   *
   * HANDLERS
   *
   */
  handleClick: function (target) {
    const listbox = target.closest("[data-hook=listbox]");

    if (listbox.dataset.open === "false") {
      console.log("open it");
      this.handleCloseAll();
      this.handleOpen(listbox);
    } else {
      console.log("close");
      this.handleClose(listbox);
    }
  },

  handleOpen: function (listbox) {
    const trigger = listbox.querySelector("[data-hook=listbox__trigger]");
    const wrapper = listbox.querySelector("[data-hook=listbox__wrapper]");

    listbox.setAttribute("data-open", true);
    wrapper.setAttribute("aria-hidden", false);
    trigger.setAttribute("aria-expanded", true);
  },

  handleClose: function (listbox) {
    const trigger = listbox.querySelector("[data-hook=listbox__trigger]");
    const wrapper = listbox.querySelector("[data-hook=listbox__wrapper]");

    listbox.setAttribute("data-open", false);
    wrapper.setAttribute("aria-hidden", true);
    trigger.setAttribute("aria-expanded", false);
  },

  handleCloseAll: function () {
    this.listboxes.forEach((listbox) => {
      this.handleClose(listbox);
    });
  },
};

export default App__listbox;
