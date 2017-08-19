import { Component, OnInit, ElementRef } from '@angular/core';

import { IMessageResult } from '../../interfaces/messageResult.interface';

import { SosService } from '../../services/sos.service';
@Component({
    selector: 'sos-list',
    template: require("./sosList.component.html")
})
export class SOSListComponent {

    constructor(private _sosService: SosService, private elementRef: ElementRef) { }
    clientName: string;
    status: boolean;
    result: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    resultClients: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    errorMessage: string;

    searchSOS(): void {
        this._sosService.getSosListByName(this.clientName)
            .subscribe(result => {
                this.result = result;
                this.clientName = "";
            },
            error => this.errorMessage = <any>error);
    }

    filterStatus(): void {
        this.getSosListByStatus(this.status);
    }

    getSosListByStatus(status: boolean): void {
        this._sosService.getSosList(this.status)
            .subscribe(result => {
                this.result = result;
            },
            error => this.errorMessage = <any>error);
    }

    /* Initializer and Native Functions */
    ngOnInit(): void {
        this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('sos'));
        this.resultClients = JSON.parse(this.elementRef.nativeElement.getAttribute('clients'));
        this.status = false;
    }
}
