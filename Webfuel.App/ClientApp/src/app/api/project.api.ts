import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from '../shared/data-source/data-source';
import { CreateProject, Project, UpdateProject, QueryProject, QueryResult } from './api.types';

@Injectable()
export class ProjectApi {
    constructor(private apiService: ApiService) { }
    
    public createProject (body: CreateProject, options?: ApiOptions): Observable<Project> {
        return this.apiService.request("POST", "api/project", body, options);
    }
    
    public updateProject (body: UpdateProject, options?: ApiOptions): Observable<Project> {
        return this.apiService.request("PUT", "api/project", body, options);
    }
    
    public deleteProject (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/project/" + params.id + "", undefined, options);
    }
    
    public queryProject (body: QueryProject, options?: ApiOptions): Observable<QueryResult<Project>> {
        return this.apiService.request("POST", "api/project/query", body, options);
    }
    
    public resolveProject (params: { id: string }, options?: ApiOptions): Observable<Project> {
        return this.apiService.request("GET", "api/project/" + params.id + "", undefined, options);
    }
    
    // Resolvers
    
    static projectResolver(param: string): ResolveFn<Project> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Project> => {
            return inject(ProjectApi).resolveProject({id: route.paramMap.get(param)! });
        };
    }
    
    // Data Sources
    
    projectDataSource: IDataSource<Project> = {
        fetch: (query) => this.queryProject(query),
        changed: new EventEmitter()
    }
}

