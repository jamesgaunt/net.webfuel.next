import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QuerySubmissionOutcome, QueryResult, SubmissionOutcome } from './api.types';

@Injectable()
export class SubmissionOutcomeApi implements IDataSource<SubmissionOutcome, QuerySubmissionOutcome, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QuerySubmissionOutcome, options?: ApiOptions): Observable<QueryResult<SubmissionOutcome>> {
        return this.apiService.request<QuerySubmissionOutcome, QueryResult<SubmissionOutcome>>("POST", "api/submission-outcome/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

