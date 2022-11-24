
const App__globalAlert = {

  init: function () {
    const alertClose = document.querySelector("[data-hook=appHeader__alertBannerCloseTrigger]");
    const globalAlertBanner = document.querySelector("[data-hook=appHeader__alertBanner]");

    let expiration;
    if (globalAlertBanner) {
      expiration = globalAlertBanner.getAttribute("data-expiration") / 24;
    }

    const closeAlert = function (e) {
      e.preventDefault();
      // alertBar.classList.add("hide");
      App__globalAlert.createCookie("alertBar", "hideAlert", expiration);
      globalAlertBanner.classList.remove("is-visible");
      App.utils.adjustBodyTopMargin();
    };

    const getCookie = function (name) {
      let value = "; " + document.cookie;
      let parts = value.split("; " + name + "=");
      if (parts.length == 2) return parts.pop().split(";").shift();
    };


    if (alertClose) {
      alertClose.addEventListener("click", closeAlert, false);
    }

    let cookieis = getCookie("alertBar");
    if (cookieis === "hideAlert") {
      if (globalAlertBanner) {
        globalAlertBanner.classList.remove("is-visible");
      }
    } else {
      if (globalAlertBanner) {
        globalAlertBanner.classList.add("is-visible");
      }
    }

    // if ( App.mediaQueries.isLargeUp ) {
    // } else {
    //   document.querySelector('body').style.paddingTop = document.querySelector('[data-hook=appHeader]').offsetHeight + 'px';
    // }

  },

  createCookie: function ( name, value, days ) {
    let expires = "";
    if (days) {
      let date = new Date();
      date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
      expires = "; expires=" + date.toGMTString();
    } else {
      expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
  }

};

export default App__globalAlert;
