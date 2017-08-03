import { Component, OnInit, ElementRef } from '@angular/core';

import { ProductService } from '../../services/product.service';
import { IMessageResult } from '../../interfaces/messageResult.interface';

@Component({
    selector: 'product-details',
   template: require("./productDetails.component.html")
})
export class ProductDetailsComponent implements OnInit {
    returnUrl: string;
    result: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    errorMessage: string;

    productId: number;

    constructor(private _service: ProductService, private elementRef: ElementRef) {
    }

    /* Navigation Functions */


    getProduct(id: number): void {
        this._service.getProduct(id).subscribe(
            result => {
                this.result = result;
            },
            error => this.errorMessage = <any>error);
    }

    /* Initializer and Native Functions */
    ngOnInit(): void {
        this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('product'));
        this.returnUrl = this.elementRef.nativeElement.getAttribute('returnurl');
    }
}
