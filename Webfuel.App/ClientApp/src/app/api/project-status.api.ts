import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryProjectStatus, QueryResult, ProjectStatus } from './api.types';

@Injectable()
export class ProjectStatusApi implements IDataSource<ProjectStatus, QueryProjectStatus, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryProjectStatus, options?: ApiOptions): Observable<QueryResult<ProjectStatus>> {
        return this.apiService.request<QueryProjectStatus, QueryResult<ProjectStatus>>("POST", "api/project-status/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

