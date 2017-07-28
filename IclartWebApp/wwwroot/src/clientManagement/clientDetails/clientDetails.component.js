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
import { ClientService } from '../../services/client.service';
var ClientViewComponent = (function () {
    function ClientViewComponent(_clientService, elementRef) {
        this._clientService = _clientService;
        this.elementRef = elementRef;
        this.tabNum = 1;
        this.result = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
    }
    ClientViewComponent.prototype.setTab = function (tabNumber) {
        this.tabNum = tabNumber;
    };
    ClientViewComponent.prototype.getClient = function (id) {
        var _this = this;
        this._clientService.getClientInfo(id).subscribe(function (result) { return _this.result = result; }, function (error) { return _this.errorMessage = error; });
    };
    ClientViewComponent.prototype.ngOnInit = function () {
        this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('client'));
        this.returnUrl = this.elementRef.nativeElement.getAttribute('returnurl');
    };
    return ClientViewComponent;
}());
ClientViewComponent = __decorate([
    Component({
        selector: 'client-details',
        template: require('./clientDetails.component.html')
    }),
    __metadata("design:paramtypes", [ClientService, ElementRef])
], ClientViewComponent);
export { ClientViewComponent };
//# sourceMappingURL=clientDetails.component.js.map