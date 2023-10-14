import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateResearchMethodology, ResearchMethodology, UpdateResearchMethodology, SortResearchMethodology, QueryResearchMethodology, QueryResult } from './api.types';

@Injectable()
export class ResearchMethodologyApi implements IDataSource<ResearchMethodology, QueryResearchMethodology, CreateResearchMethodology, UpdateResearchMethodology> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateResearchMethodology, options?: ApiOptions): Observable<ResearchMethodology> {
        return this.apiService.request<CreateResearchMethodology, ResearchMethodology>("POST", "api/research-methodology", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateResearchMethodology, options?: ApiOptions): Observable<ResearchMethodology> {
        return this.apiService.request<UpdateResearchMethodology, ResearchMethodology>("PUT", "api/research-methodology", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortResearchMethodology, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortResearchMethodology, any>("PUT", "api/research-methodology/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/research-methodology/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryResearchMethodology, options?: ApiOptions): Observable<QueryResult<ResearchMethodology>> {
        return this.apiService.request<QueryResearchMethodology, QueryResult<ResearchMethodology>>("POST", "api/research-methodology/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

