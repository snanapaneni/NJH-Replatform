const App__searchResources = {

  apiEndpoint: null,

  searchFilterForm: null,
  searchFilterResetButton: null,
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
    PageSize: 15, // 15
  },

  init: function() {
    if ( ! document.querySelector('[data-hook=searchResources]') ) {
      return false;
    }

    const searchResources = document.querySelector('[data-hook=searchResources]');

    App__searchResources.apiEndpoint = searchResources.dataset.apiEndpoint;
    App__searchResources.searchFilterForm = searchResources.querySelector('[data-hook=searchFilterForm]');
    App__searchResources.searchFilterResetButton = searchResources.querySelector('[data-hook=searchFilterResetButton]');
    App__searchResources.keywordForm = searchResources.querySelector('[data-hook=searchKeywordForm]');
    App__searchResources.keywordInput = searchResources.querySelector('[data-hook=searchKeywordInput]');
    App__searchResources.keywordSubmitButton = searchResources.querySelector('[data-hook=searchKeywordSubmitButton]');
    App__searchResources.sortSelect = searchResources.querySelector('[data-hook=searchSort]');
    App__searchResources.searchResultItems = searchResources.querySelector('[data-hook=searchResultItems]');
    App__searchResources.searchResultsSpinner = searchResources.querySelector('[data-hook=searchResultsSpinner]');
    App__searchResources.searchResultsNoResultsMessage = searchResources.querySelector('[data-hook=searchResultsNoResultsMessage]');
    App__searchResources.searchResultsCountDisplay = searchResources.querySelector('[data-hook=searchResultsCountDisplay]');
    App__searchResources.searchResultsPagination = searchResources.querySelector('[data-hook=searchResultsPagination]');
    App__searchResources.searchResultsPaginationList = searchResources.querySelector('[data-hook=searchResultsPaginationList]');


    // Event Listener: Keyword Form Submit
    App__searchResources.keywordForm.addEventListener('submit', (event) => {
      event.preventDefault();
      App__searchResources.handleSearchFormSubmission();

      App__searchResources.render();
    });

    // Event Listener: Filter Form Submit
    App__searchResources.searchFilterForm.addEventListener('submit', (event) => {
      event.preventDefault();
      App__searchResources.handleSearchFormSubmission();
      App__searchResources.keywordForm.scrollIntoView();

      App__searchResources.render();
    });

    // Event Listener: Filter Form Reset
    App__searchResources.searchFilterResetButton.addEventListener('click', (event) => {
      event.preventDefault();
      // Uncheck filters
      document.querySelectorAll('[data-hook=resourceFilterList] input[type=checkbox]:checked').forEach( (selectedCheckbox) => {
        selectedCheckbox.checked = false;
      });
      // Remove url params
      let url = window.location.href;
      url = App.utils.urlToolkit.removeURLParameter(url, 'page');
      url = App.utils.urlToolkit.removeURLParameter(url, 'topics');
      url = App.utils.urlToolkit.removeURLParameter(url, 'types');
      url = App.utils.urlToolkit.removeURLParameter(url, 'industries');
      App.utils.urlToolkit.updateURL( url );
      App__searchResources.keywordForm.scrollIntoView();

      App__searchResources.render();
    });

    // Event Listener: Sort Change
    App__searchResources.sortSelect.addEventListener('change', (event) => {
      App__searchResources.searchOptions.SortBy = App__searchResources.sortSelect.value;
      // Update browser location bar (and history)
      let url = window.location.href;
      url = App.utils.urlToolkit.removeURLParameter(url, 'page');
      url = App.utils.urlToolkit.removeURLParameter(url, 'sort');
      url = App.utils.urlToolkit.addURLParameter( url, 'sort=' + App__searchResources.sortSelect.value );
      App.utils.urlToolkit.updateURL( url );
      App__searchResources.render();
    });

    // Event Listener: Pagination Click (delegated since injected)
    App__searchResources.searchResultsPaginationList.addEventListener('click', (event) => {
      event.preventDefault();
      if ( !event.target.closest('[data-hook=paginationLink]:not([aria-disabled=true]):not([aria-current=page])') ) {
        return false;
      }

      let paginationURL = ( event.target.getAttribute('href') ) ? event.target.getAttribute('href')
                                                                : event.target.parentElement.getAttribute('href');
      App.utils.urlToolkit.updateURL( paginationURL );
      App__searchResources.render();
      App__searchResources.keywordForm.scrollIntoView();
    });

    // Event Listener: Result Card Click (delegated since injected)
    App__searchResources.searchResultItems.addEventListener('click', (event) => {
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

    // Event Listener: Filter Accordions
    document.querySelectorAll('[data-hook=searchFilterAccordion]').forEach(function (searchFilterAccordion) {
      searchFilterAccordion.querySelector('[data-hook=searchFilterAccordion__trigger]').addEventListener('click', (event) => {
        event.preventDefault();
        if ( event.target.getAttribute('aria-expanded') == "false" ) {
          App__searchResources.openAccordion( searchFilterAccordion );
        } else {
          App__searchResources.closeAccordion( searchFilterAccordion );
        }
      });
    });


    // Re-render on back button
    window.onpopstate = function (event) {
      App__searchResources.render();
    };

    // Aaand load initial results
    App__searchResources.render();
  },

  render: function() {
    const urlParams = App.utils.urlToolkit.parseQueryString(window.location);

    // Show loading state
    App__searchResources.searchResultsSpinner.style.display = 'flex';
    App__searchResources.searchResultsNoResultsMessage.style.display = 'none';
    App__searchResources.searchResultsCountDisplay.style.display = 'none';
    App__searchResources.searchResultItems.style.opacity = '.25';
    App__searchResources.searchResultsPagination.style.opacity = '.25';

    // searchOptions Assemble!
    App__searchResources.searchOptions.Keywords = (urlParams.hasOwnProperty('keyword')) ? urlParams.keyword : '';
    App__searchResources.searchOptions.Page = (urlParams.hasOwnProperty('page')) ? urlParams.page : 1;
    if (urlParams.hasOwnProperty('sort')) {
      App__searchResources.searchOptions.SortBy = urlParams.sort;
    } else {
      // If keyword present, default to Relevance
      if (urlParams.hasOwnProperty('keyword')) {
        App__searchResources.searchOptions.SortBy = 'relevance';
      // Else default to Newest
      } else {
        App__searchResources.searchOptions.SortBy = 'publisheddate-desc';
      }
    }
    App__searchResources.searchOptions.Filters = [];
    // "Topics" Filter
    if ( urlParams.hasOwnProperty('topics') ) {
      let filterValues = urlParams.topics.split(',');
      App__searchResources.searchOptions.Filters.push({
        ColumnName: "Topics",
        Values: filterValues
      });
    }
    // "Types" Filter
    if ( urlParams.hasOwnProperty('types') ) {
      let filterValues = urlParams.types.split(',');
      App__searchResources.searchOptions.Filters.push({
        ColumnName: "ResourceType",
        Values: filterValues
      });
    }
    // "Industries" Filter
    if ( urlParams.hasOwnProperty('industries') ) {
      let filterValues = urlParams.industries.split(',');
      App__searchResources.searchOptions.Filters.push({
        ColumnName: "Industry",
        Values: filterValues
      });
    }


    // Sync ui to searchOptions
    App__searchResources.keywordInput.value = App__searchResources.searchOptions.Keywords;
    App__searchResources.sortSelect.value = App__searchResources.searchOptions.SortBy;


    // Handle filters
    let selectedTopics = [];
    let selectedResourceTypes = [];
    let selectedIndustries = [];
    App__searchResources.searchOptions.Filters.forEach( (filterGroup) => {
      if ( filterGroup.ColumnName == 'Topics' ) {
        selectedTopics = filterGroup.Values.filter( (item) => {
          return item != '';
        });
      }
      if ( filterGroup.ColumnName == 'ResourceType' ) {
        selectedResourceTypes = filterGroup.Values.filter( (item) => {
          return item != '';
        });
      }
      if ( filterGroup.ColumnName == 'Industry' ) {
        selectedIndustries = filterGroup.Values.filter( (item) => {
          return item != '';
        });
      }
    });

    document.querySelector('[data-hook=resourceFilterSelectedCount][data-group=topics]').textContent = ( selectedTopics.length ) ? selectedTopics.length : '';
    document.querySelectorAll('[data-hook=resourceFilterList][data-group=topics] input[type=checkbox]').forEach( (checkbox) => {
      checkbox.checked = ( selectedTopics.includes( checkbox.value ) ) ? true : false;
    });

    document.querySelector('[data-hook=resourceFilterSelectedCount][data-group=types]').textContent = ( selectedResourceTypes.length ) ? selectedResourceTypes.length : '';
    document.querySelectorAll('[data-hook=resourceFilterList][data-group=types] input[type=checkbox]').forEach( (checkbox) => {
      checkbox.checked = ( selectedResourceTypes.includes( checkbox.value ) ) ? true : false;
    });

    document.querySelector('[data-hook=resourceFilterSelectedCount][data-group=industries]').textContent = ( selectedIndustries.length ) ? selectedIndustries.length : '';
    document.querySelectorAll('[data-hook=resourceFilterList][data-group=industries] input[type=checkbox]').forEach( (checkbox) => {
      checkbox.checked = ( selectedIndustries.includes( checkbox.value ) ) ? true : false;
    });

    App__searchResources.populateResults();
  },

  populateResults: function() {
    // console.log( App__searchResources.searchOptions );
    fetch( App__searchResources.apiEndpoint, {
      method: 'POST', // *GET, POST, PUT, DELETE, etc.
      // mode: 'cors', // no-cors, *cors, same-origin
      // cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
      headers: {
        'Content-Type': 'application/json'
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: JSON.stringify( App__searchResources.searchOptions )
    })
      .then(response => response.json())
      .then(data => {
        // console.log(data)

        // Update "currently viewing" info
        if ( data.totalItemCount > 0) {
          // App__searchResources.searchResultsCountDisplay.textContent = data.totalResultsCountDisplay;
          App__searchResources.searchResultsCountDisplay.innerHTML = data.totalResultsCountDisplay.replace('-', '<span class="visually-hidden">through</span>-');

          App__searchResources.searchResultsCountDisplay.style.display = 'block';
        } else {
          App__searchResources.searchResultsCountDisplay.style.display = 'none';
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

            resultMarkup = resultMarkup + '<div class="col-12 col-md-6 col-lg-4   routing__card" data-hook="routingCard">';
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
          App__searchResources.searchResultsSpinner.style.display = 'none';
          App__searchResources.searchResultItems.innerHTML = resultMarkup;
          App__searchResources.searchResultItems.style.opacity = '1';

        // No results
        } else {
          App__searchResources.searchResultsSpinner.style.display = 'none';
          App__searchResources.searchResultItems.innerHTML = '';
          App__searchResources.searchResultItems.style.opacity = '1';
          App__searchResources.searchResultsNoResultsMessage.style.display = 'block';
          App__searchResources.searchResultsNoResultsMessage.focus();
        }

        // Build pagination
        if ( data.totalPageCount > 1 ) {
          App__searchResources.searchResultsPaginationList.innerHTML = App.utils.pagination.buildListItems({
            pageNumber: data.pageNumber,
            totalPageCount: data.totalPageCount
          });
          App__searchResources.searchResultsPagination.style.display = 'block';
          App__searchResources.searchResultsPagination.style.opacity = '1';
        } else {
          App__searchResources.searchResultsPagination.style.display = 'none';
        }

    });
  },

  openAccordion: function ( searchFilterAccordion ) {
    searchFilterAccordion.querySelector('[data-hook=searchFilterAccordion__trigger]').setAttribute('aria-expanded', 'true');
    searchFilterAccordion.querySelector('[data-hook=searchFilterAccordion__content]').setAttribute('aria-hidden', 'false');
  },

  closeAccordion: function (searchFilterAccordion ) {
    searchFilterAccordion.querySelector('[data-hook=searchFilterAccordion__trigger]').setAttribute('aria-expanded', 'false');
    searchFilterAccordion.querySelector('[data-hook=searchFilterAccordion__content]').setAttribute('aria-hidden', 'true');
  },

  handleSearchFormSubmission: function () {
    // Sync url with state of form
    let url = window.location.href;

    // Remove page param so we always go back to first page of results
    url = App.utils.urlToolkit.removeURLParameter(url, 'page');

    // Rebuild keyword param
    url = App.utils.urlToolkit.removeURLParameter(url, 'keyword');
    url = App.utils.urlToolkit.addURLParameter( url, 'keyword=' + App__searchResources.keywordInput.value );

    // Rebuild filter params
    url = App.utils.urlToolkit.removeURLParameter(url, 'topics');
    url = App.utils.urlToolkit.removeURLParameter(url, 'types');
    url = App.utils.urlToolkit.removeURLParameter(url, 'industries');

    document.querySelectorAll('[data-hook=resourceFilterList]').forEach( (resourceFilterList) => {
      let selectedFilters = [];
      let selectedCheckboxes = resourceFilterList.querySelectorAll('input[type=checkbox]:checked');
      if ( selectedCheckboxes.length ) {
        selectedCheckboxes.forEach ( (selectedCheckbox) => {
          selectedFilters.push( selectedCheckbox.value )
        });
        url = App.utils.urlToolkit.addURLParameter( url, resourceFilterList.dataset.group + '=' + selectedFilters.join() );
      }
    });

    // Finally update browser location bar (and history)
    App.utils.urlToolkit.updateURL( url );
  }

}

export default App__searchResources;
