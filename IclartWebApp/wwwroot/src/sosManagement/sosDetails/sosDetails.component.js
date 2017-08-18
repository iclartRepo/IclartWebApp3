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
import { SosService } from '../../services/sos.service';
var SOSDetailsComponent = (function () {
    function SOSDetailsComponent(_sosService, elementRef) {
        this._sosService = _sosService;
        this.elementRef = elementRef;
        this.productsView = [];
        this.discardOrder = {};
        this.discardType = {};
        this.result = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
    }
    SOSDetailsComponent.prototype.ngOnInit = function () {
        this.sosDetail = JSON.parse(this.elementRef.nativeElement.getAttribute('sos'));
        this.sosDate = this.elementRef.nativeElement.getAttribute('sosdate');
        for (var _i = 0, _a = this.sosDetail.Orders; _i < _a.length; _i++) {
            var entry = _a[_i];
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
        for (var _b = 0, _c = this.sosDetail.CustomOrders; _b < _c.length; _b++) {
            var entry = _c[_b];
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
    };
    return SOSDetailsComponent;
}());
SOSDetailsComponent = __decorate([
    Component({
        selector: 'sos-details',
        template: require("./sosDetails.component.html")
    }),
    __metadata("design:paramtypes", [SosService, ElementRef])
], SOSDetailsComponent);
export { SOSDetailsComponent };
//# sourceMappingURL=sosDetails.component.js.map