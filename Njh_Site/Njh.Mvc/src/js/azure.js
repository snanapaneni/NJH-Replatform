import '../styles/azure.scss';

// Inject copyright year
document.querySelector('[data-hook=trbotAzureCopyrightYear]').innerHTML = new Date().getFullYear();

// function disableSubmitOnEnter(input) {
//   input.addEventListener("keypress", function (evt) {
//     if (evt.keyCode === 13 || evt.which === 13) {
//       evt.preventDefault();
//       evt.stopImmediatePropagation();
//       return false;
//     }
//   });
// }

/* ===================================================================================
   Password error message override
   Select all error containers for password fields and adjust error message.
   Currently the error message repeats the full password policy - which adds
   lots of noise. This replaces the error message with a simpler one.
   NOTE: This may need to be disabled if password error messages are triggered
   by errors unrelated to the password format policy (i.e. previously used, etc).
 */
const passwordErrorMessageCallback = function(mutationsList, observer) {
  for ( const mutation of mutationsList ) {
    if ( mutation.type === 'childList' ) {
      let message = 'Enter a valid password.';
      if ( mutation.target.innerHTML != message ) {
        mutation.target.innerHTML = message;
      }
    }
  }
};
document.querySelectorAll('input[type=password]').forEach((passwordField) => {
  let passwordErrorElement = passwordField.previousElementSibling;
  const observer = new MutationObserver( passwordErrorMessageCallback );
  observer.observe(passwordErrorElement, { childList: true });
});
/* End password error message override.
=================================================================================== */


// Remove placeholder text from inputs
let formFieldSelectors = [
  'input[type=email]',
  'input[type=password]',
  'input[type=tel]',
  'input[type=text]',
];
document.querySelectorAll(formFieldSelectors.join()).forEach((formField) => {
  formField.placeholder = '';
  // disableSubmitOnEnter(input);
});


// Add required indicators to specific labels
let formFieldLabelSelectors = [
  'label[for=email]',
  'label[for=email_ver_input]',
  'label[for=emailVerificationCode]',
  'label[for=givenName]',
  'label[for=surname]',
  'label[for=newEmailAddress]',
  'label[for=newPassword]',
  'label[for=oldPassword]',
  'label[for=password]',
  'label[for=reenterPassword]',
  'label[for=signInName]',
];
document.querySelectorAll(formFieldLabelSelectors.join()).forEach((formFieldLabel) => {
  formFieldLabel.innerHTML = formFieldLabel.innerHTML.replace(/\*/g, '') + ' <span class="visually-hidden"> required</span>';
  formFieldLabel.classList.add('is-required');
});

// Remove asterisk from login intro
let localAccountFormHeader = document.querySelector('#localAccountForm .intro h2');
if ( localAccountFormHeader ) {
  localAccountFormHeader.innerHTML = localAccountFormHeader.innerHTML.replace(/\*/g, '');
}


// Toggle Password Visibility
function makePwdToggler( pwd ) {
  // Create show-password checkbox
  let checkbox = document.createElement("input");
  checkbox.setAttribute("type", "checkbox");
  let id = pwd.id + "-toggler";
  checkbox.setAttribute("id", id);

  let label = document.createElement("label");
  label.setAttribute("for", id);
  label.classList.add("password-toggler-label");
  let span = document.createElement("span");
  span.classList.add("visually-hidden");
  span.appendChild(document.createTextNode("show password"));
  label.appendChild(span);

  let div = document.createElement("div");
  div.classList.add("password-toggle-controls");
  div.appendChild(checkbox);
  div.appendChild(label);

  // Add show-password checkbox under password input
  pwd.insertAdjacentElement("afterend", div);

  // Add toggle password callback
  function toggle() {
    if ( pwd.type === "password" ) {
      pwd.type = "text";
    } else {
      pwd.type = "password";
    }
  }
  checkbox.onclick = toggle;
  // For non-mouse usage
  checkbox.onkeydown = toggle;
}

function setupPwdTogglers() {
  let pwdInputs = document.querySelectorAll("input[type=password]");

  if ( pwdInputs ) {
    for (let i = 0; i < pwdInputs.length; i++) {
      makePwdToggler(pwdInputs[i]);
    }
  }
}

setupPwdTogglers();

// Add aria-invalid to invalid fields
let formSubmit = document.querySelector("button[type=submit]");
if ( formSubmit ) {
  formSubmit.addEventListener("click", () => {
    let invalidFields = Array.from(document.querySelectorAll(".highlightError"));

    if ( invalidFields ) {
      invalidFields.map((field) => {
        field.setAttribute("aria-invalid", true);
      });

      setTimeout(() => {
        document.querySelector('.pageLevel[aria-hidden=false]').scrollIntoView();
      }, 300);
    }
  });
}

// Inject password requirements
// Azure injects an object at window.SA_FIELDS with validation data and error messages.
// Here, we plumb that and extract the password requirements so we can display them near the New Password field
// This should trigger any time there is a "New Password" field - so on new account signup and password reset
if ( document.getElementById("newPassword") ) {
  const passwordFields = window.SA_FIELDS.AttributeFields.filter((field) => Object.values(field).includes("Password")).find((field) => field.INPUT_VALIDATION);
  if ( passwordFields ) {
    // Build up an html string to inject
    let passwordPolicy = '';

    passwordPolicy = '<p>' + passwordFields.INPUT_VALIDATION.PREDICATE_GROUPS[0].PREDICATES[0].HELP_TEXT + '</p>';

    if ( passwordFields.INPUT_VALIDATION.PREDICATE_GROUPS[1].HELP_TEXT ) {
      passwordPolicy = passwordPolicy + '<p>' + passwordFields.INPUT_VALIDATION.PREDICATE_GROUPS[1].HELP_TEXT + '</p>';
      if ( passwordFields.INPUT_VALIDATION.PREDICATE_GROUPS[1].PREDICATES ) {
        if ( passwordFields.INPUT_VALIDATION.PREDICATE_GROUPS[1].PREDICATES ) {

          // Format requirements (comma separated)
              // let passwordPolicyCharacterRequirements = [];
              // passwordFields.INPUT_VALIDATION.PREDICATE_GROUPS[1].PREDICATES.forEach(function(rule) {
              //   passwordPolicyCharacterRequirements.push(rule.HELP_TEXT);
              // });
              // passwordPolicy = passwordPolicy + '<br>' + passwordPolicyCharacterRequirements.join(', ');

          // Format requirements (unordered list)
          passwordPolicy = passwordPolicy + '<ul>';
          passwordFields.INPUT_VALIDATION.PREDICATE_GROUPS[1].PREDICATES.forEach(function(rule) {
            passwordPolicy = passwordPolicy + '<li>' + rule.HELP_TEXT + '</li>';
          });
          passwordPolicy = passwordPolicy + '</ul>';
        }
      }
    }

    let passwordPolicy_div = document.createElement('div');
    passwordPolicy_div.classList.add('password-policy');
    passwordPolicy_div.innerHTML = passwordPolicy;

    document.getElementById('newPassword_label').insertAdjacentElement('afterend', passwordPolicy_div);
  }
}


const rememberMe = document.querySelector('.rememberMe');
if ( rememberMe ) {
  rememberMe.classList.add('form-check');
  rememberMe.querySelector('input').classList.add('form-check-input');
  rememberMe.querySelector('label').classList.add('form-check-label');
}

// Inject required field instructions
document.querySelectorAll('.trbot-azure .intro').forEach((azureIntro) => {
  azureIntro.insertAdjacentHTML('beforeend', '<p class="form-instructions">Fields marked with an asterisk <span class="form-instructions__required-marker"></span> are required.</p>');
});

// Signup Email Verification
// After success message is injected add a class so we cna adjust styles (have to do this due to how markup is presented)
let emailVerificationControl_success_message = document.querySelector('#emailVerificationControl_success_message');
if ( emailVerificationControl_success_message ) {
  emailVerificationControl_success_message.addEventListener('DOMNodeInserted', function (e) {
    emailVerificationControl_success_message.classList.add('is-visible');
  }, false);
}


// Adjust the header markup and inject custom text
let loginFormIntro = document.querySelector('form#localAccountForm .intro');
if ( loginFormIntro ) {
  let loginFormIntro__header = document.createElement('h1');
  loginFormIntro__header.textContent = 'Account Sign-In';
  let loginFormIntro__instructions = document.createElement('p');
  loginFormIntro__instructions.textContent = 'Sign in with your existing account to get access to TRBOT resources and programs.';

  loginFormIntro.querySelector('h2').remove();
  loginFormIntro.prepend(loginFormIntro__header, loginFormIntro__instructions);
}
