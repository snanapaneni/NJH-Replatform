const App__imageSlider = {
  sliders: [],
  pauseTriggers: [],

  init: function () {
    this.sliders = document.querySelectorAll("[data-hook=imageSlider]");

    if (!this.sliders.length) return;

    this.handleSliders();


    this.pauseButtons = document.querySelectorAll('[data-hook=imageSlider__pause]');

    if(!this.pauseTriggers.length) return;

    this.listenToPauseTriggers();


  },

  /***
   * 
   * LISTENERS
   * 
   */

  listenToPauseTriggers: function () {

    this.pauseTriggers.forEach(trigger => {
      trigger.addEventListener('click', (e) => {
        e.preventDefault();

        this.handlePause(trigger);

      })
    })
  },
  /***
   * 
   * HANDLERS
   * 
   */
  handleSliders: function () {
    this.sliders.forEach((slider) => {
      const swiperSlider = new App.Swiper(slider, {
        autoplay: this.isAutoplay(slider),

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
    });
  },

  handlePause: function (target) {

    const slider = target.closet('[data-hook=imageSlider]');

    if(slider === undefined) return;

    slider.swiper.pause()
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
