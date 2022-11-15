const App__formBuilder = (() => {
  const forms = [
    ...document.querySelectorAll('.form-builder__form[data-ajax="true"]'),
  ];
  return {
    init: () => {
      forms.forEach((formInstance) => {
        // Add label for recaptcha field
        let recaptchaLabel = document.createElement('label');
        recaptchaLabel.setAttribute('for', 'g-recaptcha-response');
        recaptchaLabel.setAttribute('aria-hidden', 'true');
        recaptchaLabel.classList.add('visually-hidden');
        recaptchaLabel.innerHTML = 'Recaptcha Response';

        // Make sure the recaptcha response field has rendered before trying to inject the label before it
        let recaptchaResponseField = formInstance.querySelector('textarea[name=g-recaptcha-response]');
        if ( recaptchaResponseField ) {
          recaptchaResponseField.before(recaptchaLabel);
        }

        window.addEventListener("submit", (e) => {
          const form = e.target;
          if (!form.hasAttribute("data-ajax")) {
            return;
          }
          let container = document.getElementById(form.dataset.formContainer);

          e.preventDefault();
          e.stopImmediatePropagation();

          const xhr = new XMLHttpRequest();
          xhr.open(form.method, form.action);

          xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
              const data = xhr.response;

              try {
                const jsonObject = JSON.parse(data);
                if (jsonObject.redirectTo) {
                  location.href = jsonObject.redirectTo;
                }
              } catch (e) {
                const prefix = "reveals-id";
                if (data.includes(prefix)) {
                  const revealID = data.substring(
                    data.lastIndexOf(prefix) + prefix.length,
                    data.lastIndexOf("<")
                  );
                  const revealingNode = document.querySelector(revealID);
                  if (revealingNode) {
                    revealingNode.classList.add("revealed-by-form-show");
                    container.parentNode.removeChild(container);
                    revealingNode.focus();
                    revealingNode.scrollTop = 100;
                  }
                } else {
                  container.innerHTML = data;
                  let captcha = document.querySelector(".g-recaptcha");
                  let captchaError = document.querySelector(".feedback-message a[href*='AccessibleRecaptcha']");

                  const firstFocusableLink = container.querySelector(".feedback-message a");
                  if (firstFocusableLink) {
                    firstFocusableLink.focus();
                  } else {
                    container.focus();
                  }

                  if (captcha) {
                    grecaptcha.render(captcha, {
                      sitekey: captcha.dataset.sitekey,
                    });

                    //const captchaErrorDiv = document.createElement("div");
                    //captchaErrorDiv.classList.add("form-error");

                    //if (captchaError) {
                    //  captchaErrorDiv.innerText = captchaError.text;
                    //} else {
                    //  captchaErrorDiv.innerText = "reCaptcha is required";
                    //}

                    //captcha.appendChild(captchaErrorDiv);
                  }
                }
              }
            }

            let errorLinks = document.querySelectorAll(".feedback-message a");
            if (errorLinks.length > 0) {
              const header = document.querySelector(".app-header");
              const headerHeight = header.offsetHeight + 35;

              errorLinks.forEach((link) => {
                link.addEventListener("click", function (e) {
                  e.preventDefault();
                  let href = this.getAttribute("href");
                  let result = href.slice(1);
                  let target = document.getElementById(result);
                  let pos = target.offsetTop - target.scrollTop - headerHeight;

                  target.focus();
                  window.scrollTo(0, pos);
                });
              });
            }
          };
          xhr.send(new FormData(form));

          container.scrollIntoView();
        });

      });
    },
  };
})();

export default App__formBuilder;
