// Karma configuration file, see link for more information
// https://karma-runner.github.io/1.0/config/configuration-file.html

module.exports = function (config) {
  config.set({
    basePath: '',
    frameworks: ['jasmine', '@angular/cli'],
    plugins: [
      require('karma-jasmine'),
      require('karma-chrome-launcher'),
      require('karma-jasmine-html-reporter'),
      require('karma-coverage-istanbul-reporter'),
      require('@angular/cli/plugins/karma')
    ],
    client:{
      clearContext: false // leave Jasmine Spec Runner output visible in browser
    },
    coverageIstanbulReporter: {
      reports: [ 'html', 'lcovonly' ],
      fixWebpackSourcePaths: true
    },
    angularCli: {
      environment: 'dev'
    },
    reporters: ['progress', 'kjhtml'],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: true,
    singleRun: true,
    browsers: ["Chrome-headless"],
    customLaunchers: {
        "Chrome-headless": {
            base: 'Chrome',
            flags: ['--headless', '--remote-debugging-port=9222', '--no-sandbox']
        }
    },
browserNoActivityTimeout: 1000000,
    coverageReporter: {
      includeAllSources: true,
      dir: 'coverage/',
      reporters: [
          { type: "html", subdir: "html" },
          { type: 'text-summary' }
      ]
    },
    coverageIstanbulReporter: {
      reports: [ 'html', 'lcovonly' ],
      fixWebpackSourcePaths: true,
      thresholds: {
        statements: 50,
        lines: 50,
        branches: 50,
        functions: 50
      }      
    }});
  
};
