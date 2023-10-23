import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsTeamMembersConsulted, QueryResult, IsTeamMembersConsulted } from './api.types';

@Injectable()
export class IsTeamMembersConsultedApi implements IDataSource<IsTeamMembersConsulted, QueryIsTeamMembersConsulted, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsTeamMembersConsulted, options?: ApiOptions): Observable<QueryResult<IsTeamMembersConsulted>> {
        return this.apiService.request<QueryIsTeamMembersConsulted, QueryResult<IsTeamMembersConsulted>>("POST", "api/is-team-members-consulted/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

