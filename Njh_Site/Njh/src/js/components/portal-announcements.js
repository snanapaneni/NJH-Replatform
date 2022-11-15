
const App__portalAnnouncements = {

  announcementItems: [],

  init: function () {
    let availableAnnouncements = document.querySelector('[data-hook=availablePortalAnnouncements]');
    if ( availableAnnouncements ) {
      availableAnnouncements = JSON.parse(availableAnnouncements.textContent);
      if ( Array.isArray(availableAnnouncements)  &&  availableAnnouncements.length > 0 ) {
        // Setup event listeners for dismising announcements
        let announcementsList = document.querySelector('[data-hook=portalAnnouncementsList]');
        document.querySelector('[data-hook=portalAnnouncementsList]').addEventListener('click', (e) => {
            if ( !e.target.matches('[data-hook=portalAnnouncementDismissTrigger]') ) {
              return false;
            }
            e.preventDefault();
            let announcementsListItem = e.target.parentElement;
            App.portalAnnouncements.dismissItem( announcementsListItem );
          });

        // Default to showing all availableAnnouncements
        App.portalAnnouncements.announcementItems = availableAnnouncements;

        // If we have dismissedAnnouncements in localstorage, use them to filter the announcements to display
        let dismissedAnnouncements = App.portalAnnouncements.getDismissed();
        if ( Array.isArray(dismissedAnnouncements)  &&  dismissedAnnouncements.length > 0 ) {
          App.portalAnnouncements.announcementItems = availableAnnouncements.filter((e) => {
            return !dismissedAnnouncements.some(
              (item) => item.guid === e.guid
            );
          });

          App.portalAnnouncements.cleanupDismissedAnnouncements();
        }

        // Render Announcements
        if ( App.portalAnnouncements.announcementItems.length ) {
          App.portalAnnouncements.renderListItems();
          document.querySelector('[data-hook=portalAnnouncements]').style.display = "block";
        }
      }
    }
  },

  reset: function () {
    localStorage.removeItem('dismissedAnnouncements');
    location.reload();
  },

  getDismissed: function () {
    let dismissedAnnouncements = localStorage.getItem('dismissedAnnouncements');
    if ( dismissedAnnouncements ) {
      return JSON.parse(dismissedAnnouncements);
    }
    return [];
  },

  renderListItems: function () {
    if ( Array.isArray(App.portalAnnouncements.announcementItems)  &&  App.portalAnnouncements.announcementItems.length > 0 ) {
      let announcementsList = document.querySelector('[data-hook=portalAnnouncementsList]');

      App.portalAnnouncements.announcementItems.forEach(function (
        announcement
      ) {
        announcementsList.insertAdjacentHTML('beforeend',
          '<li class="portal-announcements__list-item" data-guid="' + announcement.guid + '" data-hook="portalAnnouncementsListItem">' +
            '<button class="portal-announcements__list-close-button" data-hook="portalAnnouncementDismissTrigger">' +
              '<span class="visually-hidden">Dismiss ' + announcement.title + " announcement</span>" +
            '</button>' +
            '<div class="row gx-0">' +
              `${
                announcement.imageUrl !== "" ? `
                  <div class="col-12 col-md-5">
                    <div class="portal-announcements__item-image-wrapper">
                      <img class="portal-announcements__item-image" src="${announcement.imageUrl}" alt="${announcement.imageAltText}">
                    </div>
                  </div> `
                  : ''
                }` +
              `<div class="col-12 ${announcement.imageUrl !== "" ? "col-md-7" : ""}">` +
                '<div class="portal-announcements__item-text">' +
                  '<h3 class="portal-announcements__item-title">' + announcement.title + '</h3>' +
                  '<p class="portal-announcements__item-description">' + announcement.description + '</p>' +
                  `${
                    announcement.ctaUrl !== "" || announcement.ctaText !== "" ? `
                      <p class="portal-announcements__item-link"><a href="${announcement.ctaUrl}">${announcement.ctaText}</a></p>`
                      : ''
                    }` +
                '</div>' +
              '</div>' +
            '</div>' +
          '</li>'
        );
      });
    }
  },

  dismissItem: function (announcementsListItem) {
    let dismissedAnnouncement = {
      guid: announcementsListItem.dataset.guid,
      dateDismissed: new Date().getTime(),
    };
    let announcementsComponent = document.querySelector('[data-hook=portalAnnouncements]');

    // Add to entry to dismissedAnnouncements
    let dismissedAnnouncements = App.portalAnnouncements.getDismissed();
    dismissedAnnouncements.push(dismissedAnnouncement);
    localStorage.setItem('dismissedAnnouncements', JSON.stringify(dismissedAnnouncements));

    // Remove this item from DOM
    announcementsListItem.parentNode.removeChild(announcementsListItem);

    // Remove this item from App.portalAnnouncements.announcementItems
    App.portalAnnouncements.announcementItems.forEach(function (announcement, index) {
      if ( announcement.guid === announcementsListItem.dataset.guid ) {
        App.portalAnnouncements.announcementItems.splice(index, 1);
      }
    });

    // If is last item hide entire section
    if ( !App.portalAnnouncements.announcementItems.length ) {
      announcementsComponent.style.display = "none";
    }

    // Set focus on the parent column
    announcementsComponent.closest('div.col-12').focus();
  },

  // Delete dismissedAnnounements from localstorage that were dismissed more than 90 days ago
  cleanupDismissedAnnouncements: function () {
    let dismissedAnnouncements = App.portalAnnouncements.getDismissed();
    if ( dismissedAnnouncements.length ) {
      let dismissedDateThreshold = new Date();
      dismissedDateThreshold.setDate(dismissedDateThreshold.getDate() - 90);

      dismissedAnnouncements.forEach(function (announcement, index) {
        let announcementDateDismissed = new Date(announcement.dateDismissed);
        if ( dismissedDateThreshold.getTime() > announcementDateDismissed.getTime() ) {
          // Delete this item from store
          // TODO ...
          dismissedAnnouncements.splice(index, 1);
        }
      });

      localStorage.setItem('dismissedAnnouncements', JSON.stringify(dismissedAnnouncements));
    }
  }

}

export default App__portalAnnouncements;
