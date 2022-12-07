const App__urlToolkit = {

  parseQueryString: function() {
    var queryString = {};
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i=0;i<vars.length;i++) {
      var pair = vars[i].split("=");
      // If first entry with this name
      if (typeof queryString[pair[0]] === 'undefined') {
        queryString[pair[0]] = decodeURIComponent(pair[1]);
      // If second entry with this name
      } else if (typeof queryString[pair[0]] === 'string') {
        var arr = [ queryString[pair[0]],decodeURIComponent(pair[1]) ];
        queryString[pair[0]] = arr;
      // If third or later entry with this name
      } else {
        queryString[pair[0]].push(decodeURIComponent(pair[1]));
      }
    }
    return queryString;
  },

  updateURL: function( newURL ) {
    if ( history.pushState ) {
     window.history.pushState( {path:newURL}, '', newURL );
    }
  },

  addURLParameter: function( url, parameter ) {
    let separator = (url.indexOf('?') === -1) ? '?' : '&';
    let newParameter = separator + parameter;
    url = url.replace(newParameter, '') + newParameter;
    return url;
  },

  removeURLParameter: function( url, parameter ) {
    //prefer to use l.search if you have a location/link object
    let urlParts = url.split('?');
    if ( urlParts.length >= 2 ) {

      let prefix = encodeURIComponent(parameter) + '=';
      let parts = urlParts[1].split(/[&;]/g);

      for ( var i= parts.length; i-- > 0; ) {
        if ( parts[i].lastIndexOf(prefix, 0) !== -1 ) {
          parts.splice(i, 1);
        }
      }

      let url = urlParts[0] + (parts.length > 0 ? '?' + parts.join('&') : '');
      return url;
    } else {
      return url;
    }
  }

}

export default App__urlToolkit;
