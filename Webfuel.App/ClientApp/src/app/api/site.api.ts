import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateSite, Site, UpdateSite, SortSite, QuerySite, QueryResult } from './api.types';

@Injectable()
export class SiteApi implements IDataSource<Site, QuerySite, CreateSite, UpdateSite> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateSite, options?: ApiOptions): Observable<Site> {
        return this.apiService.request<CreateSite, Site>("POST", "api/site", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateSite, options?: ApiOptions): Observable<Site> {
        return this.apiService.request<UpdateSite, Site>("PUT", "api/site", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortSite, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortSite, any>("PUT", "api/site/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/site/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QuerySite, options?: ApiOptions): Observable<QueryResult<Site>> {
        return this.apiService.request<QuerySite, QueryResult<Site>>("POST", "api/site/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

