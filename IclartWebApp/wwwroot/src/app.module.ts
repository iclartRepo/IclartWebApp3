import { NgModule, ApplicationRef } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { ManageLoginComponent } from './loginManagement/manageLogins.component';

import { AccountService } from './services/account.service';

@NgModule({
    imports: [BrowserModule, HttpModule, FormsModule],
    declarations: [ManageLoginComponent],
    entryComponents: [ManageLoginComponent],
    providers: [AccountService]
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
      
    }
}