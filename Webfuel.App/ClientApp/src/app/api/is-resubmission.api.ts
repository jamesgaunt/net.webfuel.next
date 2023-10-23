import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsResubmission, QueryResult, IsResubmission } from './api.types';

@Injectable()
export class IsResubmissionApi implements IDataSource<IsResubmission, QueryIsResubmission, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsResubmission, options?: ApiOptions): Observable<QueryResult<IsResubmission>> {
        return this.apiService.request<QueryIsResubmission, QueryResult<IsResubmission>>("POST", "api/is-resubmission/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

