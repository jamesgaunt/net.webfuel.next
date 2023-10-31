import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateResearcherRole, ResearcherRole, UpdateResearcherRole, SortResearcherRole, QueryResearcherRole, QueryResult } from './api.types';

@Injectable()
export class ResearcherRoleApi implements IDataSource<ResearcherRole, QueryResearcherRole, CreateResearcherRole, UpdateResearcherRole> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateResearcherRole, options?: ApiOptions): Observable<ResearcherRole> {
        return this.apiService.request<CreateResearcherRole, ResearcherRole>("POST", "api/researcher-role", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateResearcherRole, options?: ApiOptions): Observable<ResearcherRole> {
        return this.apiService.request<UpdateResearcherRole, ResearcherRole>("PUT", "api/researcher-role", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortResearcherRole, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortResearcherRole, any>("PUT", "api/researcher-role/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/researcher-role/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryResearcherRole, options?: ApiOptions): Observable<QueryResult<ResearcherRole>> {
        return this.apiService.request<QueryResearcherRole, QueryResult<ResearcherRole>>("POST", "api/researcher-role/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

