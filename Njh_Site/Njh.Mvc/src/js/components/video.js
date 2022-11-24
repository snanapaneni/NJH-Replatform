const App__video = {

  init: function() {
    const videoContainers = Array.from(document.querySelectorAll('[data-hook=videoComponent]'));
    const transcriptToggles = Array.from(document.querySelectorAll('[data-hook=transcriptToggle]'));

    const toggleTranscript = (e) => {
      const button = e.target;
      const transcriptID = button.getAttribute("data-transcript");
      const transcript = document.getElementById(transcriptID);

      if ( button.classList.contains('is-active') ) {
          slideUp(transcript, 250);
          button.classList.remove('is-active');
          button.setAttribute("aria-expanded", false);
          transcript.setAttribute("aria-hidden", true);
      } else {
        slideDown(transcript, 250);
        button.classList.add('is-active');
        button.setAttribute('aria-expanded', true);
        transcript.setAttribute('aria-hidden', false);
      }

    }

    if ( transcriptToggles ) {
      transcriptToggles.map(toggle => {
        toggle.addEventListener('click', toggleTranscript);
      });
    }
  }
}

export default App__video;
