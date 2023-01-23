const App__alphasort = {
  element: null,
  resultsWrapper: null,

  triggers: null,
  results: null,

  init: function () {
    this.element = document.querySelector(".alphasort");

    if (this.element === null) return;

    this.resultsWrapper = document.getElementById("alphaResultsWrapper");

    this.triggers = Array.from(
      this.element.querySelectorAll("[data-hook=alphasort__trigger]")
    );

    this.results = Array.from(
      this.element.querySelectorAll("[data-hook=alphasort__section]")
    );

    if (!this.triggers.length || !this.results.length) return;

    // console.log(this.triggers);
    // console.log(this.results);

    this.triggers.forEach((trigger) => {
      trigger.addEventListener("click", (e) => {
        if (e.target.dataset.sort === undefined) return;

        this.setResultSections(e.target);
        this.setTriggers(e.target);
        this.scrollToSection(e.target);
      });
    });
  },

  setResultSections: function (target) {
    const sortBy = target.dataset.sort;

    this.results.forEach((section) => {
      if (sortBy === "all") {
        section.classList.remove("d-none");
        section.setAttribute("aria-hidden", false);

        this.element.classList.remove("is-filtered");
      } else if (section.dataset.sortSection === sortBy) {
        section.setAttribute("aria-hidden", false);
        section.classList.remove("d-none");

        this.element.classList.add("is-filtered");
      } else {
        section.setAttribute("aria-hidden", true);
        section.classList.add("d-none");
      }
    });

    return;
  },

  setTriggers: function (target) {
    const sortBy = target.dataset.sort;

    this.triggers.forEach((trigger) => {
      trigger.classList.remove("is-active");

      if (sortBy === "all") {
        trigger.setAttribute("aria-expanded", true);
      } else {
        trigger.setAttribute("aria-expanded", false);
      }
    });

    target.classList.add("is-active");
    target.setAttribute("aria-expanded", true);

    return;
  },

  scrollToSection: function (target) {
    if (target.dataset.sort === "all") {
      return;
    }

    this.resultsWrapper.scrollIntoView({
      behavior: "smooth",
    });
  },
};

export default App__alphasort;
