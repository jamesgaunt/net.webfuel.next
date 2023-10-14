import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateFundingBody, FundingBody, UpdateFundingBody, SortFundingBody, QueryFundingBody, QueryResult } from './api.types';

@Injectable()
export class FundingBodyApi implements IDataSource<FundingBody, QueryFundingBody, CreateFundingBody, UpdateFundingBody> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateFundingBody, options?: ApiOptions): Observable<FundingBody> {
        return this.apiService.request<CreateFundingBody, FundingBody>("POST", "api/funding-body", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateFundingBody, options?: ApiOptions): Observable<FundingBody> {
        return this.apiService.request<UpdateFundingBody, FundingBody>("PUT", "api/funding-body", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortFundingBody, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortFundingBody, any>("PUT", "api/funding-body/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/funding-body/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryFundingBody, options?: ApiOptions): Observable<QueryResult<FundingBody>> {
        return this.apiService.request<QueryFundingBody, QueryResult<FundingBody>>("POST", "api/funding-body/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

