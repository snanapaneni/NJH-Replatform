
const App__featuredContent = {

  init: function() {
    const featuredContentComponents = document.querySelectorAll('[data-hook=featuredContentComponent]');

    // Make entire card clickable
    featuredContentComponents.forEach(function( featuredContentItem ) {

       featuredContentItem.querySelectorAll('a').forEach(function(  featuredContentItemLink ) {
        featuredContentItemLink.addEventListener('click', (e) => {
          e.preventDefault();
        });
      });

       featuredContentItem.addEventListener('click', (e) => {
        let link =  featuredContentItem.querySelector('a');
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

export default App__featuredContent;
