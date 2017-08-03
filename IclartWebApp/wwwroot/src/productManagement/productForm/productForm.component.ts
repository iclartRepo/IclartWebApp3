import { Component, AfterViewChecked, ViewChild, OnInit, ElementRef } from '@angular/core';
import { NgForm } from '@angular/forms';

import { ProductService } from "../../services/product.service";
import { CompetitorService } from "../../services/competitor.service";

import { IProduct } from '../../interfaces/product.interface';
import { IMessageResult } from '../../interfaces/messageResult.interface';

@Component({
    selector: 'product-form',
    template: require("./productForm.component.html")
})
export class ProductFormComponent implements OnInit {
    returnUrl: string;
    constructor(private _service: ProductService, private _adminService: CompetitorService, private elementRef: ElementRef) {

    }
    /* Competitor Price Variables */
    competitorPrices: any[] = [];
    competitorPrice: any = {};
    selectedCompetitor: any;
    selectedPrice: number;
    selectedCategory: any;
    resultCompetitors: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    resultCategories: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    realListCompetitors: any[] = [];
    editForm: any = {};
    editFormData: any = {};

    result: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    errorMessage: string;
    product: IProduct = {
        Id: 0,
        Name: "",
        CompanyPrice: 0,
        IsDeleted: false
    }

    productId: number = 0;
    /* Form Validations */
    productForm: NgForm;
    @ViewChild('productForm') currentForm: NgForm;

    ngAfterViewChecked() {
        this.formChanged();
    }

    formChanged() {
        if (this.currentForm === this.productForm) { return; }
        this.productForm = this.currentForm;
        if (this.productForm) {
            this.productForm.valueChanges
                .subscribe(data => this.onValueChanged(data));
        }
    }

    onValueChanged(data?: any) {
        if (!this.productForm) { return; }
        const form = this.productForm.form;

        for (const field in this.formErrors) {
            // clear previous error message (if any)
            this.formErrors[field] = '';
            const control = form.get(field);

            if (control && control.dirty && !control.valid) {
                const messages = this.validationMessages[field];
                for (const key in control.errors) {
                    this.formErrors[field] += messages[key] + ' ';
                }
            }
        }
    }

    formErrors = {
        'name': '',
        'category': ''
    };

    validationMessages = {
        'name': {
            'required': 'Product Name is required.'
        },
        'category': {
            'required': 'Product Category is required.'
        }
    };

    /* Pagination Functions */


    /* CRUD Functions */
    addCompetitorPrice(): void {
        this.competitorPrice = {
            "CompetitorId": this.selectedCompetitor.Id,
            "Name": this.selectedCompetitor.Name,
            "Price": this.selectedPrice,
            "Competitor": this.selectedCompetitor
        };
        this.editForm[this.selectedCompetitor.Id] = false;
        this.editFormData[this.selectedCompetitor.Id] = this.selectedPrice;
        this.removeFromCompetitorList(this.selectedCompetitor.Id);
        this.competitorPrices.push(this.competitorPrice);
        this.selectedCompetitor = null;
        this.selectedPrice = null;
    }
    edit(id: number): void {
        this.editForm[id] = true;
    }
    updatePrice(id: number): void {
        this.competitorPrices.forEach((item, index) => {
            if (item.CompetitorId == id) {
                item.Price = this.editFormData[id];
            }
        });
        this.editForm[id] = false;
    }
    saveProduct(): void {

        var productForm: any = {
            "Product": this.product,
            "ProductCategory": this.selectedCategory,
            "CompetitorPrices": this.competitorPrices
        };
        if (this.productId > 0) {
            this._service.updateProduct(this.productId, productForm)
                .subscribe(product => {
                    this.result = product;
                    if (this.result.isError == false) {
                        window.location.href = this.returnUrl;
                    }
                },
                error => this.errorMessage = <any>error);
        }
        else {
            this._service.addProduct(productForm)
                .subscribe(product => {
                    this.result = product;
                    if (this.result.isError == false) {
                        window.location.href = this.returnUrl;
                    }
                },
                error => this.errorMessage = <any>error);
        }
    }
    removeCompetitorPrice(name: string): void {
        this.competitorPrices.forEach((item, index) => {
            if (item.Name == name) {
                this.realListCompetitors.forEach((item2, index2) => {
                    if (item2.Name == name) {
                        this.resultCompetitors.ResultList.push(item2);
                    }
                });
                this.competitorPrices.splice(index, 1);
               
            }
        });
    }
    removeFromCompetitorList(id: number): void {
        this.resultCompetitors.ResultList.forEach((item, index) => {
            if (item.Id == id) {
                this.realListCompetitors.push(item);
                this.resultCompetitors.ResultList.splice(index, 1);
              
            }
        });
    }
   
    assignCategory(category: any): void {
        for (let entry of this.resultCategories.ResultList) {
            if (entry.Id == category.Id) {
                this.selectedCategory = entry;
            }
        }
    }

    /*  Navigation */
    /* Initialize */
    ngOnInit(): void {
        this.resultCompetitors = JSON.parse(this.elementRef.nativeElement.getAttribute('competitors'));


        this.resultCategories = JSON.parse(this.elementRef.nativeElement.getAttribute('categories'));
        this.returnUrl = this.elementRef.nativeElement.getAttribute('returnurl');
        this.productId = this.elementRef.nativeElement.getAttribute('productid');

        if (this.productId > 0)
        {
            this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('product'));
            this.product = this.result.Result;
            this.assignCategory(this.result.Result.ProductCategory);
            var competitorPrices: any[] = this.result.Result.CompetitorPrices;
            for (let entry of competitorPrices) {
                this.editForm[entry.CompetitorId] = false;
                this.editFormData[entry.CompetitorId] = entry.Price;
                var competitorPrice: any = {
                    "CompetitorId": entry.CompetitorId,
                    "Name": entry.Competitor.Name,
                    "Price": entry.Price,
                    "Competitor": entry.Competitor
                };
                this.competitorPrices.push(competitorPrice);
                this.removeFromCompetitorList(entry.CompetitorId);
            }
        }

        //this._adminService.getCompetitors()
        //    .subscribe(
        //    result => {
        //        this.resultCompetitors = result;
        //        this.realListCompetitors = result.ResultList;
        //    },
        //    error => this.errorMessage = <any>error);
        //this._service.getProductCategories()
        //    .subscribe(
        //    result => {
        //        this.resultCategories = result;
        //    },
        //    error => this.errorMessage = <any>error);
    
    }


}
