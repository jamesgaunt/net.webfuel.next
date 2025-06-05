import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryResearcherProfessionalBackground, QueryResult, ResearcherProfessionalBackground } from './api.types';

@Injectable()
export class ResearcherProfessionalBackgroundApi implements IDataSource<ResearcherProfessionalBackground, QueryResearcherProfessionalBackground, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryResearcherProfessionalBackground, options?: ApiOptions): Observable<QueryResult<ResearcherProfessionalBackground>> {
        return this.apiService.request<QueryResearcherProfessionalBackground, QueryResult<ResearcherProfessionalBackground>>("POST", "api/researcher-professional-background/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

