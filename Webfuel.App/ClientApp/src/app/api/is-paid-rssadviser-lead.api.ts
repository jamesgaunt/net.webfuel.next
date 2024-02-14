import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsPaidRSSAdviserLead, QueryResult, IsPaidRSSAdviserLead } from './api.types';

@Injectable()
export class IsPaidRSSAdviserLeadApi implements IDataSource<IsPaidRSSAdviserLead, QueryIsPaidRSSAdviserLead, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsPaidRSSAdviserLead, options?: ApiOptions): Observable<QueryResult<IsPaidRSSAdviserLead>> {
        return this.apiService.request<QueryIsPaidRSSAdviserLead, QueryResult<IsPaidRSSAdviserLead>>("POST", "api/is-paid-rss-adviser-lead/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

