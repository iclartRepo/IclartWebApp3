import { NgModule, ApplicationRef } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { ManageLoginComponent } from './loginManagement/manageLogins.component';

import { AccountService } from './services/account.service';

@NgModule({
    imports: [BrowserModule],
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
            console.log(e);
        }         
      
    }
}