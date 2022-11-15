const App__eventRouting = {

  init: function() {
    const routingCards = document.querySelectorAll('[data-hook=eventRoutingCard]');
    // console.log(routingCards)
    // Make entire card clickable
    routingCards.forEach(function( routingCardItem ) {

      routingCardItem.querySelectorAll('a').forEach(function( routingCardItemLink ) {

        routingCardItemLink.addEventListener('click', (e) => {
          e.preventDefault();
        });
      });

      routingCardItem.addEventListener('click', (e) => {
                    console.log('hello')

        let link = routingCardItem.querySelector('a');
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

export default App__eventRouting;
