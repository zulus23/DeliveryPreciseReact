const path = require('path');


module.exports = {
    entry:'./wwwroot/source/index.js',
    mode:'development',
    output:{
        path : path.resolve(__dirname,"wwwroot/dist"),
        filename:'bundle.js'
        
    },
    module: {
        rules: [
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
    plugins:[
        

    ]
    
    
}