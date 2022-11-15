const path = require('path');
const fs = require('fs');

let staticConfig = require( '../static/components/_config.js' );
// if ( fs.existsSync( path.resolve(__dirname, '../static/components/_config.local.js') ) ) {
//   staticConfig = require( '../static/components/_config.local.js' );
// }

let staticTemplatePartials = [
  {
    path: path.resolve(__dirname, '../static/app-header.html'),
    priority: 'low',
    location: 'body'
  },
  {
    path: path.resolve(__dirname, '../static/app-main.html'),
    priority: 'low',
    location: 'body'
  },
  {
    path: path.resolve(__dirname, '../static/app-footer.html'),
    priority: 'low',
    location: 'body'
  },
];



// console.log(staticConfig);
staticConfig.components.forEach(filename => {
  let filepath = path.resolve(__dirname, '../static/components/' + filename + '.html')
  try {
    if ( fs.existsSync( filepath ) ) {
      // File exists
      let componentPartial = {
        path: filepath,
        priority: 'low',
        location: 'main'
      }
      staticTemplatePartials.push( componentPartial )
    }
  } catch( err ) {
    console.error( err )
  }
});


exports.partials = staticTemplatePartials;
