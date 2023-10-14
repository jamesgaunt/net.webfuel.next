import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateFundingStream, FundingStream, UpdateFundingStream, SortFundingStream, QueryFundingStream, QueryResult } from './api.types';

@Injectable()
export class FundingStreamApi implements IDataSource<FundingStream, QueryFundingStream, CreateFundingStream, UpdateFundingStream> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateFundingStream, options?: ApiOptions): Observable<FundingStream> {
        return this.apiService.request<CreateFundingStream, FundingStream>("POST", "api/funding-stream", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateFundingStream, options?: ApiOptions): Observable<FundingStream> {
        return this.apiService.request<UpdateFundingStream, FundingStream>("PUT", "api/funding-stream", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortFundingStream, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortFundingStream, any>("PUT", "api/funding-stream/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/funding-stream/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryFundingStream, options?: ApiOptions): Observable<QueryResult<FundingStream>> {
        return this.apiService.request<QueryFundingStream, QueryResult<FundingStream>>("POST", "api/funding-stream/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

