import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app.module';
if (module.hot) {
    module.hot.accept();
}
window.onload = function () {
    platformBrowserDynamic().bootstrapModule(AppModule);
};
//# sourceMappingURL=main.js.map