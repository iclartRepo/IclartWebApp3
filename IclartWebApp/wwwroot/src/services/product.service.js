var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
var ProductService = (function () {
    function ProductService(_http) {
        this._http = _http;
        this.baseUrl = "/Product/";
    }
    ProductService.prototype.getProductCategories = function () {
        return this._http.get(this.baseUrl + "GetProductCategories")
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ProductService.prototype.getPrice = function (clientId, productId) {
        return this._http.get(this.baseUrl + "GetPrice?clientId=" + clientId + "&productId=" + productId)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ProductService.prototype.addProductCategory = function (name) {
        return this._http.post(this.baseUrl + "AddProductCategory", { name: name })
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    ProductService.prototype.updateProductCategory = function (id, name) {
        return this._http.put(this.baseUrl + "UpdateProductCategory", { id: id, name: name })
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    ProductService.prototype.deleteProductCategory = function (id) {
        return this._http.delete(this.baseUrl + "DeleteProductCategory?id=" + id)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    ProductService.prototype.getProducts = function () {
        return this._http.get(this.baseUrl + "GetProducts")
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ProductService.prototype.getProduct = function (id) {
        return this._http.get(this.baseUrl + "GetProduct?id=" + id)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ProductService.prototype.searchProduct = function (name) {
        return this._http.get(this.baseUrl + "SearchProduct?name=" + name)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ProductService.prototype.filterProducts = function (name) {
        return this._http.get(this.baseUrl + "FilterProducts?name=" + name)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ProductService.prototype.addProduct = function (product) {
        var headers = new Headers({ 'Content-Type': 'application/json' });
        var options = new RequestOptions({ headers: headers });
        return this._http.post(this.baseUrl + "AddProduct", { newProduct: product }, options)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    ProductService.prototype.updateProduct = function (id, product) {
        return this._http.put(this.baseUrl + "UpdateProduct", { id: id, product: product })
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    ProductService.prototype.deleteProduct = function (id) {
        return this._http.delete(this.baseUrl + "DeleteProduct?id=" + id)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    ProductService.prototype.handleError = function (error) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    };
    return ProductService;
}());
ProductService = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [Http])
], ProductService);
export { ProductService };
//# sourceMappingURL=product.service.js.map