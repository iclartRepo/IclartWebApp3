import { NgModule, ApplicationRef } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { ManageLoginComponent } from './loginManagement/manageLogins.component';
import { ManageClientsComponent } from './clientManagement/manageClients.component';
import { ClientFormComponent } from './clientManagement/clientForm/clientForm.component';
import { CompetitorAdminComponent } from './competitorManagement/competitorAdmin.component';
import { ClientViewComponent } from './clientManagement/clientDetails/clientDetails.component';
import { ProductCategoryComponent } from './productManagement/productCategory/productCategory.component';
import { ProductListComponent } from './productManagement/productList/productList.component';
import { ProductDetailsComponent } from './productManagement/productDetails/productDetails.component';
import { ProductFormComponent } from './productManagement/productForm/productForm.component';
import { SOSListComponent } from './sosManagement/sosList/sosList.component';
import { SOSFormComponent } from './sosManagement/sosForm/sosForm.component';

import { AccountService } from './services/account.service';
import { ClientService } from './services/client.service';
import { CompetitorService } from './services/competitor.service';
import { ProductService } from './services/product.service';
import { SosService } from './services/sos.service';
import { UtilitiesService } from './services/utilities.service';

@NgModule({
    imports: [BrowserModule, HttpModule, FormsModule],
    declarations: [ManageLoginComponent, ManageClientsComponent, CompetitorAdminComponent, ClientFormComponent, ClientViewComponent, ProductCategoryComponent, ProductListComponent, ProductDetailsComponent, ProductFormComponent, SOSListComponent, SOSFormComponent],
    entryComponents: [ManageLoginComponent, ManageClientsComponent, CompetitorAdminComponent, ClientFormComponent, ClientViewComponent, ProductCategoryComponent, ProductListComponent, ProductDetailsComponent, ProductFormComponent, SOSListComponent, SOSFormComponent],
    providers: [AccountService, ClientService, CompetitorService, ProductService, SosService, UtilitiesService]
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

        try {
            appRef.bootstrap(ClientFormComponent);
        }
        catch (e) {
       
        }  
        try {
            appRef.bootstrap(ClientViewComponent);
        }
        catch (e) {
      
        }    

        try {
            appRef.bootstrap(ProductCategoryComponent);
        }
        catch (e) {

        }   

        try {
            appRef.bootstrap(ProductListComponent);
        }
        catch (e) {

        }    
        try {
            appRef.bootstrap(ProductDetailsComponent);
        }
        catch (e) {

        }   

        try {
            appRef.bootstrap(ProductFormComponent);
        }
        catch (e) {

        }    
        try {
            appRef.bootstrap(SOSListComponent);
        }
        catch (e) {

        }  
        try {
            appRef.bootstrap(SOSFormComponent);
        }
        catch (e) {
            console.log(e);
        }            
      
    }
}