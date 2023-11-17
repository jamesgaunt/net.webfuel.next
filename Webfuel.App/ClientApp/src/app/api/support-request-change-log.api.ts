import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QuerySupportRequestChangeLog, QueryResult, SupportRequestChangeLog } from './api.types';

@Injectable()
export class SupportRequestChangeLogApi implements IDataSource<SupportRequestChangeLog, QuerySupportRequestChangeLog, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QuerySupportRequestChangeLog, options?: ApiOptions): Observable<QueryResult<SupportRequestChangeLog>> {
        return this.apiService.request<QuerySupportRequestChangeLog, QueryResult<SupportRequestChangeLog>>("POST", "api/support-request-change-log/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

