module.exports = {
    outputDir: '../wwwroot',
    configureWebpack: config => { config.entry.app = ["babel-polyfill", "./src/main.js"]; },
    transpileDependencies: [
        /[/\\]node_modules[/\\]test[/\\]/,
        /[/\\]node_modules[/\\][@\\]test2[/\\]test3[/\\]/,
        ['sockjs-client'],
    ],
};