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
import { ProductService } from '../../services/product.service';
var ProductDetailsComponent = (function () {
    function ProductDetailsComponent(_service, elementRef) {
        this._service = _service;
        this.elementRef = elementRef;
        this.result = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
    }
    ProductDetailsComponent.prototype.getProduct = function (id) {
        var _this = this;
        this._service.getProduct(id).subscribe(function (result) {
            _this.result = result;
        }, function (error) { return _this.errorMessage = error; });
    };
    ProductDetailsComponent.prototype.ngOnInit = function () {
        this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('product'));
        this.returnUrl = this.elementRef.nativeElement.getAttribute('returnurl');
    };
    return ProductDetailsComponent;
}());
ProductDetailsComponent = __decorate([
    Component({
        selector: 'product-details',
        template: require("./productDetails.component.html")
    }),
    __metadata("design:paramtypes", [ProductService, ElementRef])
], ProductDetailsComponent);
export { ProductDetailsComponent };
//# sourceMappingURL=productDetails.component.js.map