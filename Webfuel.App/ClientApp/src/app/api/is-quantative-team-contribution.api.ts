import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsQuantativeTeamContribution, QueryResult, IsQuantativeTeamContribution } from './api.types';

@Injectable()
export class IsQuantativeTeamContributionApi implements IDataSource<IsQuantativeTeamContribution, QueryIsQuantativeTeamContribution, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsQuantativeTeamContribution, options?: ApiOptions): Observable<QueryResult<IsQuantativeTeamContribution>> {
        return this.apiService.request<QueryIsQuantativeTeamContribution, QueryResult<IsQuantativeTeamContribution>>("POST", "api/is-quantative-team-contribution/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

