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
var SosService = (function () {
    function SosService(_http) {
        this._http = _http;
        this.baseUrl = "/SOS/";
    }
    SosService.prototype.getSosList = function (status) {
        return this._http.get(this.baseUrl + "GetSOSList?status=" + status)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    SosService.prototype.getSosListByName = function (clientName) {
        return this._http.get(this.baseUrl + "GetSOSListByClient?name=" + clientName)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    SosService.prototype.getSosDetail = function (id) {
        return this._http.get(this.baseUrl + "GetSOSDetail?id=" + id)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    SosService.prototype.getCustomProducts = function (clientId) {
        return this._http.get(this.baseUrl + "GetListCustomProducts?clientId=" + clientId)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    SosService.prototype.addSos = function (sos) {
        var headers = new Headers({ 'Content-Type': 'application/json' });
        var options = new RequestOptions({ headers: headers });
        return this._http.post(this.baseUrl + "AddSos", { model: sos }, options)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    SosService.prototype.discardSos = function (sosId, orderIds, customOrderIds) {
        var headers = new Headers({ 'Content-Type': 'application/json' });
        var options = new RequestOptions({ headers: headers });
        return this._http.post(this.baseUrl + "DiscardOrders", { sosId: sosId, orderIds: orderIds, customOrderIds: customOrderIds }, options)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    SosService.prototype.updateSos = function (sos) {
        var headers = new Headers({ 'Content-Type': 'application/json' });
        var options = new RequestOptions({ headers: headers });
        return this._http.put(this.baseUrl + "UpdateSOS", { model: sos }, options)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    SosService.prototype.handleError = function (error) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    };
    return SosService;
}());
SosService = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [Http])
], SosService);
export { SosService };
//# sourceMappingURL=sos.service.js.map