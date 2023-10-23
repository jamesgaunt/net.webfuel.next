import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsFellowship, QueryResult, IsFellowship } from './api.types';

@Injectable()
export class IsFellowshipApi implements IDataSource<IsFellowship, QueryIsFellowship, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsFellowship, options?: ApiOptions): Observable<QueryResult<IsFellowship>> {
        return this.apiService.request<QueryIsFellowship, QueryResult<IsFellowship>>("POST", "api/is-fellowship/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

