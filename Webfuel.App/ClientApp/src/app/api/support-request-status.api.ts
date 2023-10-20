import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QuerySupportRequestStatus, QueryResult, SupportRequestStatus } from './api.types';

@Injectable()
export class SupportRequestStatusApi implements IDataSource<SupportRequestStatus, QuerySupportRequestStatus, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QuerySupportRequestStatus, options?: ApiOptions): Observable<QueryResult<SupportRequestStatus>> {
        return this.apiService.request<QuerySupportRequestStatus, QueryResult<SupportRequestStatus>>("POST", "api/support-request-status/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

