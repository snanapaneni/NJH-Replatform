const App__logoZone = {

  init: function() {
    const logoZoneItems = document.querySelectorAll('[data-hook=logoZoneItem]');

    // Make entire card clickable
    logoZoneItems.forEach(function( logoZoneItem ) {

      logoZoneItem.querySelectorAll('a').forEach(function( logoZoneItemLink ) {
        logoZoneItemLink.addEventListener('click', (e) => {
          e.preventDefault();
        });
      });

      logoZoneItem.addEventListener('click', (e) => {
        let link = logoZoneItem.querySelector('a');
        let target = link.getAttribute('target');

        if ( link ) {
          if ( target === '_blank' ) {
            window.open( link );
            return false;
          } else {
            window.location = link;
          }
        }
      });
    });
  }
}

export default App__logoZone;
