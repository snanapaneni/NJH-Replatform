const App__searchEvents = {

  apiEndpoint: null,

  keywordForm: null,
  keywordInput: null,
  keywordSubmit: null,
  // sortSelect: null,

  searchResultItems: null,
  searchResultsSpinner: null,
  searchResultsNoResultsMessage: null,
  searchResultsCountDisplay: null,
  searchResultsPagination: null,
  searchResultsPaginationList: null,

  searchOptions: {
    Keywords: '',
    SortBy: '',
    Page: 1,
    PageSize: 9, // 9
  },

  init: function() {
    if ( ! document.querySelector('[data-hook=searchEvents]') ) {
      return false;
    }

    const searchEvents = document.querySelector('[data-hook=searchEvents]');

    App__searchEvents.apiEndpoint = searchEvents.dataset.apiEndpoint;
    App__searchEvents.keywordForm = searchEvents.querySelector('[data-hook=searchKeywordForm]');
    App__searchEvents.keywordInput = searchEvents.querySelector('[data-hook=searchKeywordInput]');
    App__searchEvents.keywordSubmitButton = searchEvents.querySelector('[data-hook=searchKeywordSubmitButton]');
    // App__searchEvents.sortSelect = searchEvents.querySelector('[data-hook=searchSort]');
    App__searchEvents.searchResultItems = searchEvents.querySelector('[data-hook=searchResultItems]');
    App__searchEvents.searchResultsSpinner = searchEvents.querySelector('[data-hook=searchResultsSpinner]');
    App__searchEvents.searchResultsNoResultsMessage = searchEvents.querySelector('[data-hook=searchResultsNoResultsMessage]');
    App__searchEvents.searchResultsCountDisplay = searchEvents.querySelector('[data-hook=searchResultsCountDisplay]');
    App__searchEvents.searchResultsPagination = searchEvents.querySelector('[data-hook=searchResultsPagination]');
    App__searchEvents.searchResultsPaginationList = searchEvents.querySelector('[data-hook=searchResultsPaginationList]');

    // Event Listener: Form Submit
    App__searchEvents.keywordForm.addEventListener('submit', (event) => {
      event.preventDefault();
      App__searchEvents.searchOptions.Keywords = App__searchEvents.keywordInput.value;
      // Update browser location bar (and history)
      let url = window.location.href;
      url = App.utils.urlToolkit.removeURLParameter(url, 'page');
      url = App.utils.urlToolkit.removeURLParameter(url, 'keyword');
      url = App.utils.urlToolkit.addURLParameter( url, 'keyword=' + App__searchEvents.keywordInput.value );
      App.utils.urlToolkit.updateURL( url );
      App__searchEvents.render();
    });

    // Event Listener: Sort Change
    // App__searchEvents.sortSelect.addEventListener('change', (event) => {
    //   App__searchEvents.searchOptions.SortBy = App__searchEvents.sortSelect.value;
    //   // Update browser location bar (and history)
    //   let url = window.location.href;
    //   url = App.utils.urlToolkit.removeURLParameter(url, 'page');
    //   url = App.utils.urlToolkit.removeURLParameter(url, 'sort');
    //   url = App.utils.urlToolkit.addURLParameter( url, 'sort=' + App__searchEvents.sortSelect.value );
    //   App.utils.urlToolkit.updateURL( url );
    //   App__searchEvents.render();
    // });

    // Event Listener: Pagination Click (delegated since injected)
    App__searchEvents.searchResultsPaginationList.addEventListener('click', (event) => {
      event.preventDefault();
      if ( !event.target.closest('[data-hook=paginationLink]:not([aria-disabled=true]):not([aria-current=page])') ) {
        return false;
      }

      let paginationURL = ( event.target.getAttribute('href') ) ? event.target.getAttribute('href')
                                                                : event.target.parentElement.getAttribute('href');
      App.utils.urlToolkit.updateURL( paginationURL );
      App__searchEvents.render();
      App__searchEvents.keywordForm.scrollIntoView();
    });

    // Event Listener: Result Card Click (delegated since injected)
    App__searchEvents.searchResultItems.addEventListener('click', (event) => {
      if ( !event.target.closest('[data-hook=routingCard]') ) {
        return false;
      }
      event.preventDefault();
      let routingCard = event.target.closest('[data-hook=routingCard]');
      let link = routingCard.querySelector('a');
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

    // Re-render on back button
    window.onpopstate = function (event) {
      App__searchEvents.render();
    };

    // Aaand load initial results
    App__searchEvents.render();
  },

  render: function() {
    const urlParams = App.utils.urlToolkit.parseQueryString(window.location);

    App__searchEvents.searchResultsSpinner.style.display = 'flex';
    App__searchEvents.searchResultsNoResultsMessage.style.display = 'none';
    App__searchEvents.searchResultsCountDisplay.style.display = 'none';
    App__searchEvents.searchResultItems.style.opacity = '.25';
    App__searchEvents.searchResultsPagination.style.opacity = '.25';

    // searchOptions Assemble!
    App__searchEvents.searchOptions.Keywords = (urlParams.hasOwnProperty('keyword')) ? urlParams.keyword : '';
    App__searchEvents.searchOptions.Page = (urlParams.hasOwnProperty('page')) ? urlParams.page : 1;
    if (urlParams.hasOwnProperty('sort')) {
      App__searchEvents.searchOptions.SortBy = urlParams.sort;
    } else {
      // If keyword present, default to Relevance
      if (urlParams.hasOwnProperty('keyword')) {
        App__searchEvents.searchOptions.SortBy = 'relevance';
      // Else default to Start Date
      } else {
        App__searchEvents.searchOptions.SortBy = 'startdate-asc';
      }
    }

    // Sync ui to searchOptions
    App__searchEvents.keywordInput.value = App__searchEvents.searchOptions.Keywords;
    // App__searchEvents.sortSelect.value = App__searchEvents.searchOptions.SortBy;


    App__searchEvents.populateResults();
  },

  populateResults: function() {
    fetch( App__searchEvents.apiEndpoint, {
      method: 'POST', // *GET, POST, PUT, DELETE, etc.
      // mode: 'cors', // no-cors, *cors, same-origin
      // cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
      headers: {
        'Content-Type': 'application/json'
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: JSON.stringify( App__searchEvents.searchOptions )
    })
      .then(response => response.json())
      .then(data => {
        // console.log(data)

        // Update "currently viewing" info
        if ( data.totalItemCount > 0) {
          // App__searchEvents.searchResultsCountDisplay.textContent = data.totalResultsCountDisplay;
          App__searchEvents.searchResultsCountDisplay.innerHTML = data.totalResultsCountDisplay.replace('-', '<span class="visually-hidden">through</span>-');
          App__searchEvents.searchResultsCountDisplay.style.display = 'block';
        } else {
          App__searchEvents.searchResultsCountDisplay.style.display = 'none';
        }


        // Have results, build and inject markup!
        if ( data.items.length ) {
          let resultMarkup = '';

          data.items.forEach( item => {

            let responsiveImage = App.utils.responsiveImage.create({
              imageURL: item.imageUrl,
              altText: item.imageAltText,
              class: '',
              sizes: [
                '(max-width: 720px) 100vw',
                '(max-width: 960px) 50vw',
                '(max-width: 1440px) 30vw',
                '400px'
              ],
              srcsetWidths: [
                '640',
                '960',
                '1280'
              ]
            });

            resultMarkup = resultMarkup + '<div class="col-12   routing__card" data-hook="routingCard">';
            resultMarkup = resultMarkup +   '<div class="routing__card-main">';
            resultMarkup = resultMarkup +     '<div class="row">';
            resultMarkup = resultMarkup +       '<div class="col-12 col-lg-4 col-xl-3">';
            resultMarkup = resultMarkup +         '<div class="routing__card-image-wrapper">';
            if ( item.isFeatured ) {
              resultMarkup = resultMarkup +         '<p class="routing__card-featured-label">Featured</p>';
            }

            resultMarkup = resultMarkup +           responsiveImage;

            resultMarkup = resultMarkup +         '</div>';
            resultMarkup = resultMarkup +       '</div>';
            resultMarkup = resultMarkup +       '<div class="col-12 col-lg-8 col-xl-9">';
            resultMarkup = resultMarkup +         '<div>';
            // resultMarkup = resultMarkup +         '<div class="routing__card-eyebrow">';
            // resultMarkup = resultMarkup +           '<p>' + item.pageType + '</p>';
            // resultMarkup = resultMarkup +         '</div>';
            resultMarkup = resultMarkup +           '<a href="' + item.pageUrl + '" class="routing__card-title e-arrow-link">' + item.title + '</a>';

            resultMarkup = resultMarkup +           '<p class="routing__card-date">';
            resultMarkup = resultMarkup +             '<span>' + item.displayDate + '</span>';
            if ( item.location ) {
              resultMarkup = resultMarkup +           '<span>' + item.location + '</span>';
            }
            if ( item.additionalDateInformation ) {
              resultMarkup = resultMarkup +           '<span>' + item.additionalDateInformation + '</span>';
            }
            if ( item.format ) {
              resultMarkup = resultMarkup +           '<span>' + item.format + '</span>';
            }
            resultMarkup = resultMarkup +           '</p>';

            resultMarkup = resultMarkup +           '<p class="routing__card-description">' + item.summary + '</p>';
            if ( item.sponsored ) {
              resultMarkup = resultMarkup +         '<div class="routing__card-footer"><p>' + item.sponsored + '</p></div>';
            }
            if ( item.isGated ) {
              if ( item.isLocked ) {
                resultMarkup = resultMarkup +         '<div class="routing__card-member-only-badge is-locked"><p>Member only</p></div>';
              } else {
                resultMarkup = resultMarkup +         '<div class="routing__card-member-only-badge"><p>Member only</p></div>';
              }
            }
            resultMarkup = resultMarkup +         '</div>';
            resultMarkup = resultMarkup +       '</div>';
            resultMarkup = resultMarkup +     '</div>';
            resultMarkup = resultMarkup +   '</div>';
            resultMarkup = resultMarkup + '</div>';
          });
          App__searchEvents.searchResultsSpinner.style.display = 'none';
          App__searchEvents.searchResultItems.innerHTML = resultMarkup;
          App__searchEvents.searchResultItems.style.opacity = '1';

        // No results
        } else {
          App__searchEvents.searchResultsSpinner.style.display = 'none';
          App__searchEvents.searchResultItems.innerHTML = '';
          App__searchEvents.searchResultItems.style.opacity = '1';
          App__searchEvents.searchResultsNoResultsMessage.style.display = 'block';
          App__searchEvents.searchResultsNoResultsMessage.focus();
        }

        // Build pagination
        if ( data.totalPageCount > 1 ) {
          App__searchEvents.searchResultsPaginationList.innerHTML = App.utils.pagination.buildListItems({
            pageNumber: data.pageNumber,
            totalPageCount: data.totalPageCount
          });
          App__searchEvents.searchResultsPagination.style.display = 'block';
          App__searchEvents.searchResultsPagination.style.opacity = '1';
        } else {
          App__searchEvents.searchResultsPagination.style.display = 'none';
        }

    });

  }

}

export default App__searchEvents;
