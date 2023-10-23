import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsPPIEAndEDIContribution, QueryResult, IsPPIEAndEDIContribution } from './api.types';

@Injectable()
export class IsPPIEAndEDIContributionApi implements IDataSource<IsPPIEAndEDIContribution, QueryIsPPIEAndEDIContribution, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsPPIEAndEDIContribution, options?: ApiOptions): Observable<QueryResult<IsPPIEAndEDIContribution>> {
        return this.apiService.request<QueryIsPPIEAndEDIContribution, QueryResult<IsPPIEAndEDIContribution>>("POST", "api/is-ppie-and-edi-contribution/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

