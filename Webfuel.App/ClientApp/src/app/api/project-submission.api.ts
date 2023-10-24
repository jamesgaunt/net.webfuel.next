import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateProjectSubmission, ProjectSubmission, UpdateProjectSubmission, QueryProjectSubmission, QueryResult } from './api.types';

@Injectable()
export class ProjectSubmissionApi implements IDataSource<ProjectSubmission, QueryProjectSubmission, CreateProjectSubmission, UpdateProjectSubmission> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateProjectSubmission, options?: ApiOptions): Observable<ProjectSubmission> {
        return this.apiService.request<CreateProjectSubmission, ProjectSubmission>("POST", "api/project-submission", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateProjectSubmission, options?: ApiOptions): Observable<ProjectSubmission> {
        return this.apiService.request<UpdateProjectSubmission, ProjectSubmission>("PUT", "api/project-submission", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/project-submission/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryProjectSubmission, options?: ApiOptions): Observable<QueryResult<ProjectSubmission>> {
        return this.apiService.request<QueryProjectSubmission, QueryResult<ProjectSubmission>>("POST", "api/project-submission/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<ProjectSubmission> {
        return this.apiService.request<undefined, ProjectSubmission>("GET", "api/project-submission/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static projectSubmissionResolver(param: string): ResolveFn<ProjectSubmission> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ProjectSubmission> => {
            return inject(ProjectSubmissionApi).get({id: route.paramMap.get(param)! });
        };
    }
}

