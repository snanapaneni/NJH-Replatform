const App__imageSlider = {
  sliders: [],
  sliderToggles: [],
  swipers: [],

  init: function () {
    this.sliders = document.querySelectorAll("[data-hook=imageSlider]");

    if (!this.sliders.length) return;

    this.handleSliders();

    this.swipers = document.querySelectorAll("[data-hook=imageSlider__swiper]");

    this.listenToResize();

    this.listenToSwipers();

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

        this.handleTriggerToggle(toggle);
      });
    });
  },

  // This keeps the buttons centered on the image so they don't move all over the place.
  listenToResize: function () {
    window.onresize = App.utils.timers.debounce(function () {
      const sliders = document.querySelectorAll("[data-hook=imageSlider]");

      sliders.forEach((slider) => {
        const activeImage = slider.querySelector(".swiper-slide-active img");

        App__imageSlider.handleNavigationHeight(activeImage, slider);
      });
    });

    
    
  },


  listenToSwipers: function () {

    this.swipers.forEach((slider) => {
      const swiper = slider.swiper;
      swiper.on("slideChangeTransitionEnd", function () {
         const activeImage = slider.querySelector(".swiper-slide-active img");

         App__imageSlider.handleNavigationHeight(activeImage, slider);
      });

      swiper.on('autoplayStop autoplayStart', function () {
        App__imageSlider.handleSwiperAutoPlayToggle(swiper);
      })

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
        loop: true,
        disableOnInteraction: false,
        keyboard: {
          enabled: true,
        },
        pagination: {
          el: slider.querySelector(".swiper-pagination"),
          type: "fraction",
        },
        navigation: {
          nextEl: slider.querySelector(".swiper-button-next"),
          prevEl: slider.querySelector(".swiper-button-prev"),
        },
      });

      // center the buttons on the first image of the slider.
      const firstImage = slider.querySelector(".swiper-slide-active img");
      this.handleNavigationHeight(firstImage, slider);
    });
  },

  handleSwiperAutoPlayToggle: function (swiper)  {

    const slider = swiper?.$el[0]?.parentNode;    
    if(slider === undefined || slider === null) return;

    const trigger = slider.querySelector('[data-hook=imageSlider__toggle]');
    
    if(trigger === undefined || slider === null) return;

    if(swiper.autoplay.running) {
      trigger.dataset.state = "playing";
      return
    }

    trigger.dataset.state = "paused";
    return;
  },

  handleTriggerToggle: function (target) {

    const slider = target.parentNode.querySelector(
      "[data-hook=imageSlider__swiper]"
    );

    if (slider === undefined || slider === null) return;

    const autoPlay = slider.swiper.autoplay;

    if (autoPlay.running) {
      autoPlay.stop();
      return;
    }

    autoPlay.start();
    return;
  },

  handleNavigationHeight: function (target, slider) {
    if (target === undefined || target === null) return;

    slider.style.setProperty(
      "--swiper-button-top",
      `${target.offsetHeight / 2}px`
    );
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
