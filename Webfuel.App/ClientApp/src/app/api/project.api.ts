import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateProject, Project, UpdateProject, QueryProject, QueryResult } from './api.types';

@Injectable()
export class ProjectApi implements IDataSource<Project, QueryProject, CreateProject, UpdateProject> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateProject, options?: ApiOptions): Observable<Project> {
        return this.apiService.request<CreateProject, Project>("POST", "api/project", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateProject, options?: ApiOptions): Observable<Project> {
        return this.apiService.request<UpdateProject, Project>("PUT", "api/project", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/project/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryProject, options?: ApiOptions): Observable<QueryResult<Project>> {
        return this.apiService.request<QueryProject, QueryResult<Project>>("POST", "api/project/query", body, options);
    }
    
    public resolve (params: { id: string }, options?: ApiOptions): Observable<Project> {
        return this.apiService.request<undefined, Project>("GET", "api/project/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static projectResolver(param: string): ResolveFn<Project> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Project> => {
            return inject(ProjectApi).resolve({id: route.paramMap.get(param)! });
        };
    }
}

