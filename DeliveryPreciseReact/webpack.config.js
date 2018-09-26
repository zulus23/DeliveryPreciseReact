const path = require('path');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const devMode = process.env.NODE_ENV !== 'production'
require("@babel/polyfill");
module.exports = {
    entry:["@babel/polyfill",'./wwwroot/source/index.js'],
   /* mode:'development',*/
    mode:'production',
    output:{
        path : path.resolve(__dirname,"wwwroot/dist"),
        filename:'bundle.js',
        publicPath:'/'
        
    },
    devtool:'cheap-module-source-map',
    
    module: {
        rules: [
            {
                test: /\.css$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    { loader: 'css-loader', options: { url: false, sourceMap: true } },
                  
                ],
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader",
                    options: { presets:
                            ['@babel/preset-env'] }
                }
            },
            
        ]
            
    },
   /* optimization: {
        occurrenceOrder: true // To keep filename consistent between different modes (for example building only)
    },*/
   
    /*optimization: {
        splitChunks: {
            cacheGroups: {
                styles: {
                    name: 'styles',
                    test: /\.css$/,
                    chunks: 'all',
                    enforce: true
                }
            }
        }
    },*/
    plugins:[
        new MiniCssExtractPlugin({
            // Options similar to the same options in webpackOptions.output
            // both options are optional
            filename: 'style_vendor.css',
            
        })        

    ]
    
    
}