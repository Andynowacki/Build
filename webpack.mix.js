const mix = require('laravel-mix');
const fs = require('fs');
const replaceInFiles = require('replace-in-files');

/*
 |--------------------------------------------------------------------------
 | Mix Asset Management
 |--------------------------------------------------------------------------
 |
 | Mix provides a clean, fluent API for defining some Webpack build steps
 | for your Laravel application. By default, we are compiling the Sass
 | file for your application, as well as bundling up your JS files.
 |
 */

const dist = 'SitefinityWebApp/ResourcePackages/Bootstrap4/assets';
const admin = 'SitefinityWebApp/AdminApp/assets';

const src = {
  images: 'Isobar.FED/images',
  fonts: 'Isobar.FED/fonts',
  scss: 'Isobar.FED/scss/main.scss',
  editor: 'Isobar.FED/scss/editor.scss',
  js: 'Isobar.FED/js/main.js'
};

// Add vendor libraries to this array to extract them at build time into vendors.js
const vendors = [];

mix
  .setPublicPath('./')
  .disableNotifications()
  .webpackConfig({
    stats: {
      hash: false,
      version: false,
      children: false
    }
  })
  .sass(src.scss, `${dist}/css`)
  .sass(src.editor, `${admin}/css`)
  .js(src.js, `${dist}/js`)
  .copyDirectory(src.images, `${dist}/images`)
  .copyDirectory(src.fonts, `${dist}/fonts`)
  .extract(vendors)
  .options({ processCssUrls: false })
  .version();

if (!mix.inProduction()) {
  mix
    .browserSync({
      proxy: 'http://localhost:62728/',
      files: [`${dist}/css/*.css`, `${dist}/js/*.js`]
    })
    // .browserSync({
    //   startPath: '/',
    //   notify: false,
    //   port: 3000,
    //   proxy: {
    //     target: 'https://inxsoftware.azurewebsites.net/'
    //   },
    //   serveStatic: [`${__dirname}\\SitefinityWebApp\\ResourcePackages\\Bootstrap4\\assets`],
    //   rewriteRules: [
    //     {
    //       match: /\/ResourcePackages\/Bootstrap4\/assets\/css\/main\.css/g,
    //       replace: '/css/main.css'
    //     },
    //     {
    //       match: /\/ResourcePackages\/Bootstrap4\/assets\/js\/manifest\.js/g,
    //       replace: '/js/manifest.js'
    //     },
    //     {
    //       match: /\/ResourcePackages\/Bootstrap4\/assets\/js\/vendor\.js/g,
    //       replace: '/js/vendor.js'
    //     },
    //     {
    //       match: /\/ResourcePackages\/Bootstrap4\/assets\/js\/main\.js/g,
    //       replace: '/js/main.js'
    //     }
    //   ]
    // })
    .sourceMaps();
} else {
  // in prod
  mix.then(() => {
    const files = JSON.parse(fs.readFileSync('mix-manifest.json', 'utf8'));

    const pipelines = [];

    for (const key in files) {
      if (key.includes('editor')) {
        continue;
      }

      let hashedUrl = files[key];
      hashedUrl = hashedUrl.replace('/SitefinityWebApp', '~');

      let assetName = key.split('/');
      assetName = assetName[assetName.length - 1];

      pipelines.push({ from: `__ASSET__${assetName}__`, to: hashedUrl });
    }

    // TODO: this needs to be done programatically...
    // for now this will have to do
    (async (pipelines) => {
      await replaceInFiles({
        files: ['SitefinityWebApp/Web.Staging.config', 'SitefinityWebApp/Web.Production.config'],
        from: pipelines[0].from,
        to: pipelines[0].to
      })
        .pipe(pipelines[1])
        .pipe(pipelines[2])
        .pipe(pipelines[3]);
    })(pipelines);
  });
}

// Full API
// mix.js(src, output);
// mix.react(src, output); <-- Identical to mix.js(), but registers React Babel compilation.
// mix.preact(src, output); <-- Identical to mix.js(), but registers Preact compilation.
// mix.coffee(src, output); <-- Identical to mix.js(), but registers CoffeeScript compilation.
// mix.ts(src, output); <-- TypeScript support. Requires tsconfig.json to exist in the same folder as webpack.mix.js
// mix.extract(vendorLibs);
// mix.sass(src, output);
// mix.less(src, output);
// mix.stylus(src, output);
// mix.postCss(src, output, [require('postcss-some-plugin')()]);
// mix.browserSync('my-site.test');
// mix.combine(files, destination);
// mix.babel(files, destination); <-- Identical to mix.combine(), but also includes Babel compilation.
// mix.copy(from, to);
// mix.copyDirectory(fromDir, toDir);
// mix.minify(file);
// mix.sourceMaps(); // Enable sourcemaps
// mix.version(); // Enable versioning.
// mix.disableNotifications();
// mix.setPublicPath('path/to/public');
// mix.setResourceRoot('prefix/for/resource/locators');
// mix.autoload({}); <-- Will be passed to Webpack's ProvidePlugin.
// mix.webpackConfig({}); <-- Override webpack.config.js, without editing the file directly.
// mix.babelConfig({}); <-- Merge extra Babel configuration (plugins, etc.) with Mix's default.
// mix.then(function () {}) <-- Will be triggered each time Webpack finishes building.
// mix.dump(); <-- Dump the generated webpack config object t the console.
// mix.extend(name, handler) <-- Extend Mix's API with your own components.
// mix.options({
//   extractVueStyles: false, // Extract .vue component styling to file, rather than inline.
//   globalVueStyles: file, // Variables file to be imported in every component.
//   processCssUrls: true, // Process/optimize relative stylesheet url()'s. Set to false, if you don't want them touched.
//   purifyCss: false, // Remove unused CSS selectors.
//   terser: {}, // Terser-specific options. https://github.com/webpack-contrib/terser-webpack-plugin#options
//   postCss: [] // Post-CSS options: https://github.com/postcss/postcss/blob/master/docs/plugins.md
// });
