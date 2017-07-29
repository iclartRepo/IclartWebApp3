import { Component, OnInit, ElementRef } from '@angular/core';

import { ProductService } from '../../services/product.service';
import { IMessageResult } from '../../interfaces/messageResult.interface';

@Component({
    selector: 'product-category',
    template: require('./productCategory.component.html')
})
export class ProductCategoryComponent implements OnInit {
    categoryToDelete: number;
    returnUrl: string;
    newCategory: string = "";
    editForm: any = {};
    editFormData: any = {};
    result: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    resultForm: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    errorMessage: string;



    constructor(private _service: ProductService, private elementRef: ElementRef) {
    }
    /* CRUD Functionalities */
    addCategory(): void {
        this._service.addProductCategory(this.newCategory)
            .subscribe(addResponse => {
                this.resultForm = addResponse;
                if (this.resultForm.isError == false) {
                    this.getCategories();
                    this.newCategory = "";
                }
            },
            error => this.errorMessage = <any>error);
    }
    deleteProductCategory(): void {
        this._service.deleteProductCategory(this.categoryToDelete)
            .subscribe(deleteResponse => {
                this.resultForm = deleteResponse;
                if (this.resultForm.isError == false) {
                    this.getCategories();
                }
            },
            error => this.errorMessage = <any>error);
    }
    setCategoryToDelete(id: number): void {
        this.categoryToDelete = id;
    }
    getCategories(): void {
        this._service.getProductCategories()
            .subscribe(categories => {
                this.result = categories;
                for (let entry of this.result.ResultList) {
                    this.editForm[entry.Id] = false;
                    this.editFormData[entry.Id] = entry.Name;
                }
            },
            error => this.errorMessage = <any>error);
    }
    edit(id: number): void {
        this.editForm[id] = true;
    }
    updateCategory(id: number): void {
        this._service.updateProductCategory(id, this.editFormData[id])
            .subscribe(category => {
                this.editForm[id] = false;
            },
            error => this.errorMessage = <any>error);
    }
    /* Validation */
    checkIfExist(name: string, id: number): boolean {
        for (let entry of this.result.ResultList) {
            if (entry.Name == name && entry.Id != id) {
                return true;
            }
        }
        return false;
    }
    /* Initializer and Native Functions */
    ngOnInit(): void {
        this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('categories'));
        this.returnUrl = this.elementRef.nativeElement.getAttribute('returnurl');

        for (let entry of this.result.ResultList) {
                    this.editForm[entry.Id] = false;
                    this.editFormData[entry.Id] = entry.Name;
                }
        //this._service.getProductCategories()
        //    .subscribe(categories => {
        //        this.result = categories;
        //        for (let entry of this.result.ResultList) {
        //            this.editForm[entry.Id] = false;
        //            this.editFormData[entry.Id] = entry.Name;
        //        }
        //    },
        //    error => this.errorMessage = <any>error);
    }
}
