const App__pagination = {

  /**
   *  function: buildListItems
   *  Build and return an html string of <li>'s.
   *  The options object is REQUIRED and must look like:
   *    options = {
   *      pageNumber: (int),
   *      totalPageCount: (int),
   *    }
   *
   */
  buildListItems: function( options ) {

    if ( !options.hasOwnProperty('pageNumber') || !options.hasOwnProperty('totalPageCount') ) {
      return false;
    }

    let paginationItems = ''

    // Build Previous nav item
    if ( options.pageNumber == 1 ) {
      paginationItems = paginationItems + '<li class="pagination__list-item is-previous"><a href="#" class="previous-link" tabindex="0" role="button" aria-disabled="true" aria-label="Previous page" rel="prev"><span>Previous</span></a></li>';
    } else {
      let prevUrl = window.location.href;
      prevUrl = App.utils.urlToolkit.removeURLParameter( prevUrl, 'page' );
      prevUrl = App.utils.urlToolkit.addURLParameter( prevUrl, 'page=' + (options.pageNumber - 1) );
      paginationItems = paginationItems + '<li class="pagination__list-item is-previous"><a href="' + prevUrl + '" class="previous-link" role="button" aria-label="Previous page" rel="prev" data-hook="paginationLink"><span>Previous</span></a></li>';
    }


    let pageNumbersToOutput = [];
    // If pages <= 9, ouput them all
    if ( options.totalPageCount <= 9) {
      [...Array( options.totalPageCount ).keys()].forEach( pageNumber => {
        pageNumbersToOutput.push( pageNumber + 1 );
      });
    // Else do some crazy truncated nav business
    }  else {
      // Always output first & last pages
      pageNumbersToOutput.push(1);
      pageNumbersToOutput.push(options.totalPageCount);
      // Always output current page and page before and after it
      pageNumbersToOutput.push( options.pageNumber );
      pageNumbersToOutput.push( options.pageNumber - 1);
      pageNumbersToOutput.push( options.pageNumber + 1);
      // If on last page give it an extra before it
      if ( options.pageNumber == options.totalPageCount ) {
        pageNumbersToOutput.push( options.pageNumber - 2);
      }
    }


    let pageWasOutput = false;
    [...Array( options.totalPageCount ).keys()].forEach( pageNumber => {
      let outputPage = false;
      let pageNumberCorrected = pageNumber + 1;

      if ( pageNumbersToOutput.includes(pageNumberCorrected) ) {
        outputPage = true;
      }

      if ( outputPage ) {
        let pageUrl = window.location.href;
        pageUrl = App.utils.urlToolkit.removeURLParameter( pageUrl, 'page' );
        pageUrl = App.utils.urlToolkit.addURLParameter( pageUrl, 'page=' + pageNumberCorrected );
        if ( pageNumberCorrected == options.pageNumber) {
          paginationItems = paginationItems + '<li class="pagination__list-item is-current"><a href="' + pageUrl + '" role="button" aria-current="page" aria-current="page" aria-label="Page ' + pageNumberCorrected + '" rel="prev" data-hook="paginationLink"><span>' + pageNumberCorrected + '</span></a></li>';
        } else {
          paginationItems = paginationItems + '<li class="pagination__list-item"><a href="' + pageUrl + '" role="button" aria-label="Page ' + pageNumberCorrected + '" rel="prev" data-hook="paginationLink"><span>' + pageNumberCorrected + '</span></a></li>';
        }
        pageWasOutput = true;
      } else {
        // Output ellipsis
        if ( pageWasOutput) {
          paginationItems = paginationItems + '<li class="pagination__list-item"><span>...</span></li>';
          pageWasOutput = false;
        }
      }
    });



    // Build Next nav item
    if ( options.pageNumber == options.totalPageCount ) {
      paginationItems = paginationItems + '<li class="pagination__list-item is-next"><a href="#" class="next-link" tabindex="0" role="button" aria-disabled="true" aria-label="Next page" rel="next"><span>Next</span></a></li>';
    } else {
      let nextUrl = window.location.href;
      nextUrl = App.utils.urlToolkit.removeURLParameter( nextUrl, 'page' );
      nextUrl = App.utils.urlToolkit.addURLParameter( nextUrl, 'page=' + (options.pageNumber + 1) );
      paginationItems = paginationItems + '<li class="pagination__list-item is-next"><a href="' + nextUrl + '" class="next-link" role="button" aria-label="Next page" rel="next" data-hook="paginationLink"><span>Next</span></a></li>';
    }

    return paginationItems;
  }

}

export default App__pagination;
