import { Component, OnInit, ElementRef } from '@angular/core';

import { IMessageResult } from '../../interfaces/messageResult.interface';

import { SosService } from '../../services/sos.service';

@Component({
    selector: 'sos-details',
    template: require("./sosDetails.component.html")
})
export class SOSDetailsComponent {

    constructor(private _sosService: SosService, private elementRef: ElementRef) { }

    sosId: number;
    sosDetail: any;
    sosDate: string;
    productsView: any[] = [];

    result: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    errorMessage: string;

    isDisabled(): boolean {
        let disabled: boolean = true;

        for (let entry of this.productsView) {
            if (entry.Discard == true) {
                disabled = false;
            }
        }

        return disabled;
    }

    discardOrders(): void {
        let orderIds: number[] = [];
        let customOrderIds: number[] = [];

        for (let entry of this.productsView) {
            if (entry.Custom == false && entry.Discard == true)
            {
                orderIds.push(entry.Id);
            }
            if (entry.Custom == true && entry.Discard == true) {
                customOrderIds.push(entry.Id);
            }
        }

        this._sosService.discardSos(this.sosId,orderIds, customOrderIds)
            .subscribe(result => {
                if (result.isError == false)
                {
                    this.getSosDetail();
                }
            },
            error => this.errorMessage = <any>error);
    }

    getSosDetail(): void {
        this._sosService.getSosDetail(this.sosId)
            .subscribe(result => {
                this.prepareView(result.Result);
            },
            error => this.errorMessage = <any>error);
    }

    prepareView(sosModel: any): void {
        this.productsView = [];
        for (let entry of sosModel.Orders) {
            var productView = {
                "Id": entry.Id,
                "Quantity": entry.Quantity,
                "QuantityDelivered": entry.QuantityDelivered,
                "Price": entry.Price,
                "Name": entry.Product.Name,
                "Unit": entry.Unit,
                "TotalPrice": entry.Price * entry.Quantity,
                "Custom": false,
                "Discarded": entry.Discarded,
                "Discard": false
            };

            this.productsView.push(productView);
        }

        for (let entry of sosModel.CustomOrders) {
            var productView = {
                "Id": entry.Id,
                "Quantity": entry.Quantity,
                "QuantityDelivered": entry.QuantityDelivered,
                "Price": entry.Price,
                "Name": entry.ItemDescription,
                "Unit": entry.Unit,
                "TotalPrice": entry.Price * entry.Quantity,
                "Custom": true,
                "Discarded": entry.Discarded,
                "Discard": false
            };
            this.productsView.push(productView);
        }
    }

    /* Initializer and Native Functions */
    ngOnInit(): void {
        this.sosDetail = JSON.parse(this.elementRef.nativeElement.getAttribute('sos'));
        this.sosId = this.sosDetail.Id;
        this.sosDate = this.elementRef.nativeElement.getAttribute('sosdate');

        this.prepareView(this.sosDetail);
      
    }
}
