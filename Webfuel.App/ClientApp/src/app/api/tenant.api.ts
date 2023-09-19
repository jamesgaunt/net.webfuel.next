import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService, ApiOptions } from '../core/api.service';
import { ICreateTenant, ITenant, IUpdateTenant, IQueryTenant, IQueryResult } from './api.types';

@Injectable()
export class TenantApi {
    constructor(private apiService: ApiService) { }
    
    public createTenant (body: ICreateTenant, options?: ApiOptions): Observable<ITenant> {
        return this.apiService.request("POST", "api/create-tenant", body, options);
    }
    
    public updateTenant (body: IUpdateTenant, options?: ApiOptions): Observable<ITenant> {
        return this.apiService.request("PUT", "api/update-tenant", body, options);
    }
    
    public deleteTenant (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/delete-tenant/" + params.id + "", undefined, options);
    }
    
    public queryTenant (body: IQueryTenant, options?: ApiOptions): Observable<IQueryResult<ITenant>> {
        return this.apiService.request("POST", "api/query-tenant", body, options);
    }
    
    public resolveTenant (params: { id: string }, options?: ApiOptions): Observable<ITenant> {
        return this.apiService.request("GET", "api/resolve-tenant/" + params.id + "", undefined, options);
    }
}

