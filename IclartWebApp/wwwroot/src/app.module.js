var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ManageLoginComponent } from './loginManagement/manageLogins.component';
import { AccountService } from './services/account.service';
var AppModule = (function () {
    function AppModule() {
    }
    AppModule.prototype.ngDoBootstrap = function (appRef) {
        try {
            appRef.bootstrap(ManageLoginComponent);
        }
        catch (e) {
            console.log(e);
        }
    };
    return AppModule;
}());
AppModule = __decorate([
    NgModule({
        imports: [BrowserModule],
        declarations: [ManageLoginComponent],
        entryComponents: [ManageLoginComponent],
        providers: [AccountService]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map