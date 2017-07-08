var path = require('path');

var webpack = require('webpack');

var HtmlWebpackPlugin = require('html-webpack-plugin');
var CopyWebpackPlugin = require('copy-webpack-plugin');
var CleanWebpackPlugin = require('clean-webpack-plugin');
var helpers = require('./webpack.helpers');

console.log('@@@@@@@@@ USING DEVELOPMENT @@@@@@@@@@@@@@@');

module.exports = {

    devtool: 'source-map',
    performance: {
        hints: false
    },
    entry: {
        'polyfills': './wwwroot/js/polyfills.ts',
        'vendor': './wwwroot/js/vendor.ts',
        'app': './wwwroot/src/main.ts'
    },

    output: {
        path: __dirname + '/wwwroot/',
        filename: 'js/[name].bundle.js',
        chunkFilename: 'js/[id].chunk.js',
        publicPath: '/'
    },

    resolve: {
        extensions: ['.ts', '.js', '.json', '.css', '.scss', '.html']
    },

    devServer: {
        historyApiFallback: true,
        contentBase: path.join(__dirname, '/wwwroot/'),
        watchOptions: {
            aggregateTimeout: 300,
            poll: 1000
        }
    },

    module: {
        rules: [
            {
                test: /\.ts$/,
                loaders: [
                    'awesome-typescript-loader',
                    'angular-router-loader',
                    'angular2-template-loader',
                    'source-map-loader',
                    'tslint-loader'
                ]
            },
            {
                test: /\.(png|jpg|gif|woff|woff2|ttf|svg|eot)$/,
                loader: 'file-loader?name=assets/[name]-[hash:6].[ext]'
            },
            {
                test: /favicon.ico$/,
                loader: 'file-loader?name=/[name].[ext]'
            },
            {
                test: /\.css$/,
                loader: 'style-loader!css-loader'
            },
            {
                test: /\.scss$/,
                exclude: /node_modules/,
                loaders: ['style-loader', 'css-loader', 'sass-loader']
            },
            {
                test: /\.html$/,
                loader: 'raw-loader'
            }
        ],
        exprContextCritical: false
    },
    plugins: [
        new webpack.optimize.CommonsChunkPlugin({ name: ['vendor', 'polyfills'] })

    ]

};

