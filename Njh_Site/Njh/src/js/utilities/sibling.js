const App__sibling = {

  getNext: function ( elem, callback ) {
    let sibling = elem.nextElementSibling;
    if ( !callback || typeof callback !== 'function' ) return sibling;
    let index = 0;
    while ( sibling ) {
      if ( callback(sibling, index, elem) ) return sibling;
      index++;
      sibling = sibling.nextElementSibling;
    }
  }

}

export default App__sibling;
