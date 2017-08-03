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
var ProductListComponent = (function () {
    function ProductListComponent(_service, elementRef) {
        this._service = _service;
        this.elementRef = elementRef;
        this.productName = "";
        this.categoryFilter = "Select Product Category";
        this.categories = [];
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
    ProductListComponent.prototype.searchProduct = function () {
        var _this = this;
        this.categoryFilter = "Select Product Category";
        this._service.searchProduct(this.productName)
            .subscribe(function (product) {
            _this.result = product;
            _this.productName = "";
        }, function (error) { return _this.errorMessage = error; });
    };
    ProductListComponent.prototype.setProduct = function (id) {
        this.productToDelete = id;
    };
    ProductListComponent.prototype.deleteProduct = function () {
        var _this = this;
        this._service.deleteProduct(this.productToDelete)
            .subscribe(function (products) { _this.getProducts(); }, function (error) { return _this.errorMessage = error; });
    };
    ProductListComponent.prototype.getProducts = function () {
        var _this = this;
        this._service.getProducts()
            .subscribe(function (products) { return _this.result = products; }, function (error) { return _this.errorMessage = error; });
    };
    ProductListComponent.prototype.clearFilter = function () {
        this.categoryFilter = "Select Product Category";
        this.getProducts();
    };
    ProductListComponent.prototype.filterProducts = function () {
        var _this = this;
        if (this.categoryFilter == "Select Product Category") {
            this.getProducts();
        }
        else {
            this._service.filterProducts(this.categoryFilter)
                .subscribe(function (products) { return _this.result = products; }, function (error) { return _this.errorMessage = error; });
        }
    };
    ProductListComponent.prototype.checkIfExists = function (name) {
        if (this.categories.length == 0) {
            return false;
        }
        else {
            var exists = false;
            for (var _i = 0, _a = this.categories; _i < _a.length; _i++) {
                var entry = _a[_i];
                if (entry == name) {
                    exists = true;
                }
            }
            return exists;
        }
    };
    ProductListComponent.prototype.ngOnInit = function () {
        this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('products'));
        if (this.result.ResultList != null) {
            for (var _i = 0, _a = this.result.ResultList; _i < _a.length; _i++) {
                var entry = _a[_i];
                var exists = this.checkIfExists(entry.ProductCategory.Name);
                if (exists == false) {
                    this.categories.push(entry.ProductCategory.Name);
                }
            }
        }
    };
    return ProductListComponent;
}());
ProductListComponent = __decorate([
    Component({
        selector: 'product-list',
        template: require('./productList.component.html')
    }),
    __metadata("design:paramtypes", [ProductService, ElementRef])
], ProductListComponent);
export { ProductListComponent };
//# sourceMappingURL=productList.component.js.map