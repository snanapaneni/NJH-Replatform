
const App__accordion = {

  init: function() {
    // Attach event listeners
    document.querySelectorAll('[data-hook=accordionGroupItem__title]').forEach ( function( accordionGroupItem__title ) {
      accordionGroupItem__title.addEventListener('click', function(e) {
        e.preventDefault()
        let accordionGroupItem = e.target.closest('[data-hook=accordionGroupItem]');
        if ( accordionGroupItem__title.getAttribute('aria-expanded') == 'false' ) {
          App__accordion.open( accordionGroupItem )
        } else {
          App__accordion.close( accordionGroupItem )
        }
      })
    })
  },

  open: function( accordionGroupItem ) {
    accordionGroupItem.querySelector('[data-hook=accordionGroupItem__title]').setAttribute('aria-expanded', 'true')
    accordionGroupItem.querySelector('[data-hook=accordionGroupItem__content]').style.display = 'block'
    accordionGroupItem.querySelector('[data-hook=accordionGroupItem__content]').setAttribute('aria-hidden', 'false')

    // If bottom of title is not in viewport, scroll up a bit to make it obvious that new content has appeared
    let accordionGroupItemTitleBoundingBox = accordionGroupItem.querySelector('[data-hook=accordionGroupItem__title]').getBoundingClientRect()
    if ( accordionGroupItemTitleBoundingBox.bottom > (window.innerHeight || document.documentElement.clientHeight) ) {
      window.scrollBy({
        top: 200,
        behavior: 'smooth'
      });
    }
  },

  close: function( accordionGroupItem ) {
    accordionGroupItem.querySelector('[data-hook=accordionGroupItem__title]').setAttribute('aria-expanded', 'false')
    accordionGroupItem.querySelector('[data-hook=accordionGroupItem__content]').style.display = 'none'
    accordionGroupItem.querySelector('[data-hook=accordionGroupItem__content]').setAttribute('aria-hidden', 'true')
  }
}


export default App__accordion;
