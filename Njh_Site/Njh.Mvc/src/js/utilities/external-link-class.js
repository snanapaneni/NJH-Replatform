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
        item.hasAttribute('href') &&
        (item.getAttribute('href') !== null && item.getAttribute('href') !== '')
      ) {
        item.classList.add('is-external-link');
        item.setAttribute('target', '_blank');
        item.insertAdjacentHTML(
          'beforeend',
          '<span class="external-link-icon"><span class="visually-hidden"> (Opens in a new window)</span></span>'
        );
      }
    });
  }

}

export default App__externalLinkClass;
