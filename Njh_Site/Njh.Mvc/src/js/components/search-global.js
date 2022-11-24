const App__searchGlobal = {

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
    PageSize: 15, // 15
  },

  init: function() {
    if ( ! document.querySelector('[data-hook=searchGlobal]') ) {
      return false;
    }

    const searchGlobal = document.querySelector('[data-hook=searchGlobal]');

    App__searchGlobal.apiEndpoint = searchGlobal.dataset.apiEndpoint;
    App__searchGlobal.keywordForm = searchGlobal.querySelector('[data-hook=searchKeywordForm]');
    App__searchGlobal.keywordInput = searchGlobal.querySelector('[data-hook=searchKeywordInput]');
    App__searchGlobal.keywordSubmitButton = searchGlobal.querySelector('[data-hook=searchKeywordSubmitButton]');
    App__searchGlobal.sortSelect = searchGlobal.querySelector('[data-hook=searchSort]');
    App__searchGlobal.searchResultItems = searchGlobal.querySelector('[data-hook=searchResultItems]');
    App__searchGlobal.searchResultsSpinner = searchGlobal.querySelector('[data-hook=searchResultsSpinner]');
    App__searchGlobal.searchResultsNoResultsMessage = searchGlobal.querySelector('[data-hook=searchResultsNoResultsMessage]');
    App__searchGlobal.searchResultsCountDisplay = searchGlobal.querySelector('[data-hook=searchResultsCountDisplay]');
    App__searchGlobal.searchResultsPagination = searchGlobal.querySelector('[data-hook=searchResultsPagination]');
    App__searchGlobal.searchResultsPaginationList = searchGlobal.querySelector('[data-hook=searchResultsPaginationList]');

    // Event Listener: Form Submit
    App__searchGlobal.keywordForm.addEventListener('submit', (event) => {
      event.preventDefault();
      App__searchGlobal.searchOptions.Keywords = App__searchGlobal.keywordInput.value;
      // Update browser location bar (and history)
      let url = window.location.href;
      url = App.utils.urlToolkit.removeURLParameter(url, 'page');
      url = App.utils.urlToolkit.removeURLParameter(url, 'keyword');
      url = App.utils.urlToolkit.addURLParameter( url, 'keyword=' + App__searchGlobal.keywordInput.value );
      App.utils.urlToolkit.updateURL( url );
      App__searchGlobal.render();
    });

    // Event Listener: Sort Change
    App__searchGlobal.sortSelect.addEventListener('change', (event) => {
      App__searchGlobal.searchOptions.SortBy = App__searchGlobal.sortSelect.value;
      // Update browser location bar (and history)
      let url = window.location.href;
      url = App.utils.urlToolkit.removeURLParameter(url, 'page');
      url = App.utils.urlToolkit.removeURLParameter(url, 'sort');
      url = App.utils.urlToolkit.addURLParameter( url, 'sort=' + App__searchGlobal.sortSelect.value );
      App.utils.urlToolkit.updateURL( url );
      App__searchGlobal.render();
    });

    // Event Listener: Pagination Click (delegated since injected)
    App__searchGlobal.searchResultsPaginationList.addEventListener('click', (event) => {
      event.preventDefault();
      if ( !event.target.closest('[data-hook=paginationLink]:not([aria-disabled=true]):not([aria-current=page])') ) {
        return false;
      }

      let paginationURL = ( event.target.getAttribute('href') ) ? event.target.getAttribute('href')
                                                                : event.target.parentElement.getAttribute('href');
      App.utils.urlToolkit.updateURL( paginationURL );
      App__searchGlobal.render();
      App__searchGlobal.keywordForm.scrollIntoView();
    });

    // Event Listener: Result Card Click (delegated since injected)
    App__searchGlobal.searchResultItems.addEventListener('click', (event) => {
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
      App__searchGlobal.render();
    };

    // Aaand load initial results
    App__searchGlobal.render();
  },

  render: function() {
    const urlParams = App.utils.urlToolkit.parseQueryString(window.location);

    // Show loading state
    App__searchGlobal.searchResultsSpinner.style.display = 'flex';
    App__searchGlobal.searchResultsNoResultsMessage.style.display = 'none';
    App__searchGlobal.searchResultsCountDisplay.style.display = 'none';
    App__searchGlobal.searchResultItems.style.opacity = '.25';
    App__searchGlobal.searchResultsPagination.style.opacity = '.25';

    // searchOptions Assemble!
    App__searchGlobal.searchOptions.Keywords = (urlParams.hasOwnProperty('keyword')) ? urlParams.keyword : '';
    App__searchGlobal.searchOptions.Page = (urlParams.hasOwnProperty('page')) ? urlParams.page : 1;
    if (urlParams.hasOwnProperty('sort')) {
      App__searchGlobal.searchOptions.SortBy = urlParams.sort;
    } else {
      // If keyword present, default to Relevance
      if (urlParams.hasOwnProperty('keyword')) {
        App__searchGlobal.searchOptions.SortBy = 'relevance';
      // Else default to Newest
      } else {
        App__searchGlobal.searchOptions.SortBy = 'publisheddate-desc';
      }
    }

    // Sync ui to searchOptions
    App__searchGlobal.keywordInput.value = App__searchGlobal.searchOptions.Keywords;
    App__searchGlobal.sortSelect.value = App__searchGlobal.searchOptions.SortBy;

    App__searchGlobal.populateResults();
  },

  populateResults: function() {
    fetch( App__searchGlobal.apiEndpoint, {
      method: 'POST',
      // mode: 'cors', // no-cors, *cors, same-origin
      // cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
      headers: {
        'Content-Type': 'application/json'
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: JSON.stringify( App__searchGlobal.searchOptions )
    })
      .then(response => response.json())
      .then(data => {
        // console.log(data)

        // Update "currently viewing" info
        if ( data.totalItemCount > 0) {
          // App__searchGlobal.searchResultsCountDisplay.textContent = data.totalResultsCountDisplay;
          App__searchGlobal.searchResultsCountDisplay.innerHTML = data.totalResultsCountDisplay.replace('-', '<span class="visually-hidden">through</span>-');
          App__searchGlobal.searchResultsCountDisplay.style.display = 'block';
        } else {
          App__searchGlobal.searchResultsCountDisplay.style.display = 'none';
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

            resultMarkup = resultMarkup +       '<div class="col-12 col-lg-8 col-xl-9">';
            resultMarkup = resultMarkup +         '<div>';
            resultMarkup = resultMarkup +           '<div class="routing__card-eyebrow">';
            resultMarkup = resultMarkup +             '<p>' + item.pageType + '</p>';
            resultMarkup = resultMarkup +           '</div>';

            resultMarkup = resultMarkup +           '<a href="' + item.pageUrl + '" class="routing__card-title e-arrow-link">' + item.title + '</a>';

            if ( item.publishedDateDisplay ) {
              resultMarkup = resultMarkup +         '<p class="routing__card-date">' + item.publishedDateDisplay + '</p>';
            }

            if ( item.summary ) {
              resultMarkup = resultMarkup +         '<p class="routing__card-description">' + item.summary + '</p>';
            }

            // if ( item.sponsored ) {
            //   resultMarkup = resultMarkup +         '<div class="routing__card-footer"><p>' + item.sponsored + '</p></div>';
            // }

            if ( item.isGated ) {
              if ( item.isLocked ) {
                resultMarkup = resultMarkup +       '<div class="routing__card-member-only-badge is-locked"><p>Member only</p></div>';
              } else {
                resultMarkup = resultMarkup +       '<div class="routing__card-member-only-badge"><p>Member only</p></div>';
              }
            }
            resultMarkup = resultMarkup +         '</div>';
            resultMarkup = resultMarkup +       '</div>';

            resultMarkup = resultMarkup +       '<div class="col-12 col-lg-4 col-xl-3">';
            resultMarkup = resultMarkup +         '<div class="routing__card-image-wrapper">';

            if ( item.isFeatured ) {
              resultMarkup = resultMarkup +         '<p class="routing__card-featured-label">Featured</p>';
            }

            resultMarkup = resultMarkup +           responsiveImage;

            resultMarkup = resultMarkup +         '</div>';
            resultMarkup = resultMarkup +       '</div>';
            resultMarkup = resultMarkup +     '</div>';

            resultMarkup = resultMarkup +   '</div>';
            resultMarkup = resultMarkup + '</div>';
          });
          App__searchGlobal.searchResultsSpinner.style.display = 'none';
          App__searchGlobal.searchResultItems.innerHTML = resultMarkup;
          App__searchGlobal.searchResultItems.style.opacity = '1';

        // No results
        } else {
          App__searchGlobal.searchResultsSpinner.style.display = 'none';
          App__searchGlobal.searchResultItems.innerHTML = '';
          App__searchGlobal.searchResultItems.style.opacity = '1';
          App__searchGlobal.searchResultsNoResultsMessage.style.display = 'block';
          App__searchGlobal.searchResultsNoResultsMessage.focus();
        }

        // Build pagination
        if ( data.totalPageCount > 1 ) {
          App__searchGlobal.searchResultsPaginationList.innerHTML = App.utils.pagination.buildListItems({
            pageNumber: data.pageNumber,
            totalPageCount: data.totalPageCount
          });
          App__searchGlobal.searchResultsPagination.style.display = 'block';
          App__searchGlobal.searchResultsPagination.style.opacity = '1';
        } else {
          App__searchGlobal.searchResultsPagination.style.display = 'none';
        }

    });

  }

}

export default App__searchGlobal;
