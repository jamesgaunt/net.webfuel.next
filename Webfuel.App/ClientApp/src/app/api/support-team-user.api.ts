import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { InsertSupportTeamUser, DeleteSupportTeamUser, QuerySupportTeamUser, QueryResult, SupportTeamUser } from './api.types';

@Injectable()
export class SupportTeamUserApi {
    constructor(private apiService: ApiService) { }
    
    public insert (body: InsertSupportTeamUser, options?: ApiOptions): Observable<any> {
        return this.apiService.request<InsertSupportTeamUser, any>("POST", "api/support-team-user/insert", body, options);
    }
    
    public delete (body: DeleteSupportTeamUser, options?: ApiOptions): Observable<any> {
        return this.apiService.request<DeleteSupportTeamUser, any>("POST", "api/support-team-user/delete", body, options);
    }
    
    public query (body: QuerySupportTeamUser, options?: ApiOptions): Observable<QueryResult<SupportTeamUser>> {
        return this.apiService.request<QuerySupportTeamUser, QueryResult<SupportTeamUser>>("POST", "api/support-team-user/query", body, options);
    }
}

