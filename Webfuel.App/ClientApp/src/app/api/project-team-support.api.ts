import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateProjectTeamSupport, ProjectTeamSupport, CompleteProjectTeamSupport, QueryProjectTeamSupport, QueryResult } from './api.types';

@Injectable()
export class ProjectTeamSupportApi implements IDataSource<ProjectTeamSupport, QueryProjectTeamSupport, CreateProjectTeamSupport, any> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateProjectTeamSupport, options?: ApiOptions): Observable<ProjectTeamSupport> {
        return this.apiService.request<CreateProjectTeamSupport, ProjectTeamSupport>("POST", "api/project-team-support", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public complete (body: CompleteProjectTeamSupport, options?: ApiOptions): Observable<ProjectTeamSupport> {
        return this.apiService.request<CompleteProjectTeamSupport, ProjectTeamSupport>("PUT", "api/project-team-support", body, options);
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/project-team-support/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryProjectTeamSupport, options?: ApiOptions): Observable<QueryResult<ProjectTeamSupport>> {
        return this.apiService.request<QueryProjectTeamSupport, QueryResult<ProjectTeamSupport>>("POST", "api/project-team-support/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<ProjectTeamSupport> {
        return this.apiService.request<undefined, ProjectTeamSupport>("GET", "api/project-team-support/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static projectTeamSupportResolver(param: string): ResolveFn<ProjectTeamSupport> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ProjectTeamSupport> => {
            return inject(ProjectTeamSupportApi).get({id: route.paramMap.get(param)! });
        };
    }
}

