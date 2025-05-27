import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryOutlineSubmissionStatus, QueryResult, OutlineSubmissionStatus } from './api.types';

@Injectable()
export class OutlineSubmissionStatusApi implements IDataSource<OutlineSubmissionStatus, QueryOutlineSubmissionStatus, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryOutlineSubmissionStatus, options?: ApiOptions): Observable<QueryResult<OutlineSubmissionStatus>> {
        return this.apiService.request<QueryOutlineSubmissionStatus, QueryResult<OutlineSubmissionStatus>>("POST", "api/outline-submission-status/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

