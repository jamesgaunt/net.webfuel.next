import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateResearcher, Researcher, UpdateResearcher, QueryResearcher, QueryResult } from './api.types';

@Injectable()
export class ResearcherApi implements IDataSource<Researcher, QueryResearcher, CreateResearcher, UpdateResearcher> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateResearcher, options?: ApiOptions): Observable<Researcher> {
        return this.apiService.request<CreateResearcher, Researcher>("POST", "api/researcher", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateResearcher, options?: ApiOptions): Observable<Researcher> {
        return this.apiService.request<UpdateResearcher, Researcher>("PUT", "api/researcher", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/researcher/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryResearcher, options?: ApiOptions): Observable<QueryResult<Researcher>> {
        return this.apiService.request<QueryResearcher, QueryResult<Researcher>>("POST", "api/researcher/query", body, options);
    }
    
    public resolve (params: { id: string }, options?: ApiOptions): Observable<Researcher> {
        return this.apiService.request<undefined, Researcher>("GET", "api/researcher/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static researcherResolver(param: string): ResolveFn<Researcher> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Researcher> => {
            return inject(ResearcherApi).resolve({id: route.paramMap.get(param)! });
        };
    }
}

