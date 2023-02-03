const App__mediaQueries = {
  is: function (breakpoint, callback) {
    if (breakpoint.matches) {
      callback();
    }
  },
  breakpoints: new Map(),

  isXS: false,
  isSM: false,
  isMD: false,
  isLG: false,
  isXL: false,
  isXXL: false,

  lg: "screen and (min-width: 992px)",
  isLargeUp: "",

  init: function () {
    //console.log("breakpoints, and go.");
    // Set all breakpoints.
    this.breakpoints.set("xs", {
      screen: "screen and (min-width: 0px)",
      condition: "isXS",
    });
    this.breakpoints.set("sm", {
      screen: "screen and (min-width: 576px)",
      condition: "isSM",
    });
    this.breakpoints.set("md", {
      screen: "screen and (min-width: 768px)",
      condition: "isMD",
    });
    this.breakpoints.set("lg", {
      screen: "screen and (min-width: 992px)",
      condition: "isLG",
    });
    this.breakpoints.set("xl", {
      screen: "screen and (min-width: 1200px)",
      condition: "isXL",
    });
    this.breakpoints.set("xxl", {
      screen: "screen and (min-width: 1400px)",
      condition: "isXXL",
    });

    this.breakpoints.forEach((value, key) => {
      const matchMedia = window.matchMedia(value.screen);
      //console.log(`Init Processing ${key}`);
      this.processLogic(matchMedia, key, value);

      matchMedia.addEventListener("change", () => {
        //console.log(`Listener Processing ${key}`);
        this.processLogic(matchMedia, key, value);
      });
    });
  },

  processLogic: function (matchMedia, key, value) {
    if (!matchMedia.matches) {
      //console.log(`${key}: fail`);
      this.resetCondition(value.condition);
    } else {
      //console.log(`${key}: pass`);
      this.matchesCondition(value.condition);
      this.processScreen(key);
    }

    console.log(this);
  },

  resetCondition: function (condition) {
    this[condition] = false;
  },
  matchesCondition: function (condition) {
    this[condition] = true;
  },

  processScreen: function (breakpoint) {
    switch (breakpoint) {
      case "xs":
        break;

      case "sm":
        break;

      case "md":
        // App.appHeader.smallScreenNav__build(
        //   App.appHeader.largeScreenNav__destroy
        // );
        break;

      case "lg":
        // App.appHeader.largeScreenNav__build(
        //   App.appHeader.smallScreenNav__destroy
        // );
        break;

      case "xl":
        break;

      case "xxl":
        break;

      default:
        break;
    }
  },
};
