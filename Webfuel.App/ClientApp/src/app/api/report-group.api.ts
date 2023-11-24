import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateReportGroup, ReportGroup, UpdateReportGroup, SortReportGroup, QueryReportGroup, QueryResult } from './api.types';

@Injectable()
export class ReportGroupApi implements IDataSource<ReportGroup, QueryReportGroup, CreateReportGroup, UpdateReportGroup> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateReportGroup, options?: ApiOptions): Observable<ReportGroup> {
        return this.apiService.request<CreateReportGroup, ReportGroup>("POST", "api/report-group", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateReportGroup, options?: ApiOptions): Observable<ReportGroup> {
        return this.apiService.request<UpdateReportGroup, ReportGroup>("PUT", "api/report-group", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortReportGroup, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortReportGroup, any>("PUT", "api/report-group/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/report-group/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryReportGroup, options?: ApiOptions): Observable<QueryResult<ReportGroup>> {
        return this.apiService.request<QueryReportGroup, QueryResult<ReportGroup>>("POST", "api/report-group/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<ReportGroup> {
        return this.apiService.request<undefined, ReportGroup>("GET", "api/report-group/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static reportGroupResolver(param: string): ResolveFn<ReportGroup> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ReportGroup> => {
            return inject(ReportGroupApi).get({id: route.paramMap.get(param)! });
        };
    }
}

