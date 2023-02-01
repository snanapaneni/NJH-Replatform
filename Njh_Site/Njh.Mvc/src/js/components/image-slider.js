const App__imageSlider = {
  sliders: [],
  sliderToggles: [],

  init: function () {
    this.sliders = document.querySelectorAll("[data-hook=imageSlider]");

    if (!this.sliders.length) return;

    this.handleSliders();

    this.listenToResize();

    this.sliderToggles = document.querySelectorAll(
      "[data-hook=imageSlider__toggle]"
    );

    if (!this.sliderToggles.length) return;

    this.listenToToggles();
  },

  /***
   *
   * LISTENERS
   *
   */

  listenToToggles: function () {
    this.sliderToggles.forEach((toggle) => {
      toggle.addEventListener("click", (e) => {
        e.preventDefault();

        this.handleToggle(toggle);
      });
    });
  },

  // This keeps the buttons centered on the image so they don't move all over the place.
  listenToResize: function () {
    window.onresize = App.utils.timers.debounce(function () {
      const sliders = document.querySelectorAll("[data-hook=imageSlider]");
      sliders.forEach((slider) => {
        const firstImage = slider.querySelector("img");

        if (firstImage !== undefined) {
          slider.style.setProperty(
            "--swiper-button-top",
            `${firstImage.offsetHeight / 2}px`
          );
        }
      });
    });
  },
  /***
   *
   * HANDLERS
   *
   */
  handleSliders: function () {
    this.sliders.forEach((slider) => {
      const swiper = slider.querySelector("[data-hook=imageSlider__swiper]");

      const initSlider = new App.Swiper(swiper, {
        autoplay: this.isAutoplay(slider),
        autoHeight: true,
        slidesPerView: 1,
        keyboard: {
          enabled: true,
        },
        pagination: {
          el: slider.querySelector(".swiper-pagination"),
          type: "fraction",
        },

        // Navigation arrows
        navigation: {
          nextEl: slider.querySelector(".swiper-button-next"),
          prevEl: slider.querySelector(".swiper-button-prev"),
        },
      });

      // center the buttons on the first image of the slider. 
      const firstImage = slider.querySelector("img");

      if (firstImage !== undefined) {
        slider.style.setProperty(
          "--swiper-button-top",
          `${firstImage.offsetHeight / 2}px`
        );
      }
    });
  },

  handleToggle: function (target) {
    let state = target.dataset.state;

    const slider = target.parentNode.querySelector(
      "[data-hook=imageSlider__swiper]"
    );

    if (slider === undefined || state === undefined) return;

    const autoPlay = slider.swiper.autoplay;

    if (state === "playing") {
      autoPlay.pause();
      target.dataset.state = "paused";
      return;
    }

    autoPlay.run();
    target.dataset.state = "playing";

    return;
  },

  /***
   *
   * CONDITIONS
   *
   */

  isAutoplay: function (slider) {
    let autoPlay =
      slider.dataset.autoplay !== undefined &&
      slider.dataset.autoplay !== "false"
        ? slider.dataset.autoplay
        : false;

    if (!autoPlay) return;

    return {
      delay: parseInt(autoPlay),
    };
  },
};

export default App__imageSlider;
