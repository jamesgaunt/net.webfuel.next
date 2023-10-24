import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateWorkActivity, WorkActivity, UpdateWorkActivity, SortWorkActivity, QueryWorkActivity, QueryResult } from './api.types';

@Injectable()
export class WorkActivityApi implements IDataSource<WorkActivity, QueryWorkActivity, CreateWorkActivity, UpdateWorkActivity> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateWorkActivity, options?: ApiOptions): Observable<WorkActivity> {
        return this.apiService.request<CreateWorkActivity, WorkActivity>("POST", "api/work-activity", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateWorkActivity, options?: ApiOptions): Observable<WorkActivity> {
        return this.apiService.request<UpdateWorkActivity, WorkActivity>("PUT", "api/work-activity", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortWorkActivity, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortWorkActivity, any>("PUT", "api/work-activity/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/work-activity/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryWorkActivity, options?: ApiOptions): Observable<QueryResult<WorkActivity>> {
        return this.apiService.request<QueryWorkActivity, QueryResult<WorkActivity>>("POST", "api/work-activity/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

