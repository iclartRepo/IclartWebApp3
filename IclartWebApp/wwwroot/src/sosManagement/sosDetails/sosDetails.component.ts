import { Component, OnInit, ElementRef } from '@angular/core';

import { IMessageResult } from '../../interfaces/messageResult.interface';

import { SosService } from '../../services/sos.service';

@Component({
    selector: 'sos-details',
    template: require("./sosDetails.component.html")
})
export class SOSDetailsComponent {

    constructor(private _sosService: SosService, private elementRef: ElementRef) { }

    sosDetail: any;
    sosDate: string;
    productsView: any[] = [];
    discardOrder: any = {};
    discardType: any = {};

    result: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    errorMessage: string;

    /* Initializer and Native Functions */
    ngOnInit(): void {
        this.sosDetail = JSON.parse(this.elementRef.nativeElement.getAttribute('sos'));
        this.sosDate = this.elementRef.nativeElement.getAttribute('sosdate');

        for (let entry of this.sosDetail.Orders) {
            var productView = {
                "Id": entry.Id,
                "Quantity": entry.Quantity,
                "QuantityDelivered": entry.QuantityDelivered,
                "Price": entry.Price,
                "Name": entry.Product.Name,
                "Unit": entry.Unit,
                "TotalPrice": entry.Price * entry.Quantity,
                "Custom": false
            };
            this.discardOrder[entry.Id] = false;
            this.discardType[entry.Id] = 'standard';
            this.productsView.push(productView);
        }

        for (let entry of this.sosDetail.CustomOrders) {
            var productView = {
                "Id": entry.Id,
                "Quantity": entry.Quantity,
                "QuantityDelivered": entry.QuantityDelivered,
                "Price": entry.Price,
                "Name": entry.ItemDescription,
                "Unit": entry.Unit,
                "TotalPrice": entry.Price * entry.Quantity,
                "Custom": true
            };
            this.discardOrder[entry.Id] = false;
            this.discardType[entry.Id] = 'custom';
            this.productsView.push(productView);
        }
    }
}
