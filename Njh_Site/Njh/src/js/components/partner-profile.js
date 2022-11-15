const App__partnerProfile = {

  init: function() {
    const partnerProfileItems = document.querySelectorAll('[data-hook=partnerProfileGridItem]');

    // Make entire card clickable
    partnerProfileItems.forEach(function( partnerProfileItem ) {

      partnerProfileItem.querySelectorAll('a').forEach(function( partnerProfileItemLink ) {
        partnerProfileItemLink.addEventListener('click', (e) => {
          e.preventDefault();
        });
      });

      partnerProfileItem.addEventListener('click', (e) => {
        let link = partnerProfileItem.querySelector('a');
        let target = link.getAttribute('target');

        if ( link ) {
          if ( target === '_blank' ) {
            window.open( link );
            return false;
          } else {
            window.location = link;
          }
        }

        // if ( link ) {
        //   location.href = link.getAttribute('href');
        // }


      });
    });
  }

}

export default App__partnerProfile;
