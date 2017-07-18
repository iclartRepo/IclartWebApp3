import { Component, ElementRef } from '@angular/core';

import { IMessageResult } from '../interfaces/messageResult.interface';

import { AccountService } from '../services/account.service';
@Component({
    selector: 'login-accounts',
    template: require('./manageLogins.component.html')
})

export class ManageLoginComponent {
    currentUsers: any;
    accountToDelete: string;
    accountSearch: string;
    errorMessage: string;
    constructor(private elementRef: ElementRef, private _authService: AccountService) {
        this.currentUsers = JSON.parse(this.elementRef.nativeElement.getAttribute('users'));
        console.log(this.currentUsers);
    }

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

    setUser(id: string): void {
        this.accountToDelete = id;
    }

    retrieveAccounts(): void {
        this._authService.getUsers()
            .subscribe(users => {
                this.result = users;
                this.currentUsers = this.result.ResultList;
            },
            error => this.errorMessage = <any>error);
    }

    deleteAccount(): void {
        this._authService.deleteUser(this.accountToDelete)
            .subscribe(deleteResponse => {
                this.resultDeletion = deleteResponse;
                if (this.resultDeletion.isError == false) {
                    this.retrieveAccounts();
                }
            },
            error => this.errorMessage = <any>error);
    }

    searchAccount(): void {
        this._authService.searchUser(this.accountSearch)
            .subscribe(users => {
                this.result = users;
                this.currentUsers = this.result.ResultList;
            },
            error => this.errorMessage = <any>error);
    }
 
}