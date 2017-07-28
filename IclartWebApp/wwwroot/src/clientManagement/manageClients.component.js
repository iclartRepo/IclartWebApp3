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
import { ClientService } from '../services/client.service';
var ManageClientsComponent = (function () {
    function ManageClientsComponent(elementRef, _clientService) {
        this.elementRef = elementRef;
        this._clientService = _clientService;
        this.clientName = "";
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
    }
    ManageClientsComponent.prototype.deleteClient = function () {
        var _this = this;
        this._clientService.deleteClient(this.clientToDelete)
            .subscribe(function (deleteResponse) {
            _this.resultDeletion = deleteResponse;
            if (_this.resultDeletion.isError == false) {
                _this.getListClients();
            }
        }, function (error) { return _this.errorMessage = error; });
    };
    ManageClientsComponent.prototype.getListClients = function () {
        var _this = this;
        this._clientService.getClients()
            .subscribe(function (peoples) { return _this.result = peoples; }, function (error) { return _this.errorMessage = error; });
    };
    ManageClientsComponent.prototype.searchClient = function () {
        var _this = this;
        this._clientService.searchClients(this.clientName)
            .subscribe(function (peoples) {
            _this.result = peoples;
            _this.clientName = "";
        }, function (error) { return _this.errorMessage = error; });
    };
    ManageClientsComponent.prototype.setClient = function (id) {
        this.clientToDelete = id;
    };
    ManageClientsComponent.prototype.ngOnInit = function () {
        this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('clients'));
    };
    return ManageClientsComponent;
}());
ManageClientsComponent = __decorate([
    Component({
        selector: 'client-list',
        template: require('./manageClients.component.html')
    }),
    __metadata("design:paramtypes", [ElementRef, ClientService])
], ManageClientsComponent);
export { ManageClientsComponent };
//# sourceMappingURL=manageClients.component.js.map