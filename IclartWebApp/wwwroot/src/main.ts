﻿// Entry point for JiT compilation.
declare var System: any;

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app.module';

// Enables Hot Module Replacement.
declare var module: any;
if (module.hot) {
    module.hot.accept();
}

window.onload = function () {
    platformBrowserDynamic().bootstrapModule(AppModule);
}
