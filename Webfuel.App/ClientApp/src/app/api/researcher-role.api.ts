import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryResearcherRole, QueryResult, ResearcherRole } from './api.types';

@Injectable()
export class ResearcherRoleApi implements IDataSource<ResearcherRole, QueryResearcherRole, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryResearcherRole, options?: ApiOptions): Observable<QueryResult<ResearcherRole>> {
        return this.apiService.request<QueryResearcherRole, QueryResult<ResearcherRole>>("POST", "api/researcher-role/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

