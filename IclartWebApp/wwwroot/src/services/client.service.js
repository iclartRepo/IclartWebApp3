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
var ClientService = (function () {
    function ClientService(_http) {
        this._http = _http;
        this.baseUrl = "/Client/";
    }
    ClientService.prototype.getClients = function () {
        return this._http.get(this.baseUrl + "GetClientList")
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ClientService.prototype.searchClients = function (name) {
        return this._http.get(this.baseUrl + "SearchClient?clientName=" + name)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ClientService.prototype.getClientInfo = function (id) {
        return this._http.get(this.baseUrl + "GetClientInfo?id=" + id)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    ClientService.prototype.addClient = function (client) {
        var headers = new Headers({ 'Content-Type': 'application/json' });
        var options = new RequestOptions({ headers: headers });
        return this._http.post(this.baseUrl + "AddClient", { client: client }, options)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    ClientService.prototype.updateClient = function (client) {
        var headers = new Headers({ 'Content-Type': 'application/json' });
        var options = new RequestOptions({ headers: headers });
        return this._http.put(this.baseUrl + "UpdateClient", { client: client }, options)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    ClientService.prototype.deleteClient = function (id) {
        return this._http.delete(this.baseUrl + "DeleteClient?id=" + id)
            .map(function (response) { return response.json(); })
            .do(function (data) { return console.log('All: ' + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    ClientService.prototype.handleError = function (error) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    };
    return ClientService;
}());
ClientService = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [Http])
], ClientService);
export { ClientService };
//# sourceMappingURL=client.service.js.map