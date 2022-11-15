const App__responsiveImage = {

  create: function( options ) {
    // Convert imageUrl to URL object so we can remove any url params
    options.imageUrl = new URL( options.imageURL );

    let responsiveImage =               '<img';

    responsiveImage = responsiveImage + ' src="' + options.imageUrl.origin + options.imageUrl.pathname + '"';
    responsiveImage = responsiveImage + ' loading="lazy"';
    responsiveImage = responsiveImage + ' class="' + options.class + '"';
    responsiveImage = responsiveImage + ' alt="' + options.altText + '"';

    responsiveImage = responsiveImage + ' sizes="' + options.sizes.join() + '"';

    // Build srcset
    let srcsetValues = [];
    options.srcsetWidths.forEach( srcsetWidth => {
      srcsetValues.push( options.imageUrl.pathname + '?width=' + srcsetWidth + ' ' + srcsetWidth + 'w');
    });
    responsiveImage = responsiveImage + ' srcset="' + srcsetValues.join() + '"';

    responsiveImage = responsiveImage + '>';

    /*
    <img src="/getmedia/01edeff7-1435-4c47-b67a-d56913eb8562/placeholder-photo-horizontal-2.jpg?width=1800&amp;height=1196&amp;ext=.jpg"
    alt="Sample alt text"
    loading="lazy"
    sizes="(max-width: 720px) 100vw,
           (max-width: 960px) 50vw,
           (max-width: 1440px) 30vw,
           400px"
    srcset="/getmedia/01edeff7-1435-4c47-b67a-d56913eb8562/placeholder-photo-horizontal-2.jpg?ext=.jpg&amp;width=640 640w,
            /getmedia/01edeff7-1435-4c47-b67a-d56913eb8562/placeholder-photo-horizontal-2.jpg?ext=.jpg&amp;width=960 960w,
            /getmedia/01edeff7-1435-4c47-b67a-d56913eb8562/placeholder-photo-horizontal-2.jpg?ext=.jpg&amp;width=1280 1280w">
    */

    return responsiveImage;
  }

}

export default App__responsiveImage;
