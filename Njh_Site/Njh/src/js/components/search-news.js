const App__searchNews = {

  apiEndpoint: null,

  keywordForm: null,
  keywordInput: null,
  keywordSubmit: null,
  sortSelect: null,

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
    PageSize: 16, // 16
  },

  init: function() {
    if ( ! document.querySelector('[data-hook=searchNews]') ) {
      return false;
    }

    const searchNews = document.querySelector('[data-hook=searchNews]');

    App__searchNews.apiEndpoint = searchNews.dataset.apiEndpoint;
    App__searchNews.keywordForm = searchNews.querySelector('[data-hook=searchKeywordForm]');
    App__searchNews.keywordInput = searchNews.querySelector('[data-hook=searchKeywordInput]');
    App__searchNews.keywordSubmitButton = searchNews.querySelector('[data-hook=searchKeywordSubmitButton]');
    App__searchNews.sortSelect = searchNews.querySelector('[data-hook=searchSort]');
    App__searchNews.searchResultItems = searchNews.querySelector('[data-hook=searchResultItems]');
    App__searchNews.searchResultsSpinner = searchNews.querySelector('[data-hook=searchResultsSpinner]');
    App__searchNews.searchResultsNoResultsMessage = searchNews.querySelector('[data-hook=searchResultsNoResultsMessage]');
    App__searchNews.searchResultsCountDisplay = searchNews.querySelector('[data-hook=searchResultsCountDisplay]');
    App__searchNews.searchResultsPagination = searchNews.querySelector('[data-hook=searchResultsPagination]');
    App__searchNews.searchResultsPaginationList = searchNews.querySelector('[data-hook=searchResultsPaginationList]');

    // Event Listener: Form Submit
    App__searchNews.keywordForm.addEventListener('submit', (event) => {
      event.preventDefault();
      App__searchNews.searchOptions.Keywords = App__searchNews.keywordInput.value;
      // Update browser location bar (and history)
      let url = window.location.href;
      url = App.utils.urlToolkit.removeURLParameter(url, 'page');
      url = App.utils.urlToolkit.removeURLParameter(url, 'keyword');
      url = App.utils.urlToolkit.addURLParameter( url, 'keyword=' + App__searchNews.keywordInput.value );
      App.utils.urlToolkit.updateURL( url );
      App__searchNews.render();
    });

    // Event Listener: Sort Change
    App__searchNews.sortSelect.addEventListener('change', (event) => {
      App__searchNews.searchOptions.SortBy = App__searchNews.sortSelect.value;
      // Update browser location bar (and history)
      let url = window.location.href;
      url = App.utils.urlToolkit.removeURLParameter(url, 'page');
      url = App.utils.urlToolkit.removeURLParameter(url, 'sort');
      url = App.utils.urlToolkit.addURLParameter( url, 'sort=' + App__searchNews.sortSelect.value );
      App.utils.urlToolkit.updateURL( url );
      App__searchNews.render();
    });

    // Event Listener: Pagination Click (delegated since injected)
    App__searchNews.searchResultsPaginationList.addEventListener('click', (event) => {
      event.preventDefault();
      if ( !event.target.closest('[data-hook=paginationLink]:not([aria-disabled=true]):not([aria-current=page])') ) {
        return false;
      }

      let paginationURL = ( event.target.getAttribute('href') ) ? event.target.getAttribute('href')
                                                                : event.target.parentElement.getAttribute('href');
      App.utils.urlToolkit.updateURL( paginationURL );
      App__searchNews.render();
      App__searchNews.keywordForm.scrollIntoView();
    });

    // Event Listener: Result Card Click (delegated since injected)
    App__searchNews.searchResultItems.addEventListener('click', (event) => {
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
      App__searchNews.render();
    };

    // Aaand load initial results
    App__searchNews.render();
  },

  render: function() {
    const urlParams = App.utils.urlToolkit.parseQueryString(window.location);

    // Show loading state
    App__searchNews.searchResultsSpinner.style.display = 'flex';
    App__searchNews.searchResultsNoResultsMessage.style.display = 'none';
    App__searchNews.searchResultsCountDisplay.style.display = 'none';
    App__searchNews.searchResultItems.style.opacity = '.25';
    App__searchNews.searchResultsPagination.style.opacity = '.25';

    // searchOptions Assemble!
    App__searchNews.searchOptions.Keywords = (urlParams.hasOwnProperty('keyword')) ? urlParams.keyword : '';
    App__searchNews.searchOptions.Page = (urlParams.hasOwnProperty('page')) ? urlParams.page : 1;
    if (urlParams.hasOwnProperty('sort')) {
      App__searchNews.searchOptions.SortBy = urlParams.sort;
    } else {
      // If keyword present, default to Relevance
      if (urlParams.hasOwnProperty('keyword')) {
        App__searchNews.searchOptions.SortBy = 'relevance';
      // Else default to Newest
      } else {
        App__searchNews.searchOptions.SortBy = 'publisheddate-desc';
      }
    }

    // Sync ui to searchOptions
    App__searchNews.keywordInput.value = App__searchNews.searchOptions.Keywords;
    App__searchNews.sortSelect.value = App__searchNews.searchOptions.SortBy;


    App__searchNews.populateResults();
  },

  populateResults: function() {
    fetch( App__searchNews.apiEndpoint, {
      method: 'POST',
      // mode: 'cors', // no-cors, *cors, same-origin
      // cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
      headers: {
        'Content-Type': 'application/json'
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: JSON.stringify( App__searchNews.searchOptions )
    })
      .then(response => response.json())
      .then(data => {
        // console.log(data)

        // Update "currently viewing" info
        if ( data.totalItemCount > 0) {
          // App__searchNews.searchResultsCountDisplay.textContent = data.totalResultsCountDisplay;
          App__searchNews.searchResultsCountDisplay.innerHTML = data.totalResultsCountDisplay.replace('-', '<span class="visually-hidden">through</span>-');
          App__searchNews.searchResultsCountDisplay.style.display = 'block';
        } else {
          App__searchNews.searchResultsCountDisplay.style.display = 'none';
        }

        // Inject results
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

            resultMarkup = resultMarkup + '<div class="col-12 col-md-6 col-lg-3   routing__card" data-hook="routingCard">';
            resultMarkup = resultMarkup +     '<div class="routing__card-main">';
            resultMarkup = resultMarkup +         '<div class="routing__card-image-wrapper">';
            if ( item.isFeatured ) {
              resultMarkup = resultMarkup +             '<p class="routing__card-featured-label">Featured</p>';
            }
            resultMarkup = resultMarkup +             responsiveImage;
            resultMarkup = resultMarkup +         '</div>';
            resultMarkup = resultMarkup +         '<div class="routing__card-eyebrow">';
            resultMarkup = resultMarkup +             '<p>' + item.pageType + '</p>';
            resultMarkup = resultMarkup +         '</div>';
            resultMarkup = resultMarkup +         '<a href="' + item.pageUrl + '" class="routing__card-title e-arrow-link">' + item.title + '</a>';
            resultMarkup = resultMarkup +         '<p class="routing__card-date">' + item.publishedDateDisplay + '</p>';

            if ( item.isGated ) {
              if ( item.isLocked ) {
                resultMarkup = resultMarkup +         '<div class="routing__card-member-only-badge is-locked"><p>Member only</p></div>';
              } else {
                resultMarkup = resultMarkup +         '<div class="routing__card-member-only-badge"><p>Member only</p></div>';
              }
            }

            resultMarkup = resultMarkup +     '</div>';
            resultMarkup = resultMarkup + '</div>';
          });
          App__searchNews.searchResultsSpinner.style.display = 'none';
          App__searchNews.searchResultItems.innerHTML = resultMarkup;
          App__searchNews.searchResultItems.style.opacity = '1';

        // No results
        } else {
          App__searchNews.searchResultsSpinner.style.display = 'none';
          App__searchNews.searchResultItems.innerHTML = '';
          App__searchNews.searchResultItems.style.opacity = '1';
          App__searchNews.searchResultsNoResultsMessage.style.display = 'block';
          App__searchNews.searchResultsNoResultsMessage.focus();
        }

        // Build pagination
        if ( data.totalPageCount > 1 ) {
          App__searchNews.searchResultsPaginationList.innerHTML = App.utils.pagination.buildListItems({
            pageNumber: data.pageNumber,
            totalPageCount: data.totalPageCount
          });
          App__searchNews.searchResultsPagination.style.display = 'block';
          App__searchNews.searchResultsPagination.style.opacity = '1';
        } else {
          App__searchNews.searchResultsPagination.style.display = 'none';
        }

    });

  }

}

export default App__searchNews;
