import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService, ApiOptions } from '../core/api.service';
import { IWidget, ISimpleQuery, IQueryResult } from './api.types';

@Injectable()
export class WidgetApi {
    constructor(private apiService: ApiService) { }
    
    public get (params: { widgetId: string }, options?: ApiOptions): Observable<IWidget> {
        options = options || {};
        return this.apiService.GET("api/Widget/" + params.widgetId + "?r=" + Math.random(), options).pipe(map((res) => <IWidget>res.body));
    }
    
    public insert (params: { widget: IWidget }, options?: ApiOptions): Observable<IWidget> {
        options = options || {};
        return this.apiService.POST("api/Widget?r=" + Math.random(), params.widget, options).pipe(map((res) => <IWidget>res.body));
    }
    
    public update (params: { widget: IWidget }, options?: ApiOptions): Observable<IWidget> {
        options = options || {};
        return this.apiService.PUT("api/Widget?r=" + Math.random(), params.widget, options).pipe(map((res) => <IWidget>res.body));
    }
    
    public delete (params: { widgetId: string }, options?: ApiOptions): Observable<any> {
        options = options || {};
        return this.apiService.DELETE("api/Widget/" + params.widgetId + "?r=" + Math.random(), options);
    }
    
    public query (params: { query: ISimpleQuery }, options?: ApiOptions): Observable<IQueryResult<IWidget>> {
        options = options || {};
        options.retryCount = options.retryCount || 3;
        return this.apiService.POST("api/Widget/query?r=" + Math.random(), params.query, options).pipe(map((res) => <IQueryResult<IWidget>>res.body));
    }
}

