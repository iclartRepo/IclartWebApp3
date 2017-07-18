import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';

import { IMessageResult } from '../interfaces/messageResult.interface';

@Injectable()

export class AccountService {
    private baseUrl = '/Account/';
    antiForgeryToken: any;
    constructor(private _http: Http) {
        this.antiForgeryToken = document.getElementsByName('__RequestVerificationToken')[0];
    }

    getUsers(): Observable<IMessageResult> {
        let postedData = {
            '__RequestVerificationToken': this.antiForgeryToken.value
        };
        let headers = new Headers({
            'Content-Type': 'application/x-www-form-urlencoded', 'X-Requested-With': 'XMLHttpRequest'
        });
        let options = new RequestOptions({ headers: headers });
        let params: URLSearchParams = this.serialize(postedData);
        return this._http.post(this.baseUrl + 'GetUsers', params, options)
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    searchUser(userName: string): Observable<IMessageResult> {
        var postedData = {
            "__RequestVerificationToken": this.antiForgeryToken.value,
            "userName": userName
        };
        let headers = new Headers({
            'Content-Type': 'application/x-www-form-urlencoded', 'X-Requested-With': 'XMLHttpRequest'
        });
        let options = new RequestOptions({ headers: headers });
        let params: URLSearchParams = this.serialize(postedData);
        return this._http.post(this.baseUrl + "SearchUsers", params, options)
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    deleteUser(id: string): Observable<IMessageResult> {
        let postedData = {
            '__RequestVerificationToken': this.antiForgeryToken.value,
            'id': id
        };
        let headers = new Headers({
            'Content-Type': 'application/x-www-form-urlencoded', 'X-Requested-With': 'XMLHttpRequest'
        });
        let options = new RequestOptions({ headers: headers });
        let params: URLSearchParams = this.serialize(postedData);
        return this._http.post(this.baseUrl + 'DeleteUser', params, options)
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    serialize(obj: any) {
        let params: URLSearchParams = new URLSearchParams();
        for (let key in obj) {

            if (obj.hasOwnProperty(key)) {
                let element = obj[key];

                params.set(key, element);
            }


        }

        return params;
    }
    private handleError(error: Response) {
        // in a real world app, we may send the server to some remote logging infrastructure
        // instead of just logging it to the console
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}