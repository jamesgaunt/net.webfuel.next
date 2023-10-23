import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsLeadApplicantNHS, QueryResult, IsLeadApplicantNHS } from './api.types';

@Injectable()
export class IsLeadApplicantNHSApi implements IDataSource<IsLeadApplicantNHS, QueryIsLeadApplicantNHS, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsLeadApplicantNHS, options?: ApiOptions): Observable<QueryResult<IsLeadApplicantNHS>> {
        return this.apiService.request<QueryIsLeadApplicantNHS, QueryResult<IsLeadApplicantNHS>>("POST", "api/is-lead-applicant-nhs/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

