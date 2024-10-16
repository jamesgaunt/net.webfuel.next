import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateWidget, Widget, UpdateWidget, SortWidget, WidgetType } from './api.types';

@Injectable()
export class WidgetApi {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateWidget, options?: ApiOptions): Observable<Widget> {
        return this.apiService.request<CreateWidget, Widget>("POST", "api/widget", body, options);
    }
    
    public update (body: UpdateWidget, options?: ApiOptions): Observable<Widget> {
        return this.apiService.request<UpdateWidget, Widget>("PUT", "api/widget", body, options);
    }
    
    public sort (body: SortWidget, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortWidget, any>("PUT", "api/widget/sort", body, options);
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/widget/" + params.id + "", undefined, options);
    }
    
    public refresh (params: { id: string }, options?: ApiOptions): Observable<Widget> {
        return this.apiService.request<undefined, Widget>("POST", "api/widget/refresh/" + params.id + "", undefined, options);
    }
    
    public selectActive (options?: ApiOptions): Observable<Array<Widget>> {
        return this.apiService.request<undefined, Array<Widget>>("GET", "api/widget/select-active", undefined, options);
    }
    
    public selectAvailableType (options?: ApiOptions): Observable<Array<WidgetType>> {
        return this.apiService.request<undefined, Array<WidgetType>>("GET", "api/widget/select-available-type", undefined, options);
    }
}

