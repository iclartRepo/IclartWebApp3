import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';

import { IMessageResult } from '../interfaces/messageResult.interface';

@Injectable()

export class SosService {
    private baseUrl: string = "/SOS/";
    constructor(private _http: Http) {
    }

    getSosList(): Observable<IMessageResult> {
        return this._http.get(this.baseUrl + "GetSOSList")
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }

    getSosDetail(id: number): Observable<IMessageResult> {
        return this._http.get(this.baseUrl + "GetSOSDetail?id=" + id)
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }

    getCustomProducts(clientId: number): Observable<IMessageResult> {
        return this._http.get(this.baseUrl + "GetListCustomProducts?clientId=" + clientId)
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }

    addSos(sos: any): Observable<IMessageResult> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(this.baseUrl + "AddSos", { model: sos }, options)
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    discardSos(sosId:number, orderIds: number[], customOrderIds: number[]): Observable<IMessageResult> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(this.baseUrl + "DiscardOrders", { sosId: sosId, orderIds: orderIds, customOrderIds: customOrderIds }, options)
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        // in a real world app, we may send the server to some remote logging infrastructure
        // instead of just logging it to the console
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }


}

