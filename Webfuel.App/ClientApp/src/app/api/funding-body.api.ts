import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { CreateFundingBody, FundingBody, UpdateFundingBody, SortFundingBody, QueryFundingBody, QueryResult } from './api.types';

@Injectable()
export class FundingBodyApi {
    constructor(private apiService: ApiService) { }
    
    public createFundingBody (body: CreateFundingBody, options?: ApiOptions): Observable<FundingBody> {
        return this.apiService.request("POST", "api/funding-body", body, options);
    }
    
    public updateFundingBody (body: UpdateFundingBody, options?: ApiOptions): Observable<FundingBody> {
        return this.apiService.request("PUT", "api/funding-body", body, options);
    }
    
    public sortFundingBody (body: SortFundingBody, options?: ApiOptions): Observable<any> {
        return this.apiService.request("PUT", "api/funding-body/sort", body, options);
    }
    
    public deleteFundingBody (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/funding-body/" + params.id + "", undefined, options);
    }
    
    public queryFundingBody (body: QueryFundingBody, options?: ApiOptions): Observable<QueryResult<FundingBody>> {
        return this.apiService.request("POST", "api/funding-body/query", body, options);
    }
}

