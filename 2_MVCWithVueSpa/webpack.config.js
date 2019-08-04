const path = require('path')
const webpack = require('webpack')
const VueLoaderPlugin = require('vue-loader/lib/plugin')
const mode = 'development';

module.exports = {
  mode,
  entry: './VueApp/main.js',
  devtool: 'source-map',
  output: {
    path: path.resolve(__dirname, './wwwroot/js'),
    publicPath: '/admin',
    filename: 'app.min.js'
  },
  watch: true,
  watchOptions: {
    ignored: [ 'wwwroot/**/*', 'node_modules' ]
  },
  module: {
    rules: [
      {
        test: /\.vue$/,
        loader: 'vue-loader'
      },
      { 
        test: /\.js$/,
        exclude: /(node_modules)/,
        use: [
          {
            loader: 'babel-loader',
            options: {
              'presets': [
                [
                  '@babel/preset-env',
                  {
                    useBuiltIns: 'usage',
                    targets: {
                      browsers: [
                        'IE >= 9'
                      ]
                    }
                  }
                ]
              ],
              'plugins': [
                '@babel/plugin-proposal-class-properties'
              ]
            }
          }
        ]
      },
      {
        test: /\.css$/,
        use: [
          'vue-style-loader',
          {
            loader: 'css-loader',
            options: {              
              modules: true
            }
          }
        ]
      }
    ]
  },
  plugins: [
    new VueLoaderPlugin(),
    new webpack.ProvidePlugin({
      jQuery: 'jquery',
      $: 'jquery',
      jquery: 'jquery'
    })
  ],
  resolve: {
    extensions: [ '.js', '.vue' ],
    alias: {
      'vue$': 'vue/dist/vue',
      '@': path.resolve('VueApp')
    }
  }
}

// module.exports = () => {
//   console.log('Building for \x1b[33m%s\x1b[0m', process.env.NODE_ENV)

//   const isDevBuild = !(process.env.NODE_ENV && process.env.NODE_ENV === 'production')
  
//   const extractCSS = new MiniCssExtractPlugin({
//     filename: 'style.css'
//   })

//   return [{
//     mode: (isDevBuild ? 'development' :'production'  ),
//     stats: { modules: false },
//     entry: { 'main': './VueApp/main.js' },
//     resolve: {
//       extensions: ['.js', '.vue'],
//       alias: isDevBuild ? {
//         'vue$': 'vue/dist/vue',
//         'components': path.resolve(__dirname, './VueApp/components'),
//         'views': path.resolve(__dirname, './VueApp/views'),
//         'utils': path.resolve(__dirname, './VueApp/utils'),
//         'api': path.resolve(__dirname, './VueApp/store/api')
//       } : {
//         'components': path.resolve(__dirname, './VueApp/components'),
//         'views': path.resolve(__dirname, './VueApp/views'),
//         'utils': path.resolve(__dirname, './VueApp/utils'),
//         'api': path.resolve(__dirname, './VueApp/store/api')
//       }
//     },
//     output: {
//       path: path.join(__dirname, bundleOutputDir),
//       filename: '[name].js',
//       publicPath: '/dist/'
//     },
//     module: {
//       rules: [
//         { test: /\.vue$/, include: /VueApp/, use: 'vue-loader' },
//         { test: /\.js$/, include: /VueApp/, use: 'babel-loader' },
//         { test: /\.css$/, use: isDevBuild ? ['style-loader', 'css-loader'] : [MiniCssExtractPlugin.loader, 'css-loader'] },
//         { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' }
//       ]
//     },
//     plugins: [
//       new VueLoaderPlugin(),
//       // new webpack.DllReferencePlugin({
//       //   context: __dirname,
//       //   manifest: require('./wwwroot/dist/vendor-manifest.json')
//       // })
//     ]//.concat(isDevBuild ? [
//     //   // Plugins that apply in development builds only
//     //   new webpack.SourceMapDevToolPlugin({
//     //     filename: '[file].map', // Remove this line if you prefer inline source maps
//     //     moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
//     //   })
//     // ] : [
//     //   extractCSS,
//     //   // Compress extracted CSS.
//     //   new OptimizeCSSPlugin({
//     //     cssProcessorOptions: {
//     //       safe: true
//     //     }
//     //   })
//     // ])
//   }]
// }
