import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QuerySubmissionStage, QueryResult, SubmissionStage } from './api.types';

@Injectable()
export class SubmissionStageApi implements IDataSource<SubmissionStage, QuerySubmissionStage, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QuerySubmissionStage, options?: ApiOptions): Observable<QueryResult<SubmissionStage>> {
        return this.apiService.request<QuerySubmissionStage, QueryResult<SubmissionStage>>("POST", "api/submission-stage/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

