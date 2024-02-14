import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsPrePostAward, QueryResult, IsPrePostAward } from './api.types';

@Injectable()
export class IsPrePostAwardApi implements IDataSource<IsPrePostAward, QueryIsPrePostAward, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsPrePostAward, options?: ApiOptions): Observable<QueryResult<IsPrePostAward>> {
        return this.apiService.request<QueryIsPrePostAward, QueryResult<IsPrePostAward>>("POST", "api/is-pre-post-award/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

