const App__moneris = {
  tokenEndpoint: false,
  paymentForm: {},

  responseCodes: {
    '001': 'Successful creation of temporary token',
    '940': 'Invalid profile id (on tokenization request)',
    '941': 'Error generating token',
    '942': 'Invalid Profile ID, or source URL',
    '943': 'Invalid credit card number',
    '944': 'Invalid expiration date (format as MMYY)',
    '945': 'Invalid CVD security code (should be 3 or 4 digits)'
  },

  init: function () {
    if ( document.querySelector('[data-hook=monerisPaymentForm]') ) {

      window.addEventListener ('message', App__moneris.responseMessage, false);
      App__moneris.paymentForm = document.querySelector('[data-hook=monerisPaymentForm]');
      App__moneris.paymentForm.addEventListener('submit', App__moneris.handlePaymentFormSubmit );
      App__moneris.tokenEndpoint = App__moneris.paymentForm.dataset.monerisTokenEndpoint;

      let initialSelectedpaymentMethod = document.querySelector('[data-hook=paymentMethodRadio]:checked');
      if ( initialSelectedpaymentMethod ) {
        if (initialSelectedpaymentMethod.value === 'credit-card') {
          document.querySelector('[data-hook=paymentMethodDetails__creditCard]').style.display = 'block';
        } else if (paymentMethodRadio.value === 'invoice') {
          document.querySelector('[data-hook=paymentMethodDetails__invoice]').style.display = "block";
        }
      }


      let paymentMethodRadioInputs = document.querySelectorAll('[data-hook=paymentMethodRadio]');
      paymentMethodRadioInputs.forEach( paymentMethodRadio => {
        paymentMethodRadio.addEventListener('click', (e) => {

          document.querySelectorAll('[data-hook*=paymentMethodDetails__]').forEach( paymentMethodDetail => {
            paymentMethodDetail.style.display = 'none';
          });

          if (paymentMethodRadio.value === 'credit-card') {
            document.querySelector('[data-hook=paymentMethodDetails__creditCard]').style.display = 'block';
          } else if (paymentMethodRadio.value === 'invoice') {
            document.querySelector('[data-hook=paymentMethodDetails__invoice]').style.display = "block";
          }

        });
      });
    }
  },

  responseMessage: function(e) {
    if ( e.data ) {
      let response = eval('(' + e.data + ')');
      if ( response.dataKey ) {
        // Update hidden field with moneris token and submit form
        document.querySelector('[data-hook=monerisTokenInput]').value = response.dataKey;
      } else {
        // Build error messages and inject into DOM
        let monerisErrors = [];
        response.responseCode.forEach( function (errorCode) {
          let monerisError = {
            code: errorCode,
            message: App__moneris.responseCodes[errorCode],
            link: '#moneris-iframe',
          }
          monerisErrors.push(monerisError);
        });

        // Reverse order of error messages received from Moneris to make them match order of their form
        monerisErrors.reverse();

        // Inject JSON into hidden field for backend to parse and display
        document.querySelector('[data-hook=monerisPaymentErrors]').value = JSON.stringify(monerisErrors);
      }

      App__moneris.paymentForm.submit();
    }
  },

  handlePaymentFormSubmit: function ( event ) {
      if (document.querySelector('[data-hook=paymentMethodRadio]')) {
          if ('credit-card' === document.querySelector('[data-hook=paymentMethodRadio]:checked').value) {
              event.preventDefault();
              let moneris = document.querySelector('[data-hook=monerisFrameWrapper] iframe').contentWindow;
              if (App.moneris.tokenEndpoint) {
                  moneris.postMessage('tokenize', App.moneris.tokenEndpoint);
              }
          }
      }
      else {
          event.preventDefault();
          let moneris = document.querySelector('[data-hook=monerisFrameWrapper] iframe').contentWindow;
          if (App.moneris.tokenEndpoint) {
              moneris.postMessage('tokenize', App.moneris.tokenEndpoint);
          }
      }
  }

}

export default App__moneris;
