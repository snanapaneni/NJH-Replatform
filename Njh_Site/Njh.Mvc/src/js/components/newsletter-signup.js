
const App__newsletterSignup = {

  init: function() {
    document.querySelectorAll('[data-hook=newsletterSignup__form]').forEach ( function( newsletterSignupForm ) {
      // Attach event listeners
      newsletterSignupForm.addEventListener('submit', function(e) {
        e.preventDefault()
        App__newsletterSignup.submit( newsletterSignupForm );
      })

      // Add label for recaptcha field
      let recaptchaLabel = document.createElement('label');
      recaptchaLabel.setAttribute('for', 'g-recaptcha-response');
      recaptchaLabel.setAttribute('aria-hidden', 'true');
      recaptchaLabel.classList.add('visually-hidden');
      recaptchaLabel.innerHTML = 'Recaptcha Response';
      newsletterSignupForm.querySelector('textarea[name=g-recaptcha-response]').before(recaptchaLabel);
    });
  },

  submit: function( form ) {
    const formData = new FormData( form );
    const currentNewsletterSignupComponent = form.closest('[data-hook=newsletterSignup]');

    // Disable submit button to prevent duplicate submissions
    currentNewsletterSignupComponent.querySelector('[type=submit]').disabled = 'true';

    // Clear all error states on fields
    currentNewsletterSignupComponent.querySelectorAll('input.is-invalid').forEach(function(field){
      field.classList.remove('is-invalid');
      field.removeAttribute('aria-invalid');
      field.removeAttribute('aria-describedby');
    });
    currentNewsletterSignupComponent.querySelectorAll('.invalid-feedback').forEach(function(error){
      error.remove();
    });

    // Convert formData to JSON
    let formDataObject = {};
    formData.forEach(function(value, key){
        formDataObject[key] = value;
    });
    formDataObject.captchaResponse = formDataObject['g-recaptcha-response'];
    delete formDataObject['g-recaptcha-response'];

    const options = {
      method: 'POST',
      body: JSON.stringify(formDataObject),
      headers: {
        'Content-Type': 'application/json'
      }
    }

    fetch( form.action, options )
      // .then(function(response) {
      //   if (!response.ok) {
      //     throw Error(response.statusText);
      //   }
      //   return response;
      // })
      .then(response => response.json())
      .then(function(response) {


        // Success!
        if ( response.Valid ) {

          let newsletterSignup__successMessage = currentNewsletterSignupComponent.querySelector('[data-hook=feedbackMessage]');
          newsletterSignup__successMessage.style.display = 'block';
          form.style.display = 'none';
          newsletterSignup__successMessage.focus();

        } else {

          currentNewsletterSignupComponent.querySelector('[type=submit]').removeAttribute('disabled');

          // console.log(response);
          for (const [fieldName, errorMessage] of Object.entries(response.errors)) {
            // let inputLabel = currentNewsletterSignupComponent.querySelector('label[for=' + fieldName + ']');
            let inputField = currentNewsletterSignupComponent.querySelector('input[name=' + fieldName + ']');

            // Build error markup
            let inputFieldError = document.createElement('p');
            inputFieldError.classList.add('invalid-feedback');
            inputFieldError.textContent = errorMessage;

            // If is input field
            if ( inputField ) {
              inputFieldError.id = inputField.id + '-error';
              inputField.setAttribute('aria-invalid' , 'true');
              inputField.setAttribute('aria-describedby', inputField.id + '-error');
              inputField.classList.add('is-invalid');
              // Inject error
              inputField.after(inputFieldError);
            }

            // If is Google ReCAPTCHA
            if ( fieldName === 'CaptchaResponse' ) {
              inputFieldError.style.display = 'block';
              // Inject error
               currentNewsletterSignupComponent.querySelector('.g-recaptcha').after(inputFieldError);
               // Add is-invalid class on wrapper so can be focused if needed
               let newsletterSignup__recaptchaWrapper = currentNewsletterSignupComponent.querySelector('[data-hook=newsletterSignup__recaptchaWrapper]');
               newsletterSignup__recaptchaWrapper.classList.add('is-invalid');

            }

            // Focus on first error
            currentNewsletterSignupComponent.querySelector('.is-invalid').focus();
          }

        }
      })
      .catch(function(error) {
        console.log('errors...');
        // console.log(error);
      });

  }

}

export default App__newsletterSignup;
