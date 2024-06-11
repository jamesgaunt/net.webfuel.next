import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateResearcherLocation, ResearcherLocation, UpdateResearcherLocation, SortResearcherLocation, QueryResearcherLocation, QueryResult } from './api.types';

@Injectable()
export class ResearcherLocationApi implements IDataSource<ResearcherLocation, QueryResearcherLocation, CreateResearcherLocation, UpdateResearcherLocation> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateResearcherLocation, options?: ApiOptions): Observable<ResearcherLocation> {
        return this.apiService.request<CreateResearcherLocation, ResearcherLocation>("POST", "api/researcher-location", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateResearcherLocation, options?: ApiOptions): Observable<ResearcherLocation> {
        return this.apiService.request<UpdateResearcherLocation, ResearcherLocation>("PUT", "api/researcher-location", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortResearcherLocation, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortResearcherLocation, any>("PUT", "api/researcher-location/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/researcher-location/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryResearcherLocation, options?: ApiOptions): Observable<QueryResult<ResearcherLocation>> {
        return this.apiService.request<QueryResearcherLocation, QueryResult<ResearcherLocation>>("POST", "api/researcher-location/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

