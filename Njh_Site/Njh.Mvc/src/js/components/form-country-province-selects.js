const App__formCountryProvinceSelects = {

  countryCodesRequiringState: ['CA', 'US'],

  countrySelectFieldWrapper: document.querySelector('[data-hook=countrySelectWrapper]'),
  countrySelectField: document.querySelector('[data-hook=countrySelectWrapper] select'),
  additionalAddressFieldsWrapper: document.querySelector('[data-hook=additionalAddressFieldsWrapper]'),

  provinceSelectFieldWrapper: document.querySelector('[data-hook=provinceSelectWrapper]'),
  provinceSelectField:   document.querySelector('[data-hook=provinceSelectWrapper] select'),
  provinceSelectLabel:   document.querySelector('[data-hook=provinceSelectWrapper] label'),

  postalCodeInputField: document.querySelector('[data-hook=postalCodeInputWrapper] input'),
  postalCodeInputLabel: document.querySelector('[data-hook=postalCodeInputWrapper] label'),

  init: function () {

    if ( App__formCountryProvinceSelects.countrySelectField ) {

      // If no country selected, default to Canada
      if ( ! App__formCountryProvinceSelects.countrySelectField.value ) {
        App__formCountryProvinceSelects.countrySelectField.value = 'CA';
      }

      // If Province/State selected at page load, store it in a data attribute so we can re-select it after the options are built
      if ( App__formCountryProvinceSelects.provinceSelectField.value ) {
        App__formCountryProvinceSelects.provinceSelectFieldWrapper.setAttribute('data-selected', App__formCountryProvinceSelects.provinceSelectField.value);
      }

      // Adjust field visibility based on country selected when page loads
      App__formCountryProvinceSelects.adjustFieldVisibility( App__formCountryProvinceSelects.countrySelectField.value );

      // Handle country select change
      App__formCountryProvinceSelects.countrySelectField.addEventListener('change', (event) => {
        App__formCountryProvinceSelects.adjustFieldVisibility( event.target.value );
        // App__formCountryProvinceSelects.countrySelectField.setAttribute('data-selected', event.target.value);
      });

      // Handle province/state select change
      // App__formCountryProvinceSelects.provinceSelectField.addEventListener('change', (event) => {
      //   App__formCountryProvinceSelects.provinceSelectField.setAttribute('data-selected', event.target.value);
      // });

    }

  },

  adjustFieldVisibility: function ( countryCode ) {
    if ( countryCode ) {
      if ( App__formCountryProvinceSelects.countryCodesRequiringState.includes( countryCode ) ) {

        App__formCountryProvinceSelects.populateProvinceOptions( countryCode );

        App__formCountryProvinceSelects.provinceSelectFieldWrapper.style.display = 'block';
        App__formCountryProvinceSelects.provinceSelectLabel.classList.add('is-required');
        App__formCountryProvinceSelects.postalCodeInputLabel.classList.add('is-required');
      } else {
        //App__formCountryProvinceSelects.resetProvinceOptions();
        App__formCountryProvinceSelects.provinceSelectFieldWrapper.style.display = 'none';
        App__formCountryProvinceSelects.postalCodeInputLabel.classList.remove('is-required');
        App__formCountryProvinceSelects.provinceSelectLabel.classList.remove('is-required');
      }

      if ( App__formCountryProvinceSelects.additionalAddressFieldsWrapper ) {
        App__formCountryProvinceSelects.additionalAddressFieldsWrapper.style.display = 'block';
      }

    } else {

      if ( App__formCountryProvinceSelects.additionalAddressFieldsWrapper ) {
        App__formCountryProvinceSelects.additionalAddressFieldsWrapper.style.display = 'none';
      }

    }
  },

  populateProvinceOptions: function ( countryCode ) {

    // Fetch state options based on selected country
    fetch( '/api/CountryStates?countryCode=' + countryCode )
      .then(response => response.json())
      .then(data => {
        if ( data.hasStates ) {

          if ( App__formCountryProvinceSelects.provinceSelectField ) {
            // Build options markup and populate select menu
            let selectOptions = '<option value="">Please select one</option>';
            data.states.forEach(state => {
              selectOptions = selectOptions + '<option value="' + state.stateCode + '">' + state.displayName + '</option>';
            });
            App__formCountryProvinceSelects.provinceSelectField.innerHTML = selectOptions;

            // Try to set the Province select value to the data-selected if it exists
            if ( App__formCountryProvinceSelects.provinceSelectFieldWrapper.dataset.selected ) {
              App__formCountryProvinceSelects.provinceSelectField.value = App__formCountryProvinceSelects.provinceSelectFieldWrapper.dataset.selected;
              // Clear data attribute so first option will be selected on subsequent changes via else statment below
               App__formCountryProvinceSelects.provinceSelectFieldWrapper.removeAttribute('data-selected');

            } else {
              // Select the first Province option
              App__formCountryProvinceSelects.provinceSelectField.querySelector('option:first-child').setAttribute('selected', 'selected');
            }
          }

          App__formCountryProvinceSelects.provinceSelectLabel.innerHTML = 'Province';
          if ( countryCode === 'US' ) {
            App__formCountryProvinceSelects.provinceSelectLabel.innerHTML = 'State';
          }

          App__formCountryProvinceSelects.provinceSelectFieldWrapper.style.display = 'block';
          App__formCountryProvinceSelects.postalCodeInputLabel.classList.add('is-required');


        } else {
          App__formCountryProvinceSelects.provinceSelectFieldWrapper.style.display = 'none';
          App__formCountryProvinceSelects.postalCodeInputLabel.classList.remove('is-required');

        }
    });
  }
}

export default App__formCountryProvinceSelects
