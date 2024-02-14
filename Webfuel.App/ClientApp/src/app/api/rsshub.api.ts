import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateRSSHub, RSSHub, UpdateRSSHub, SortRSSHub, QueryRSSHub, QueryResult } from './api.types';

@Injectable()
export class RSSHubApi implements IDataSource<RSSHub, QueryRSSHub, CreateRSSHub, UpdateRSSHub> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateRSSHub, options?: ApiOptions): Observable<RSSHub> {
        return this.apiService.request<CreateRSSHub, RSSHub>("POST", "api/rss-hub", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateRSSHub, options?: ApiOptions): Observable<RSSHub> {
        return this.apiService.request<UpdateRSSHub, RSSHub>("PUT", "api/rss-hub", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortRSSHub, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortRSSHub, any>("PUT", "api/rss-hub/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/rss-hub/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryRSSHub, options?: ApiOptions): Observable<QueryResult<RSSHub>> {
        return this.apiService.request<QueryRSSHub, QueryResult<RSSHub>>("POST", "api/rss-hub/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

