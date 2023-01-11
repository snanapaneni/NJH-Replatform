const App__externalLinkClass = {
  add: function () {
    let selectors = [
      '.app-header__alert-banner-content a:not([href*="bot.com"]):not([href*="bot-lh"]):not([href*="website-app-trbot"]):not([href="#"]):not([href^="#main"]):not([href^="/"]):not([href^="mailto"])',
      '.app-header__utility-nav-portal-menu a:not([href*="bot.com"]):not([href*="bot-lh"]):not([href*="website-app-trbot"]):not([href="#"]):not([href^="#main"]):not([href^="/"]):not([href^="mailto"])',
      'main a:not([href*="bot.com"]):not([href*="bot-lh"]):not([href*="website-app-trbot"]):not([href^="#"]):not([href^="#main"]):not([href^="/"]):not([href^="mailto"])',
      'main a[target="_blank"]',
      '.app-footer__primary-nav-list a:not([href*="bot.com"]):not([href*="bot-lh"]):not([href*="website-app-trbot"]):not([href="#"]):not([href^="#main"]):not([href^="/"]):not([href^="mailto"])',
      '.app-footer .logo-zone a:not([href*="bot.com"]):not([href*="bot-lh"]):not([href*="website-app-trbot"]):not([href="#"]):not([href^="#main"]):not([href^="/"]):not([href^="mailto"])',
    ];

    document.querySelectorAll(selectors.join()).forEach(function (item) {
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
};

export default App__externalLinkClass;
