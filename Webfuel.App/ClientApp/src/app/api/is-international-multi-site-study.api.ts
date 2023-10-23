import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsInternationalMultiSiteStudy, QueryResult, IsInternationalMultiSiteStudy } from './api.types';

@Injectable()
export class IsInternationalMultiSiteStudyApi implements IDataSource<IsInternationalMultiSiteStudy, QueryIsInternationalMultiSiteStudy, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsInternationalMultiSiteStudy, options?: ApiOptions): Observable<QueryResult<IsInternationalMultiSiteStudy>> {
        return this.apiService.request<QueryIsInternationalMultiSiteStudy, QueryResult<IsInternationalMultiSiteStudy>>("POST", "api/is-international-multi-site-study/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

