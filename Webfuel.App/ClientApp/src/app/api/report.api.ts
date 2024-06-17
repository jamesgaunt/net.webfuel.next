import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateReport, Report, CopyReport, UpdateReport, RunReport, ReportStep, RunAnnualReport, QueryReport, QueryResult } from './api.types';

@Injectable()
export class ReportApi implements IDataSource<Report, QueryReport, CreateReport, UpdateReport> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateReport, options?: ApiOptions): Observable<Report> {
        return this.apiService.request<CreateReport, Report>("POST", "api/report", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public copy (body: CopyReport, options?: ApiOptions): Observable<Report> {
        return this.apiService.request<CopyReport, Report>("POST", "api/report/copy", body, options);
    }
    
    public update (body: UpdateReport, options?: ApiOptions): Observable<Report> {
        return this.apiService.request<UpdateReport, Report>("PUT", "api/report", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/report/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public run (body: RunReport, options?: ApiOptions): Observable<ReportStep> {
        return this.apiService.request<RunReport, ReportStep>("POST", "api/report/run", body, options);
    }
    
    public runAnnualReport (body: RunAnnualReport, options?: ApiOptions): Observable<ReportStep> {
        return this.apiService.request<RunAnnualReport, ReportStep>("POST", "api/report/run-annual", body, options);
    }
    
    public query (body: QueryReport, options?: ApiOptions): Observable<QueryResult<Report>> {
        return this.apiService.request<QueryReport, QueryResult<Report>>("POST", "api/report/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<Report> {
        return this.apiService.request<undefined, Report>("GET", "api/report/" + params.id + "", undefined, options);
    }
    
    public listHead (options?: ApiOptions): Observable<Array<Report>> {
        return this.apiService.request<undefined, Array<Report>>("GET", "api/report/list-head", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static reportResolver(param: string): ResolveFn<Report> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Report> => {
            return inject(ReportApi).get({id: route.paramMap.get(param)! });
        };
    }
}

