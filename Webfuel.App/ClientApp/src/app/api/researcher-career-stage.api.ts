import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateResearcherCareerStage, ResearcherCareerStage, UpdateResearcherCareerStage, SortResearcherCareerStage, QueryResearcherCareerStage, QueryResult } from './api.types';

@Injectable()
export class ResearcherCareerStageApi implements IDataSource<ResearcherCareerStage, QueryResearcherCareerStage, CreateResearcherCareerStage, UpdateResearcherCareerStage> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateResearcherCareerStage, options?: ApiOptions): Observable<ResearcherCareerStage> {
        return this.apiService.request<CreateResearcherCareerStage, ResearcherCareerStage>("POST", "api/researcher-career-stage", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateResearcherCareerStage, options?: ApiOptions): Observable<ResearcherCareerStage> {
        return this.apiService.request<UpdateResearcherCareerStage, ResearcherCareerStage>("PUT", "api/researcher-career-stage", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortResearcherCareerStage, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortResearcherCareerStage, any>("PUT", "api/researcher-career-stage/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/researcher-career-stage/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryResearcherCareerStage, options?: ApiOptions): Observable<QueryResult<ResearcherCareerStage>> {
        return this.apiService.request<QueryResearcherCareerStage, QueryResult<ResearcherCareerStage>>("POST", "api/researcher-career-stage/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

