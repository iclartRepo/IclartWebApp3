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
import { Http, Headers, RequestOptions, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
var AccountService = (function () {
    function AccountService(_http) {
        this._http = _http;
        this.baseUrl = '/Account/';
        this.antiForgeryToken = document.getElementsByName('__RequestVerificationToken')[0];
    }
    AccountService.prototype.getUsers = function () {
        var postedData = {
            '__RequestVerificationToken': this.antiForgeryToken.value
        };
        var headers = new Headers({
            'Content-Type': 'application/x-www-form-urlencoded', 'X-Requested-With': 'XMLHttpRequest'
        });
        var options = new RequestOptions({ headers: headers });
        var params = this.serialize(postedData);
        return this._http.post(this.baseUrl + 'GetUsers', params, options)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    AccountService.prototype.deleteUser = function (id) {
        var postedData = {
            '__RequestVerificationToken': this.antiForgeryToken.value,
            'id': id
        };
        var headers = new Headers({
            'Content-Type': 'application/x-www-form-urlencoded', 'X-Requested-With': 'XMLHttpRequest'
        });
        var options = new RequestOptions({ headers: headers });
        var params = this.serialize(postedData);
        return this._http.post(this.baseUrl + 'DeleteUser', params, options)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    AccountService.prototype.serialize = function (obj) {
        var params = new URLSearchParams();
        for (var key in obj) {
            if (obj.hasOwnProperty(key)) {
                var element = obj[key];
                params.set(key, element);
            }
        }
        return params;
    };
    AccountService.prototype.handleError = function (error) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    };
    return AccountService;
}());
AccountService = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [Http])
], AccountService);
export { AccountService };
//# sourceMappingURL=account.service.js.map