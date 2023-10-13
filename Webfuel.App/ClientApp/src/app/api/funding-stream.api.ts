import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateFundingStream, FundingStream, UpdateFundingStream, SortFundingStream, QueryFundingStream, QueryResult } from './api.types';

@Injectable()
export class FundingStreamApi {
    constructor(private apiService: ApiService) { }
    
    public createFundingStream (body: CreateFundingStream, options?: ApiOptions): Observable<FundingStream> {
        return this.apiService.request("POST", "api/funding-stream", body, options);
    }
    
    public updateFundingStream (body: UpdateFundingStream, options?: ApiOptions): Observable<FundingStream> {
        return this.apiService.request("PUT", "api/funding-stream", body, options);
    }
    
    public sortFundingStream (body: SortFundingStream, options?: ApiOptions): Observable<any> {
        return this.apiService.request("PUT", "api/funding-stream/sort", body, options);
    }
    
    public deleteFundingStream (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/funding-stream/" + params.id + "", undefined, options);
    }
    
    public queryFundingStream (body: QueryFundingStream, options?: ApiOptions): Observable<QueryResult<FundingStream>> {
        return this.apiService.request("POST", "api/funding-stream/query", body, options);
    }
    
    // Data Sources
    
    fundingStreamDataSource: IDataSource<FundingStream> = {
        fetch: (query) => this.queryFundingStream(query),
        changed: new EventEmitter()
    }
}

