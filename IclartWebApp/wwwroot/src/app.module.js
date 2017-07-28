var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { ManageLoginComponent } from './loginManagement/manageLogins.component';
import { ManageClientsComponent } from './clientManagement/manageClients.component';
import { ClientFormComponent } from './clientManagement/clientForm/clientForm.component';
import { CompetitorAdminComponent } from './competitorManagement/competitorAdmin.component';
import { ClientViewComponent } from './clientManagement/clientDetails/clientDetails.component';
import { AccountService } from './services/account.service';
import { ClientService } from './services/client.service';
import { CompetitorService } from './services/competitor.service';
var AppModule = (function () {
    function AppModule() {
    }
    AppModule.prototype.ngDoBootstrap = function (appRef) {
        try {
            appRef.bootstrap(ManageLoginComponent);
        }
        catch (e) {
        }
        try {
            appRef.bootstrap(ManageClientsComponent);
        }
        catch (e) {
        }
        try {
            appRef.bootstrap(CompetitorAdminComponent);
        }
        catch (e) {
        }
        try {
            appRef.bootstrap(ClientFormComponent);
        }
        catch (e) {
            console.log(e);
        }
        try {
            appRef.bootstrap(ClientViewComponent);
        }
        catch (e) {
            console.log(e);
        }
    };
    return AppModule;
}());
AppModule = __decorate([
    NgModule({
        imports: [BrowserModule, HttpModule, FormsModule],
        declarations: [ManageLoginComponent, ManageClientsComponent, CompetitorAdminComponent, ClientFormComponent, ClientViewComponent],
        entryComponents: [ManageLoginComponent, ManageClientsComponent, CompetitorAdminComponent, ClientFormComponent, ClientViewComponent],
        providers: [AccountService, ClientService, CompetitorService]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map