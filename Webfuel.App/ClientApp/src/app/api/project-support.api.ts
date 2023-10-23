import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateProjectSupport, ProjectSupport, UpdateProjectSupport, QueryProjectSupport, QueryResult } from './api.types';

@Injectable()
export class ProjectSupportApi implements IDataSource<ProjectSupport, QueryProjectSupport, CreateProjectSupport, UpdateProjectSupport> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateProjectSupport, options?: ApiOptions): Observable<ProjectSupport> {
        return this.apiService.request<CreateProjectSupport, ProjectSupport>("POST", "api/project-support", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateProjectSupport, options?: ApiOptions): Observable<ProjectSupport> {
        return this.apiService.request<UpdateProjectSupport, ProjectSupport>("PUT", "api/project-support", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/project-support/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryProjectSupport, options?: ApiOptions): Observable<QueryResult<ProjectSupport>> {
        return this.apiService.request<QueryProjectSupport, QueryResult<ProjectSupport>>("POST", "api/project-support/query", body, options);
    }
    
    public resolve (params: { id: string }, options?: ApiOptions): Observable<ProjectSupport> {
        return this.apiService.request<undefined, ProjectSupport>("GET", "api/project-support/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static projectSupportResolver(param: string): ResolveFn<ProjectSupport> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ProjectSupport> => {
            return inject(ProjectSupportApi).resolve({id: route.paramMap.get(param)! });
        };
    }
}

