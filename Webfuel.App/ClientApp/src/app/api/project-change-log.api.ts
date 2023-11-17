import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryProjectChangeLog, QueryResult, ProjectChangeLog } from './api.types';

@Injectable()
export class ProjectChangeLogApi implements IDataSource<ProjectChangeLog, QueryProjectChangeLog, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryProjectChangeLog, options?: ApiOptions): Observable<QueryResult<ProjectChangeLog>> {
        return this.apiService.request<QueryProjectChangeLog, QueryResult<ProjectChangeLog>>("POST", "api/project-change-log/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

