import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryFullSubmissionStatus, QueryResult, FullSubmissionStatus } from './api.types';

@Injectable()
export class FullSubmissionStatusApi implements IDataSource<FullSubmissionStatus, QueryFullSubmissionStatus, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryFullSubmissionStatus, options?: ApiOptions): Observable<QueryResult<FullSubmissionStatus>> {
        return this.apiService.request<QueryFullSubmissionStatus, QueryResult<FullSubmissionStatus>>("POST", "api/full-submission-status/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

