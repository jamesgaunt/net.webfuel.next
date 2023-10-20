import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateFundingCallType, FundingCallType, UpdateFundingCallType, SortFundingCallType, QueryFundingCallType, QueryResult } from './api.types';

@Injectable()
export class FundingCallTypeApi implements IDataSource<FundingCallType, QueryFundingCallType, CreateFundingCallType, UpdateFundingCallType> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateFundingCallType, options?: ApiOptions): Observable<FundingCallType> {
        return this.apiService.request<CreateFundingCallType, FundingCallType>("POST", "api/funding-call-type", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateFundingCallType, options?: ApiOptions): Observable<FundingCallType> {
        return this.apiService.request<UpdateFundingCallType, FundingCallType>("PUT", "api/funding-call-type", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortFundingCallType, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortFundingCallType, any>("PUT", "api/funding-call-type/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/funding-call-type/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryFundingCallType, options?: ApiOptions): Observable<QueryResult<FundingCallType>> {
        return this.apiService.request<QueryFundingCallType, QueryResult<FundingCallType>>("POST", "api/funding-call-type/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

