import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QuerySubmissionStatus, QueryResult, SubmissionStatus } from './api.types';

@Injectable()
export class SubmissionStatusApi implements IDataSource<SubmissionStatus, QuerySubmissionStatus, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QuerySubmissionStatus, options?: ApiOptions): Observable<QueryResult<SubmissionStatus>> {
        return this.apiService.request<QuerySubmissionStatus, QueryResult<SubmissionStatus>>("POST", "api/submission-status/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

