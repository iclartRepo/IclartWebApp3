var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ElementRef } from '@angular/core';
import { AccountService } from '../services/account.service';
var ManageLoginComponent = (function () {
    function ManageLoginComponent(elementRef, _authService) {
        this.elementRef = elementRef;
        this._authService = _authService;
        this.result = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.resultDeletion = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.currentUsers = JSON.parse(this.elementRef.nativeElement.getAttribute('users'));
        console.log(this.currentUsers);
    }
    ManageLoginComponent.prototype.setUser = function (id) {
        this.accountToDelete = id;
    };
    ManageLoginComponent.prototype.retrieveAccounts = function () {
        var _this = this;
        this._authService.getUsers()
            .subscribe(function (users) {
            _this.result = users;
            _this.currentUsers = _this.result.ResultList;
        }, function (error) { return _this.errorMessage = error; });
    };
    ManageLoginComponent.prototype.deleteAccount = function () {
        var _this = this;
        this._authService.deleteUser(this.accountToDelete)
            .subscribe(function (deleteResponse) {
            _this.resultDeletion = deleteResponse;
            if (_this.resultDeletion.isError == false) {
                _this.retrieveAccounts();
            }
        }, function (error) { return _this.errorMessage = error; });
    };
    return ManageLoginComponent;
}());
ManageLoginComponent = __decorate([
    Component({
        selector: 'login-accounts',
        template: require('./manageLogins.component.html')
    }),
    __metadata("design:paramtypes", [ElementRef, AccountService])
], ManageLoginComponent);
export { ManageLoginComponent };
//# sourceMappingURL=manageLogins.component.js.map