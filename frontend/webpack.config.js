const path               = require('path');
const DefinePlugin       = require('webpack').DefinePlugin;
const HtmlWebpackPlugin  = require('html-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const CopyWebpackPlugin  = require('copy-webpack-plugin');
const VueLoaderPlugin    = require('vue-loader/lib/plugin');

module.exports = {
  entry: {
    app: './src/main.js'
  },
  output: {
    filename: '[name].js',
    path: path.resolve(__dirname, 'dist')
  },
  mode: process.env.NODE_ENV,
  module: {
    rules: [
      {
        test: /\.vue$/,
        use: [
          'vue-loader'
        ]
      },
      {
        test: /\.js$/,
        loader: 'babel-loader'
      },
      {
        test: /\.scss$/,
        use: [
          'vue-style-loader',
          'css-loader',
          'sass-loader'
        ]
      }
    ],
  },
  optimization: {
    splitChunks: {
      chunks: 'all'
    }
  },
  plugins: [
    new DefinePlugin({
      BACK_URL: JSON.stringify(process.env.BACK_URL)
    }),
    new VueLoaderPlugin(),
    new CleanWebpackPlugin(),
    new CopyWebpackPlugin([
      {
        from: 'static',
        cache: true,
      },
    ]),
    new HtmlWebpackPlugin({
      title: 'Cinnabuns',
      inject: false,
      filename: 'index.html',
      template: require('html-webpack-template'),
      appMountIds: ['app'],
      baseHref: "/",
      meta: [
        {
          name: 'viewport',
          content: 'width=device-width, width=900'
        }
      ],
      headHtmlSnippet: '<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script><link href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css" rel="stylesheet"><script src="https://code.jquery.com/ui/1.12.0/jquery-ui.min.js" integrity="sha256-eGE6blurk5sHj+rmkfsGYeKyZx3M4bG+ZlFyA7Kns7E=" crossorigin="anonymous"></script><style>body, html {margin: 0;padding: 0}</style>',
      minify: {
        collapseWhitespace: true,
        removeComments: true,
        removeRedundantAttributes: true,
        removeScriptTypeAttributes: true,
        removeStyleLinkTypeAttributes: true,
        useShortDoctype: true
      }
    }),
  ],
  watchOptions: {
    aggregateTimeout: 300,
    poll: 1000,
    ignored: ['node_modules'],
  }
}