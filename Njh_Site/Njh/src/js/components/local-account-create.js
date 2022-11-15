import accessibleAutocomplete from 'accessible-autocomplete';

const App__localAccountCreate = {

  resetButton: null,

  companyAutocompleteWrapper: null,
  companyAddress: null,
  companyFormStateField: null,
  existingCompanyKeyField: null,

  init: function () {

    if ( ! document.querySelector('[data-hook=localAccountCreateForm]') ) {
      return false;
    }

    // Cache DOM objects
    App__localAccountCreate.localAccountCreateForm = document.querySelector('[data-hook=localAccountCreateForm]');
    App__localAccountCreate.existingCompanyWrapper = document.querySelector('[data-hook=localAccountCreateForm__existingCompanyWrapper]');
    App__localAccountCreate.newCompanyWrapper = document.querySelector('[data-hook=localAccountCreateForm__newCompanyWrapper]');
    App__localAccountCreate.resetButton = document.querySelector('[data-hook=localAccountCreateForm__resetButton]');
    App__localAccountCreate.companyAutocompleteWrapper = document.querySelector('[data-hook=localAccountCreateForm__companyAutocompleteWrapper]');
    App__localAccountCreate.companyAddress = document.querySelector('[data-hook=localAccountCreateForm__companyAddress]');
    App__localAccountCreate.companyFormStateField = document.querySelector('[data-hook=localAccountCreateForm__companyFormStateField]');
    App__localAccountCreate.existingCompanyKeyField = document.querySelector('[data-hook=localAccountCreateForm__existingCompanyKeyField]');
    App__localAccountCreate.newCompanyCountrySelectField = document.querySelector('[data-hook=countrySelectWrapper] select');
    App__localAccountCreate.newCompanyProvinceSelectlabel = document.querySelector('[data-hook=provinceSelectWrapper] label');
    App__localAccountCreate.newCompanyProvinceSelectField = document.querySelector('[data-hook=provinceSelectWrapper] select');

    if ( App__localAccountCreate.companyAutocompleteWrapper ) {

      const addNewLabel = App__localAccountCreate.companyAutocompleteWrapper.dataset.newLabel;
      const allowAddNew = (App__localAccountCreate.companyAutocompleteWrapper.dataset.allowAddNew.toLowerCase() == 'true') ? true : false;

      accessibleAutocomplete({
        element: App__localAccountCreate.companyAutocompleteWrapper,
        id: 'ExistingCompany', // To match it to the existing <label>.
        name: 'ExistingCompany',
        minLength: 3,
        displayMenu: 'overlay',
        showNoOptionsFound: false,
        onConfirm: ( (confirmedOption) => {
          if ( typeof confirmedOption != "undefined" ) {
            if ( confirmedOption.name != addNewLabel ) {
              App__localAccountCreate.existingCompanyKeyField.value = confirmedOption.key;
              if ( confirmedOption.address && confirmedOption.city ) {
                App__localAccountCreate.companyAddress.textContent = confirmedOption.address + ', ' + confirmedOption.city;
              } else {
                App__localAccountCreate.companyAddress.textContent = '';
              }
            } else {

              App__localAccountCreate.adjustFormState( 'New' );

            }
          }
        }),
        templates: {
          inputValue: (selectedOption) => {
            if (selectedOption) {
              return selectedOption.name
            }
          },
          suggestion: (suggestedOption) => {
            let option =      '<span class="autocomplete-option__content" style="display:flex; flex-direction:column;">';
            option = option +     '<span class="autocomplete-option__company" style="font-weight:bold;">' + suggestedOption.name + '</span>';
              if ( suggestedOption.address || suggestedOption.city ) {
                option = option + '<span>';
                  option = option + '<span class="autocomplete-option__address">' + suggestedOption.address + '</span>, ';
                  option = option + '<span class="autocomplete-option__city">' + suggestedOption.city + '</span>';
                option = option + '</span>';
              }
            option = option + '</span>';
            return option;
          }
        },
        source: function(query, populateResults) {
          // If query.length is 3, make call to api server
          if ( query.length === 3 ) {

            fetch( '/api/search/account?searchTerm=' + query )
              .then(response => response.json())
              .then(results => {
                if ( results.accountKeys.length ) {
                  // Cache these in local storage for future searches over 3 characters
                  localStorage.setItem('companyOptions', JSON.stringify(results.accountKeys));

                  if ( allowAddNew ) {
                    results.accountKeys.push({
                      "key": "",
                      "name": addNewLabel,
                      "address": "",
                      "city": ""
                    });
                  }

                  populateResults( results.accountKeys );

                } else {

                  if ( allowAddNew ) {
                    results.accountKeys.push({
                      "key": "",
                      "name": addNewLabel,
                      "address": "",
                      "city": ""
                    });
                  }

                  populateResults( results.accountKeys );
                }
            });
          // Else is > 3, so use cached array instead of another network request
          } else if ( query.length > 3 ) {
            // Get cached results from local storage
            let results = localStorage.getItem('companyOptions');
            if ( results ) {
              results = JSON.parse(results);
              const filteredResults = results.filter(result => result.name.toLowerCase().indexOf(query.toLowerCase()) !== -1);

              if ( allowAddNew ) {
                filteredResults.push({
                    "key": "",
                    "name": addNewLabel,
                    "address": "",
                    "city": ""
                });
              }

              populateResults( filteredResults );
            }
          }

          // Event Listener: Add New company option (should be the last option we added above)
          App__localAccountCreate.companyAutocompleteWrapper.addEventListener('click', (e) => {
              if ( !e.target.matches('li:last-child') ) {
                return false;
              }
              e.preventDefault();
              e.stopPropagation();
              App__localAccountCreate.resetFormFields();
              App__localAccountCreate.adjustFormState( 'New' );

              /* The Country and State/Province fields are mostly handled by App__formCountryProvinceSelects,
               * but since we are resetting them in some cases, we need to replicate some of that logic here
               */
              App__localAccountCreate.newCompanyCountrySelectField.value = 'CA'; // Default to Canada
              App__localAccountCreate.newCompanyCountrySelectField.dispatchEvent(new Event('change'));
              App__localAccountCreate.newCompanyProvinceSelectlabel.classList.add('is-required'); // Reflect required status on State/Province select
          });

        }
      });
    }

    // Set initial state of form
    if ( ! App__localAccountCreate.companyFormStateField.value ) {
      App__localAccountCreate.companyFormStateField.value = 'Existing';
    }

    // If Autocomplete field has errors
    if ( App__localAccountCreate.companyAutocompleteWrapper
         && App__localAccountCreate.companyAutocompleteWrapper.dataset.error ) {
      // Add class to wrapper element so we can apply proper error styles (the js lib overwrites all classes on the input so have to go this route)
      App__localAccountCreate.companyAutocompleteWrapper.querySelector('.autocomplete__wrapper').classList.add('autocomplete__wrapper--has-error');

      // Create and inject the error feedback below the autocomplete input field
      let existingCompanyFieldErrorFeedback = document.createElement('p');
      existingCompanyFieldErrorFeedback.setAttribute('id', 'ExistingCompany');
      existingCompanyFieldErrorFeedback.classList.add('invalid-feedback');
      existingCompanyFieldErrorFeedback.textContent = App__localAccountCreate.companyAutocompleteWrapper.dataset.error;
      App__localAccountCreate.companyAutocompleteWrapper.querySelector('input').insertAdjacentElement('afterend', existingCompanyFieldErrorFeedback);
    }

    App__localAccountCreate.adjustFormState( App__localAccountCreate.companyFormStateField.value );

    App__localAccountCreate.localAccountCreateForm.style.display = 'block';

    // Event Listener: Form Submit
    App__localAccountCreate.localAccountCreateForm.addEventListener('submit', (e) => {
      let formSubmitButton = App__localAccountCreate.localAccountCreateForm.querySelector('[type=submit]');
      formSubmitButton.textContent = 'Submitting...';
      formSubmitButton.setAttribute('disabled', 'disabled');
    });


    // Event Listener: Reset button click
    if ( App__localAccountCreate.resetButton ) {
      App__localAccountCreate.resetButton.addEventListener('click', (e) => {
        e.preventDefault();
        App__localAccountCreate.resetFormFields();
        App__localAccountCreate.adjustFormState( 'Existing' );
      });
    }
  },

  resetFormFields: function () {
    App__localAccountCreate.existingCompanyKeyField.value = '' ;
    App__localAccountCreate.companyAutocompleteWrapper.querySelector('.autocomplete__wrapper').classList.remove('autocomplete__wrapper--has-error');
    document.querySelectorAll('input[type=text]').forEach( (field) => { field.value = '' ; });
    document.querySelectorAll('select').forEach( (field) => { field.selectedIndex = 0; });

    if ( document.querySelector('[data-hook=feedbackMessage]') ) {
      document.querySelector('[data-hook=feedbackMessage]').remove();
    }
    document.querySelectorAll('.invalid-feedback').forEach( (element) => {
      element.remove();
    });
    document.querySelectorAll('.is-invalid').forEach( (element) => {
      element.removeAttribute('aria-describedby');
      element.classList.remove('is-invalid', 'input-validation-error')
    });
  },

  adjustFormState: function ( state ) {
    App__localAccountCreate.companyFormStateField.value = state;
    if ( state == 'Existing' ) {
      App__localAccountCreate.newCompanyWrapper.style.display = 'none';
      App__localAccountCreate.existingCompanyWrapper.style.display = 'block';
      App__localAccountCreate.resetButton.style.display = 'none';
      App__localAccountCreate.companyAddress.textContent = '';

      // If company was chosen fill in the various bits.
      // Typically this happens on invalid form submission where we need to repopulate the autocomplete field
      if ( App__localAccountCreate.existingCompanyKeyField.value ) {
        let companyInfo = App__localAccountCreate.existingCompanyKeyField.value.split('||');
        App__localAccountCreate.companyAutocompleteWrapper.querySelector('input').value = companyInfo[0];
        // Inject address info below company name field
        App__localAccountCreate.companyAddress.textContent = companyInfo[1] + ', ' + companyInfo[2];
      }
    } else if ( state == 'New') {
      App__localAccountCreate.newCompanyWrapper.style.display = 'block';
      App__localAccountCreate.existingCompanyWrapper.style.display = 'none';
      App__localAccountCreate.resetButton.style.display = 'inline';
    }

    // Hide autocomplete ui in case it fired
    setTimeout(() => {
      App__localAccountCreate.companyAutocompleteWrapper.querySelector('.autocomplete__menu').classList.remove('autocomplete__menu--visible');
      App__localAccountCreate.companyAutocompleteWrapper.querySelector('.autocomplete__menu').classList.add('autocomplete__menu--hidden');
    }, "250");
  }

}

export default App__localAccountCreate;
