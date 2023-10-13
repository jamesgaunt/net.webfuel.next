import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateResearchMethodology, ResearchMethodology, UpdateResearchMethodology, SortResearchMethodology, QueryResearchMethodology, QueryResult } from './api.types';

@Injectable()
export class ResearchMethodologyApi {
    constructor(private apiService: ApiService) { }
    
    public createResearchMethodology (body: CreateResearchMethodology, options?: ApiOptions): Observable<ResearchMethodology> {
        return this.apiService.request("POST", "api/research-methodology", body, options);
    }
    
    public updateResearchMethodology (body: UpdateResearchMethodology, options?: ApiOptions): Observable<ResearchMethodology> {
        return this.apiService.request("PUT", "api/research-methodology", body, options);
    }
    
    public sortResearchMethodology (body: SortResearchMethodology, options?: ApiOptions): Observable<any> {
        return this.apiService.request("PUT", "api/research-methodology/sort", body, options);
    }
    
    public deleteResearchMethodology (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/research-methodology/" + params.id + "", undefined, options);
    }
    
    public queryResearchMethodology (body: QueryResearchMethodology, options?: ApiOptions): Observable<QueryResult<ResearchMethodology>> {
        return this.apiService.request("POST", "api/research-methodology/query", body, options);
    }
    
    // Data Sources
    
    researchMethodologyDataSource: IDataSource<ResearchMethodology> = {
        fetch: (query) => this.queryResearchMethodology(query),
        changed: new EventEmitter()
    }
}

