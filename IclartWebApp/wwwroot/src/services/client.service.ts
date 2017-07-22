import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';

import { IClient } from '../interfaces/client.interface';
import { IMessageResult } from '../interfaces/messageResult.interface';

@Injectable()

export class ClientService {
    private baseUrl: string = "/Client/";
    constructor(private _http: Http) { }

    getClients(): Observable<IMessageResult> {
        return this._http.get(this.baseUrl + "GetClientList")
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }
    searchClients(name: string): Observable<IMessageResult> {
        return this._http.get(this.baseUrl + "SearchClient?clientName=" + name)
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }
    getClientInfo(id: number): Observable<IMessageResult> {
        return this._http.get(this.baseUrl + "GetClientInfo?id=" + id)
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }

    addClient(client: any): Observable<IMessageResult> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(this.baseUrl + "AddClient", { client }, options)
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    updateClient(client: IClient): Observable<IMessageResult> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.put(this.baseUrl + "UpdateClient", { client }, options)
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    deleteClient(id: number): Observable<IMessageResult> {
        return this._http.delete(this.baseUrl + "DeleteClient?id=" + id)
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

