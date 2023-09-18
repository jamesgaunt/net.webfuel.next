import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService, ApiOptions } from '../core/api.service';
import { ICreateWidget, IWidget, IUpdateWidget, IQueryWidget, IQueryResult, IWidgetQueryView } from './api.types';

@Injectable()
export class WidgetApi {
    constructor(private apiService: ApiService) { }
    
    public createWidget (body: ICreateWidget, options?: ApiOptions): Observable<IWidget> {
        return this.apiService.request("POST", "api/create-widget", body, options);
    }
    
    public updateWidget (body: IUpdateWidget, options?: ApiOptions): Observable<IWidget> {
        return this.apiService.request("PUT", "api/update-widget", body, options);
    }
    
    public deleteWidget (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/delete-widget/" + params.id + "", undefined, options);
    }
    
    public queryWidget (body: IQueryWidget, options?: ApiOptions): Observable<IQueryResult<IWidgetQueryView>> {
        return this.apiService.request("POST", "api/query-widget", body, options);
    }
}

