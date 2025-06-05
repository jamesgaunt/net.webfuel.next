import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryRSSHub, QueryResult, RSSHub } from './api.types';

@Injectable()
export class RSSHubApi implements IDataSource<RSSHub, QueryRSSHub, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryRSSHub, options?: ApiOptions): Observable<QueryResult<RSSHub>> {
        return this.apiService.request<QueryRSSHub, QueryResult<RSSHub>>("POST", "api/rss-hub/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

