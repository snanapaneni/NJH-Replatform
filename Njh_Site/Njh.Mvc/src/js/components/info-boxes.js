const App__infoBoxes = {
  modal: undefined,
  modalBody: undefined,
  data: undefined,
  sampleObject: {
    Body: `<div class="row gy-3"> <div class="col-12 col-lg-4"> <img src="/images/PHYSICIAN_IMAGE.jpg" alt="PHYSICIAN_NAME" loading="eager" /> </div> <div class="col-12 col-lg-8"> <h3><a href="/PHYSICIAN_URL">PHYSICIAN_NAME</a></h3> <p>PHYSICIAN_BIO</p> <ul role="list" aria-label="Positions"> <li>POSITION_NAME</li> <li>POSITION_NAME</li> </ul> <ul role="list" aria-label="Divisions and departments"> <li><a href="/DIVISION_URL">DIVISION_NAME</a></li> <li><a href="/DIVISION_URL">DIVISION_NAME</a></li> </ul> <hr class="hr-orange--500" /> <a href="/PHYSICIAN_URL">Full Profile</a> <hr /> <div class="row gy-3 align-items-center justify-content-end"> <div class="col-12 col-md-6"> <h4>Patient Rating</h4> <div class="info-boxes__rating"> <img src="/images/stars/1_0.svg" alt="" role="presentation" /> <img src="/images/stars/1_0.svg" alt="" role="presentation" /> <img src="/images/stars/1_0.svg" alt="" role="presentation" /> <img src="/images/stars/1_0.svg" alt="" role="presentation" /> <img src="/images/stars/0_8.svg" alt="" role="presentation" /> </div> <p class="text-muted"> <span itemprop="ratingValue">PHYSICIAN_RATING</span> out of <span itemprop="bestRating">5</span> </p> </div> <div class="col-12 col-md-6 text-md-end"> <a href="https://www.nationaljewish.org/Patients-Visitors/Make-an-Appointment" class="btn btn-sm btn-blue--700"> Make an Appointment </a> </div> </div> </div> </div>`,
  },

  allTriggers: [],

  init: function () {
    this.modal = document.getElementById("infoBoxModal");

    if (this.modal === undefined || !this.modal) return;

    this.modalBody = this.modal.querySelector("[data-hook=physician_body]");

    // Precautionary reset
    this.handleReset();

    this.listenToModalOpen();

    this.listenToModalClose(); 


    this.allTriggers = document.querySelectorAll('[data-hook=itemBoxes__revealAll]')
    this.listenToAllTrigger();

    
  },

  /***
   *
   * LISTENERS
   *
   */

  listenToModalOpen: async function () {
    this.modal.addEventListener("show.bs.modal", async (e) => {
      // Button that triggered the modal

      this.handleLoading(true);
      this.modalEvent = e;

      await this.setData(e);

      if (this.data === undefined) {
        this.noResults();
        this.handleLayout(false);
        return;
      }

      this.handleLayout();

      this.handleLoading(false);

      console.log(this.data);
    });
  },

  listenToModalClose: function () {
    this.modal.addEventListener("hide.bs.modal", async (e) => {
      this.handleReset();
    });
  },


  listenToAllTrigger: function () {
    if(!this.allTriggers.length) return;

    this.allTriggers.forEach(trigger => {
      trigger.addEventListener('click', (e) => {
        const infoBoxes = trigger.closest('.info-boxes');
        const items = infoBoxes.querySelectorAll('.info-boxes__item');

        if(infoBoxes === undefined || !items.length) return;

        items.forEach(item => {
          item.classList.remove('d-none');
        })

        trigger.remove();
      })
    })
  },

  /***
   *
   * SETTERS
   *
   */

  setData: async function (e) {
    const button = e.relatedTarget;

    if (button === undefined || button.dataset.nodeid === undefined) return;


    /**
     *
     * response {
     *   success: boolean,
     *   message: string,
     *   result: object
     * }
     * */


    const fetchData = await window.fetch(
      `/api/physicians/${button.dataset.nodeid}`
    );

    console.log(fetchData);

    // Temporarily return sample object
    if (!fetchData.ok) {
      return new Promise((resolve, reject) => {
        setTimeout(() => {
          this.data = this.sampleObject;

          resolve(this.data);
        }, 800);
      });
    }

    const response = await fetchData.json();

    if (response.success === undefined || !response.success) return;

    this.data = response.result;

    return;
  },

  /***
   *
   * HANDLERS
   *
   */

  handleLayout: function () {
    this.modalBody.innerHTML = App.Purify.sanitize(this.data.Body);
  },

  handleLoading: function (currentState = true) {
    this.modal.setAttribute("data-loading", currentState);
  },

  handleReset: function () {
    // RESET IT ALL
    this.data = undefined;
    this.modalBody.innerHTML = '';
  },
};

export default App__infoBoxes;
