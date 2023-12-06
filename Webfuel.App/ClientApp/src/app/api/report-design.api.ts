import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { ReportSchema, GetReportReference, ReportReference, QueryReportReference, QueryResult, ValidateDesign, ReportDesign } from './api.types';

@Injectable()
export class ReportDesignApi {
    constructor(private apiService: ApiService) { }
    
    public getReportSchema (params: { reportProviderId: string }, options?: ApiOptions): Observable<ReportSchema> {
        return this.apiService.request<undefined, ReportSchema>("GET", "api/report-design/schema/" + params.reportProviderId + "", undefined, options);
    }
    
    public getReportReference (body: GetReportReference, options?: ApiOptions): Observable<ReportReference> {
        return this.apiService.request<GetReportReference, ReportReference>("POST", "api/report-design/get-report-reference", body, options);
    }
    
    public queryReportReference (body: QueryReportReference, options?: ApiOptions): Observable<QueryResult<ReportReference>> {
        return this.apiService.request<QueryReportReference, QueryResult<ReportReference>>("POST", "api/report-design/query-report-reference", body, options);
    }
    
    public validateDesign (body: ValidateDesign, options?: ApiOptions): Observable<ReportDesign> {
        return this.apiService.request<ValidateDesign, ReportDesign>("POST", "api/report-design/validate-design", body, options);
    }
}

