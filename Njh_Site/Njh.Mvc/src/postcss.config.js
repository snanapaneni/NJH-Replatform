const purgecss = require('@fullhuman/postcss-purgecss')

module.exports = {
  plugins: [

    // Add vendor prefixes
    require('autoprefixer'),

    // Tree-shake (Remove unused styles)
    // To disable CSS Tree-Shaking, comment this entry out completely
    
    purgecss({
      content: [
        './js/**/*.js',
        './js/**/*.jsx',
        './static/*.html',
        './static/**/*.html',
        '../Views/**/*.cshtml',
        '../wwwroot/static/*.html',
      ],
      fontFace: true,
      keyframes: true,
      variables: true,
      safelist: {
        standard: [
          /^content-has-/,
          /^has-/,
          /^is-/,
          /^t-/,
          /^will-/,
        ],
        deep: [
          /animate$/,
          /^autocomplete__/,
          /^ktc-/,
          /^modal-/,
          /^page-/,
          /^trbot-azure/,
        ],
        greedy: []
      },
    }),

  ]
}
