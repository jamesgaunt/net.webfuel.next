import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QuerySuportRequestStatus, QueryResult, SuportRequestStatus } from './api.types';

@Injectable()
export class SuportRequestStatusApi implements IDataSource<SuportRequestStatus, QuerySuportRequestStatus, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QuerySuportRequestStatus, options?: ApiOptions): Observable<QueryResult<SuportRequestStatus>> {
        return this.apiService.request<QuerySuportRequestStatus, QueryResult<SuportRequestStatus>>("POST", "api/suport-request-status/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

