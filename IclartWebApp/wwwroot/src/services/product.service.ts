import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';

import { IMessageResult } from '../interfaces/messageResult.interface';

@Injectable()

export class ProductService {
    private baseUrl: string = "/Product/";
    constructor(private _http: Http) { }

    getProductCategories(): Observable<IMessageResult> {
        return this._http.get(this.baseUrl + "GetProductCategories")
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }
    getPrice(clientId: number, productId: number): Observable<number> {
        return this._http.get(this.baseUrl + "GetPrice?clientId=" + clientId + "&productId=" + productId)
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }
    addProductCategory(name: string): Observable<IMessageResult> {
        return this._http.post(this.baseUrl + "AddProductCategory", { name: name })
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }
    updateProductCategory(id: number, name: string): Observable<IMessageResult> {
        return this._http.put(this.baseUrl + "UpdateProductCategory", { id: id, name: name })
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }
    deleteProductCategory(id: number): Observable<IMessageResult> {
        return this._http.delete(this.baseUrl + "DeleteProductCategory?id=" + id)
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    getProducts(): Observable<IMessageResult> {
        return this._http.get(this.baseUrl + "GetProducts")
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }
    getProduct(id: number): Observable<IMessageResult> {
        return this._http.get(this.baseUrl + "GetProduct?id=" + id)
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }
    searchProduct(name: string): Observable<IMessageResult> {
        return this._http.get(this.baseUrl + "SearchProduct?name=" + name)
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }
    filterProducts(name: string): Observable<IMessageResult> {
        return this._http.get(this.baseUrl + "FilterProducts?name=" + name)
            .map((response: Response) => <IMessageResult>response.json())
            .catch(this.handleError);
    }
    addProduct(product: any): Observable<IMessageResult> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(this.baseUrl + "AddProduct", { newProduct: product }, options)
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }
    updateProduct(id: number, product: any): Observable<IMessageResult> {
        return this._http.put(this.baseUrl + "UpdateProduct", { id: id, product: product })
            .map((response: Response) => <IMessageResult>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }
    deleteProduct(id: number): Observable<IMessageResult> {
        return this._http.delete(this.baseUrl + "DeleteProduct?id=" + id)
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

