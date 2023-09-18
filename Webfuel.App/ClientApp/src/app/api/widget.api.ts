import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService, ApiOptions } from '../core/api.service';
import { ICreateWidgetCommand, IWidget, IUpdateWidgetCommand, IQueryWidgetCommand, IQueryResult } from './api.types';

@Injectable()
export class WidgetApi {
    constructor(private apiService: ApiService) { }
    
    public createWidget (body: ICreateWidgetCommand, options?: ApiOptions): Observable<IWidget> {
        return this.apiService.request("POST", "api/create-widget", body, options);
    }
    
    public updateWidget (body: IUpdateWidgetCommand, options?: ApiOptions): Observable<IWidget> {
        return this.apiService.request("PUT", "api/update-widget", body, options);
    }
    
    public deleteWidget (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/delete-widget/" + params.id + "", undefined, options);
    }
    
    public queryWidget (body: IQueryWidgetCommand, options?: ApiOptions): Observable<IQueryResult<IWidget>> {
        return this.apiService.request("POST", "api/query-widget", body, options);
    }
}

