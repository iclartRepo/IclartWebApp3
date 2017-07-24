import { NgModule, ApplicationRef } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { ManageLoginComponent } from './loginManagement/manageLogins.component';
import { ManageClientsComponent } from './clientManagement/manageClients.component';
import { CompetitorAdminComponent } from './competitorManagement/competitorAdmin.component';

import { AccountService } from './services/account.service';
import { ClientService } from './services/client.service';
import { CompetitorService } from './services/competitor.service';

@NgModule({
    imports: [BrowserModule, HttpModule, FormsModule],
    declarations: [ManageLoginComponent, ManageClientsComponent, CompetitorAdminComponent],
    entryComponents: [ManageLoginComponent, ManageClientsComponent, CompetitorAdminComponent],
    providers: [AccountService, ClientService, CompetitorService]
})
export class AppModule {
    ngDoBootstrap(appRef: ApplicationRef)
    {
        try
        {
            appRef.bootstrap(ManageLoginComponent);
        }
        catch (e)
        {
            
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
      
    }
}