import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { UpdateProject, Project, UpdateProjectRequest, UpdateProjectResearcher, UpdateProjectSupportSettings, UnlockProject, CreateTestProject, QueryProject, QueryResult, ReportStep } from './api.types';

@Injectable()
export class ProjectApi implements IDataSource<Project, QueryProject, any, UpdateProject> {
    constructor(private apiService: ApiService) { }
    
    public update (body: UpdateProject, options?: ApiOptions): Observable<Project> {
        return this.apiService.request<UpdateProject, Project>("PUT", "api/project", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public updateRequest (body: UpdateProjectRequest, options?: ApiOptions): Observable<Project> {
        return this.apiService.request<UpdateProjectRequest, Project>("PUT", "api/project/request", body, options);
    }
    
    public updateResearcher (body: UpdateProjectResearcher, options?: ApiOptions): Observable<Project> {
        return this.apiService.request<UpdateProjectResearcher, Project>("PUT", "api/project/researcher", body, options);
    }
    
    public updateSupportSettings (body: UpdateProjectSupportSettings, options?: ApiOptions): Observable<Project> {
        return this.apiService.request<UpdateProjectSupportSettings, Project>("PUT", "api/project/support-settings", body, options);
    }
    
    public unlock (body: UnlockProject, options?: ApiOptions): Observable<Project> {
        return this.apiService.request<UnlockProject, Project>("PUT", "api/project/unlock", body, options);
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/project/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public createTest (body: CreateTestProject, options?: ApiOptions): Observable<Project> {
        return this.apiService.request<CreateTestProject, Project>("POST", "api/project:create-test", body, options);
    }
    
    public query (body: QueryProject, options?: ApiOptions): Observable<QueryResult<Project>> {
        return this.apiService.request<QueryProject, QueryResult<Project>>("POST", "api/project/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<Project> {
        return this.apiService.request<undefined, Project>("GET", "api/project/" + params.id + "", undefined, options);
    }
    
    public export (body: QueryProject, options?: ApiOptions): Observable<ReportStep> {
        return this.apiService.request<QueryProject, ReportStep>("PUT", "api/project/export", body, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static projectResolver(param: string): ResolveFn<Project> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Project> => {
            return inject(ProjectApi).get({id: route.paramMap.get(param)! });
        };
    }
}

