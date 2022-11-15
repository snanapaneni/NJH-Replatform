
const App__feedbackMessage = {

  init: function() {

    // Focus on feedbackMessage if it is present on fresh page load
    const feedbackMessage = document.querySelector('[data-hook=feedbackMessage]');
    if (feedbackMessage) {
      feedbackMessage.focus();
      setTimeout(() => {
        feedbackMessage.scrollIntoView({ block: 'center' });
      }, 300);
    }

    // Setup evnt listeners for error summary links
    // If an error summary link is clicked, set focus on related element (necessary to make Firefox work as expected)
    document.querySelectorAll('[data-hook=feedbackMessage__errorList] a').forEach ( function( errorListLink ) {
      errorListLink.addEventListener('click', function(e) {
        e.preventDefault()

        let errorListLink_relatedField = document.getElementById( e.target.getAttribute('href').substring(1) );
        errorListLink_relatedField.focus({preventScroll:true});

        setTimeout(() => {
          errorListLink_relatedField.scrollIntoView({ block: 'center' });
        }, 100);

      })
    })
  },

}

export default App__feedbackMessage;
