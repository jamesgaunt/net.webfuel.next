import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateResearcherOrganisationType, ResearcherOrganisationType, UpdateResearcherOrganisationType, SortResearcherOrganisationType, QueryResearcherOrganisationType, QueryResult } from './api.types';

@Injectable()
export class ResearcherOrganisationTypeApi implements IDataSource<ResearcherOrganisationType, QueryResearcherOrganisationType, CreateResearcherOrganisationType, UpdateResearcherOrganisationType> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateResearcherOrganisationType, options?: ApiOptions): Observable<ResearcherOrganisationType> {
        return this.apiService.request<CreateResearcherOrganisationType, ResearcherOrganisationType>("POST", "api/researcher-organisation-type", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateResearcherOrganisationType, options?: ApiOptions): Observable<ResearcherOrganisationType> {
        return this.apiService.request<UpdateResearcherOrganisationType, ResearcherOrganisationType>("PUT", "api/researcher-organisation-type", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortResearcherOrganisationType, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortResearcherOrganisationType, any>("PUT", "api/researcher-organisation-type/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/researcher-organisation-type/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryResearcherOrganisationType, options?: ApiOptions): Observable<QueryResult<ResearcherOrganisationType>> {
        return this.apiService.request<QueryResearcherOrganisationType, QueryResult<ResearcherOrganisationType>>("POST", "api/researcher-organisation-type/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

