import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService, ApiOptions } from '../core/api.service';
import { ICreateTenantDomain, ITenantDomain, IUpdateTenantDomain, IQueryTenantDomain, IQueryResult } from './api.types';

@Injectable()
export class TenantDomainApi {
    constructor(private apiService: ApiService) { }
    
    public createTenantDomain (body: ICreateTenantDomain, options?: ApiOptions): Observable<ITenantDomain> {
        return this.apiService.request("POST", "api/create-tenant-domain", body, options);
    }
    
    public updateTenantDomain (body: IUpdateTenantDomain, options?: ApiOptions): Observable<ITenantDomain> {
        return this.apiService.request("PUT", "api/update-tenant-domain", body, options);
    }
    
    public deleteTenantDomain (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/delete-tenant-domain/" + params.id + "", undefined, options);
    }
    
    public queryTenantDomain (body: IQueryTenantDomain, options?: ApiOptions): Observable<IQueryResult<ITenantDomain>> {
        return this.apiService.request("POST", "api/query-tenant-domain", body, options);
    }
}

