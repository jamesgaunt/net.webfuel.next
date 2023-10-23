import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsCTUTeamContribution, QueryResult, IsCTUTeamContribution } from './api.types';

@Injectable()
export class IsCTUTeamContributionApi implements IDataSource<IsCTUTeamContribution, QueryIsCTUTeamContribution, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsCTUTeamContribution, options?: ApiOptions): Observable<QueryResult<IsCTUTeamContribution>> {
        return this.apiService.request<QueryIsCTUTeamContribution, QueryResult<IsCTUTeamContribution>>("POST", "api/is-ctu-team-contribution/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

