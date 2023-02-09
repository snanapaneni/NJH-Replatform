const App__linkClasses = {
  init: function () {
    this.correctAnchorSpanButtons();
    this.correctSpanAnchorButtons();

    this.addExternalIcon();
  },
  addExternalIcon: function () {
    const internalUrls = [
      // Same Page
      '[href^="#"]',

      // Relative
      '[href^="/"]',
      '[href^="./"]',

      // Device triggers
      '[href^="mailto"]',
      '[href^="tel"]',

      // Dev/Prod environments
      '[href*="nationaljewish.org"]',
      '[href*="njhealth.org"]',
      '[href*="njhmvc"]',
      '[href*="localhost"]',
    ];

    // The selectors we want to target.
    const selectors = [".app-main a", ".app-header__utility-nav a"];

    // Add any "non-automated" selectors here.
    let querySelector = [];

    // Loop over the selectors and apply a not to the interal urls.
    selectors.map((selector) => {
      querySelector.push(`${selector}:not(${internalUrls.join(",")})`);
    });

    document.querySelectorAll(querySelector.join(",")).forEach(function (item) {
      if (
        item.hasAttribute("href") &&
        item.getAttribute("href") !== null &&
        item.getAttribute("href") !== ""
      ) {
        item.classList.add("is-external-link");
        item.setAttribute("target", "_blank");
        item.insertAdjacentHTML(
          "beforeend",
          '<span class="app-icon icon-external-link"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M304 24c0 13.3 10.7 24 24 24H430.1L207 271c-9.4 9.4-9.4 24.6 0 33.9s24.6 9.4 33.9 0l223-223V184c0 13.3 10.7 24 24 24s24-10.7 24-24V24c0-13.3-10.7-24-24-24H328c-13.3 0-24 10.7-24 24zM72 32C32.2 32 0 64.2 0 104V440c0 39.8 32.2 72 72 72H408c39.8 0 72-32.2 72-72V312c0-13.3-10.7-24-24-24s-24 10.7-24 24V440c0 13.3-10.7 24-24 24H72c-13.3 0-24-10.7-24-24V104c0-13.3 10.7-24 24-24H200c13.3 0 24-10.7 24-24s-10.7-24-24-24H72z"/></svg><span class="visually-hidden"> (Opens in a new window)</span></span>'
        );
      }
    });
  },

  correctAnchorSpanButtons: function (target = ".app-main a > span.btn") {
    // Get only links that have span.btn as direct children.
    const buttons = document.querySelectorAll(target);

    if (!buttons || buttons.length === 0) return;

    // Loop over the spans.
    buttons.forEach(function (span) {
      const anchor = span.parentElement;

      // Bail if there are no classes
      if (!span.classList.length) return;

      // Add all classes from the span to the anchor.
      span.classList.forEach((spanClass) => {
        anchor.classList.add(spanClass);
      });

      // Replace the span with just the text of the button.
      anchor.innerHTML = span.textContent;
    });
  },

  correctSpanAnchorButtons: function (target = ".app-main span.btn > a") {
    // Get only links that have span.btn as direct parents of anchors.
    const buttons = document.querySelectorAll(target);

    if (!buttons || buttons.length === 0) return;

    // Loop over the anchors.
    buttons.forEach(function (anchor) {
      const span = anchor.parentElement;

      // Bail if there are no classes
      if (!span.classList.length) return;

      // Add all classes from the span to the anchor.
      span.classList.forEach((spanClass) => {
        anchor.classList.add(spanClass);
      });

      // Insert the anchor before the span
      span.parentNode.insertBefore(anchor, span);

      // Delete the span from the DOM
      span.remove();
    });
  },
};

export default App__linkClasses;
