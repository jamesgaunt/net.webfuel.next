import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QuerySupportTeam, QueryResult, SupportTeam } from './api.types';

@Injectable()
export class SupportTeamApi implements IDataSource<SupportTeam, QuerySupportTeam, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QuerySupportTeam, options?: ApiOptions): Observable<QueryResult<SupportTeam>> {
        return this.apiService.request<QuerySupportTeam, QueryResult<SupportTeam>>("POST", "api/support-team/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

