import { Component, OnInit, ElementRef } from '@angular/core';

import { ClientService } from '../services/client.service';
import { IMessageResult } from '../interfaces/messageResult.interface';

@Component({
    selector: 'client-list',
    template: require('./manageClients.component.html')
})
export class ManageClientsComponent implements OnInit {
    clientToDelete: number;
    clientName: string = "";
    result: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    resultDeletion: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    errorMessage: string;

    constructor(private elementRef: ElementRef, private _clientService: ClientService) {
    }

    /* CRUD Functionalities */
 
    deleteClient(): void {
        this._clientService.deleteClient(this.clientToDelete)
            .subscribe(deleteResponse => {
                this.resultDeletion = deleteResponse;
                if (this.resultDeletion.isError == false) {
                    this.getListClients();
                }
            },
            error => this.errorMessage = <any>error);
    }
    getListClients(): void {
        this._clientService.getClients()
            .subscribe(peoples => this.result = peoples,
            error => this.errorMessage = <any>error);
    }
    searchClient(): void {
        this._clientService.searchClients(this.clientName)
            .subscribe(peoples => {
                this.result = peoples;
                this.clientName = "";
            },
            error => this.errorMessage = <any>error);
    }
    setClient(id: number): void {
        this.clientToDelete = id;
    }
    /* Initialize Functions */
    ngOnInit(): void {
        //this._clientService.getClients()
        //    .subscribe(peoples => this.result = peoples,
        //    error => this.errorMessage = <any>error);
        this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('clients'));
    }
}
