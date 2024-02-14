import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsPaidRSSAdviserCoapplicant, QueryResult, IsPaidRSSAdviserCoapplicant } from './api.types';

@Injectable()
export class IsPaidRSSAdviserCoapplicantApi implements IDataSource<IsPaidRSSAdviserCoapplicant, QueryIsPaidRSSAdviserCoapplicant, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsPaidRSSAdviserCoapplicant, options?: ApiOptions): Observable<QueryResult<IsPaidRSSAdviserCoapplicant>> {
        return this.apiService.request<QueryIsPaidRSSAdviserCoapplicant, QueryResult<IsPaidRSSAdviserCoapplicant>>("POST", "api/is-paid-rss-adviser-coapplicant/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

