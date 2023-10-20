import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateApplicationStage, ApplicationStage, UpdateApplicationStage, SortApplicationStage, QueryApplicationStage, QueryResult } from './api.types';

@Injectable()
export class ApplicationStageApi implements IDataSource<ApplicationStage, QueryApplicationStage, CreateApplicationStage, UpdateApplicationStage> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateApplicationStage, options?: ApiOptions): Observable<ApplicationStage> {
        return this.apiService.request<CreateApplicationStage, ApplicationStage>("POST", "api/application-stage", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateApplicationStage, options?: ApiOptions): Observable<ApplicationStage> {
        return this.apiService.request<UpdateApplicationStage, ApplicationStage>("PUT", "api/application-stage", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortApplicationStage, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortApplicationStage, any>("PUT", "api/application-stage/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/application-stage/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryApplicationStage, options?: ApiOptions): Observable<QueryResult<ApplicationStage>> {
        return this.apiService.request<QueryApplicationStage, QueryResult<ApplicationStage>>("POST", "api/application-stage/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

