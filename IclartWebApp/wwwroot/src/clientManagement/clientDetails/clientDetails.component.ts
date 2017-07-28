import { Component, OnInit, ElementRef } from '@angular/core';

import { ClientService } from '../../services/client.service';
import { IMessageResult } from '../../interfaces/messageResult.interface';

@Component({
    selector: 'client-details',
    template: require('./clientDetails.component.html')
})
export class ClientViewComponent implements OnInit {

    tabNum: number = 1;
    returnUrl: string;

    result: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    errorMessage: string;

    clientId: number;

    constructor(private _clientService: ClientService, private elementRef:ElementRef) {
    }

    /* Navigation Functions */
    //back(): void {
    //    this._router.navigate(['/clients']);
    //}

    //editClient(id: number): void {
    //    this._router.navigate(['/client-form', id]);
    //}

    /* Pagination Functions */
    setTab(tabNumber: number): void {
        this.tabNum = tabNumber;
    }


    /* Function to Get Client Info */
    getClient(id: number) {
        this._clientService.getClientInfo(id).subscribe(
            result => this.result = result,
            error => this.errorMessage = <any>error);
    }

    /* Initializer and Native Functions */
    ngOnInit(): void {
        this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('client'));
        this.returnUrl = this.elementRef.nativeElement.getAttribute('returnurl');
    }
}
