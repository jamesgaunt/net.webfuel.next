import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateSupportRequest, SupportRequest, UpdateSupportRequest, TriageSupportRequest, Project, QuerySupportRequest, QueryResult } from './api.types';

@Injectable()
export class SupportRequestApi implements IDataSource<SupportRequest, QuerySupportRequest, CreateSupportRequest, UpdateSupportRequest> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateSupportRequest, options?: ApiOptions): Observable<SupportRequest> {
        return this.apiService.request<CreateSupportRequest, SupportRequest>("POST", "api/support-request", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateSupportRequest, options?: ApiOptions): Observable<SupportRequest> {
        return this.apiService.request<UpdateSupportRequest, SupportRequest>("PUT", "api/support-request", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public triage (body: TriageSupportRequest, options?: ApiOptions): Observable<Project> {
        return this.apiService.request<TriageSupportRequest, Project>("POST", "api/support-request/triage", body, options);
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/support-request/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QuerySupportRequest, options?: ApiOptions): Observable<QueryResult<SupportRequest>> {
        return this.apiService.request<QuerySupportRequest, QueryResult<SupportRequest>>("POST", "api/support-request/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<SupportRequest> {
        return this.apiService.request<undefined, SupportRequest>("GET", "api/support-request/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static supportRequestResolver(param: string): ResolveFn<SupportRequest> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<SupportRequest> => {
            return inject(SupportRequestApi).get({id: route.paramMap.get(param)! });
        };
    }
}
