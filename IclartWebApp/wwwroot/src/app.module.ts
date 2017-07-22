import { NgModule, ApplicationRef } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { ManageLoginComponent } from './loginManagement/manageLogins.component';
import { ManageClientsComponent } from './clientManagement/manageClients.component'

import { AccountService } from './services/account.service';
import { ClientService } from './services/client.service';

@NgModule({
    imports: [BrowserModule, HttpModule, FormsModule],
    declarations: [ManageLoginComponent, ManageClientsComponent],
    entryComponents: [ManageLoginComponent, ManageClientsComponent],
    providers: [AccountService, ClientService]
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
            console.log(e);
        }         
      
    }
}