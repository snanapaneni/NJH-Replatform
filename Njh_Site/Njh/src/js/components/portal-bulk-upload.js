
import Uppy from '@uppy/core';
import XHRUpload from '@uppy/xhr-upload';
// import ProgressBar from '@uppy/progress-bar';

const App__portalBulkUpload = {

  fileInput: null,
  uploadButton: null,
  uploadProgressBarWrapper: null,
  uploadProgressBarLabel: null,
  uploadProgressBar: null,

  errorMessage: null,
  successMessage: null,

  options: {
    maxFileSize: 0,
    endpoint: '',
    localizationStrings: {},
  },

  uppy: {},

  init: function() {

    App__portalBulkUpload.uploadButton = document.querySelector('[data-hook=portalBulkUpload__uploadButton]');

    if ( App__portalBulkUpload.uploadButton ) {

      App__portalBulkUpload.fileInput                = document.querySelector('[data-hook=portalBulkUpload__fileInput]');
      App__portalBulkUpload.uploadProgressBarWrapper = document.querySelector('[data-hook=portalBulkUpload__uploadProgressBarWrapper]');
      App__portalBulkUpload.uploadProgressBarLabel   = document.querySelector('[data-hook=portalBulkUpload__uploadProgressBarLabel]');
      App__portalBulkUpload.uploadProgressBar        = document.querySelector('[data-hook=portalBulkUpload__uploadProgressBar]');

      App__portalBulkUpload.errorMessage             = document.querySelector('[data-hook=portalBulkUpload__errorMessage]');
      App__portalBulkUpload.successMessage           = document.querySelector('[data-hook=portalBulkUpload__successMessage]');

      App__portalBulkUpload.options.maxFileSize         = parseInt(App__portalBulkUpload.uploadButton.getAttribute('data-max-filesize'));
      App__portalBulkUpload.options.endpoint            = App__portalBulkUpload.uploadButton.getAttribute('data-endpoint');

      let localizationStrings = document.querySelector('[data-hook=portalBulkUpload__localizationStrings]');
      if ( localizationStrings ) {
        App__portalBulkUpload.options.localizationStrings = JSON.parse(localizationStrings.textContent);
      }
      // App__portalBulkUpload.options.endpoint = 'https://xhr-server.herokuapp.com/upload'; // Testing

      if ( App__portalBulkUpload.fileInput ) {

        App__portalBulkUpload.uppy = new Uppy({
          autoProceed: false,
          debug: false,
          restrictions: {
            maxFileSize: App__portalBulkUpload.options.maxFileSize,
            minFileSize: 1,
            maxTotalFileSize: null,
            maxNumberOfFiles: 1,
            minNumberOfFiles: 1,
            allowedFileTypes: ['.csv'],
            requiredMetaFields: [],
          },
          locale: {
            strings: App__portalBulkUpload.options.localizationStrings,
          },
        });


        App__portalBulkUpload.uppy.use(XHRUpload, {
          endpoint: App__portalBulkUpload.options.endpoint,
          // formData: true,
          fieldName: 'files',
        });

        App__portalBulkUpload.fileInput.addEventListener('change', (event) => {
          // console.log('fileInput change event')
          // App__portalBulkUpload.fileInput.value = null;

          App__portalBulkUpload.errorMessage.style.display = 'none';
          App__portalBulkUpload.successMessage.style.display = 'none';

          const files = Array.from(event.target.files)
          // Get the last file in case user tried selecting several
          const lastFile = files.at(-1);

          // Reset uppy to prevent multiple file warning
          App__portalBulkUpload.uppy.reset();

          // Not doing a foreach here since we only support 1 file. Instead just add  the last file selected to (the now reset) Uppy
          // files.forEach((file) => {
            try {
              App__portalBulkUpload.uppy.addFile({
                source: 'file input',
                name: lastFile.name,
                type: lastFile.type,
                data: lastFile,
              });
              App__portalBulkUpload.removeErrors();
            } catch (err) {
              // console.log(err)
              if (err.isRestriction) {
                // handle restrictions
                // console.log('Restriction error:', err)
                App__portalBulkUpload.addErrors( err )
              } else {
                // handle other errors
                // console.error(err)
                App__portalBulkUpload.addErrors( err )
              }
            }
          // })
        });

        App__portalBulkUpload.uppy.on('file-added', (file) => {
          // console.log('file-added event')
          App__portalBulkUpload.uploadButton.removeAttribute('disabled', '');
        })

        App__portalBulkUpload.uppy.on('upload-progress', (file, progress) => {
          // console.log('upload-progress event')
          App__portalBulkUpload.updateProgressBar(file, progress);
        });

        App__portalBulkUpload.uppy.on('upload-success', (file, response) => {
          // console.log('upload-success event')

          if ( file.name ) {
            let portalBulkUpload__successMessageDescription = document.querySelector('[data-hook=portalBulkUpload__successMessageDescription]');
            portalBulkUpload__successMessageDescription.textContent = portalBulkUpload__successMessageDescription.textContent.replace('{0}', file.name);

            let portalBulkUpload__successMessage = document.querySelector('[data-hook=portalBulkUpload__successMessage]');
            portalBulkUpload__successMessage.innerHTML.replace('{0}', file.name);
            portalBulkUpload__successMessage.style.display = 'block';
            portalBulkUpload__successMessage.focus();

            document.querySelector('[data-hook=portalBulkUpload__form]').style.display = 'none';
          }
        });

        App__portalBulkUpload.uppy.on('error', (error) => {
          // console.log('error event')
        });

        App__portalBulkUpload.uppy.on('upload-error', (file, error, response) => {
          // console.log('upload-error event')
          App__portalBulkUpload.uploadButton.setAttribute('disabled', '');
          App__portalBulkUpload.errorMessage.style.display = 'block';
          App__portalBulkUpload.errorMessage.focus();
        });

        App__portalBulkUpload.uppy.on('complete', () => {
          // App__portalBulkUpload.fileInput.value = null
        });

        App__portalBulkUpload.uploadButton.addEventListener('click', (e) => {
          e.preventDefault();
          App__portalBulkUpload.fileInput.setAttribute('disabled', '');
          App__portalBulkUpload.uploadButton.setAttribute('disabled', '');
          App__portalBulkUpload.uppy.upload();
        });

      }

    } else {
      return false;
    }
  },

  updateProgressBar: function (file, progress) {
    let currentPercentage = Math.round( (progress.bytesUploaded / progress.bytesTotal) * 100 );

    App__portalBulkUpload.uploadProgressBarWrapper.style.display = 'block';

    App__portalBulkUpload.uploadProgressBarLabel.textContent = currentPercentage + '% uploaded';

    App__portalBulkUpload.uploadProgressBar.setAttribute('aria-valuenow', currentPercentage);
    App__portalBulkUpload.uploadProgressBar.style.width = currentPercentage + '%';
  },

  addErrors: function( err ) {
    let errorMarkup = document.createElement('div');
    errorMarkup.classList.add('invalid-feedback');
    errorMarkup.setAttribute('id', 'portal-bulk-upload-error');
    errorMarkup.textContent = err;

    App__portalBulkUpload.fileInput.setAttribute('aria-describedby', 'portal-bulk-upload-error');
    App__portalBulkUpload.fileInput.classList.add('is-invalid');

    // Inject error
    App__portalBulkUpload.fileInput.after(errorMarkup);

    App__portalBulkUpload.uppy.reset();
    App__portalBulkUpload.fileInput.removeAttribute('disabled', '');
    App__portalBulkUpload.uploadButton.setAttribute('disabled', 'disabled');


    // App__portalBulkUpload.uploadButton.style.display = 'none';
  },

  removeErrors: function() {
    let errorMarkup = document.querySelector('#portal-bulk-upload-error');
    if (errorMarkup) {
      errorMarkup.remove();
      App__portalBulkUpload.fileInput.classList.remove('is-invalid');
      App__portalBulkUpload.fileInput.removeAttribute('aria-describedby');
      App__portalBulkUpload.uploadButton.removeAttribute('disabled');
      App__portalBulkUpload.uploadButton.style.display = 'inline';
    }
  }


}

export default App__portalBulkUpload;
