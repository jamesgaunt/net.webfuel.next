import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { InsertReportColumn, ReportDesign, UpdateReportColumn, DeleteReportColumn, InsertReportFilter, UpdateReportFilter, DeleteReportFilter, ReportSchema, GetReportReference, ReportReference, QueryReportReference, QueryResult, ValidateDesign } from './api.types';

@Injectable()
export class ReportDesignApi {
    constructor(private apiService: ApiService) { }
    
    public insertReportColumn (body: InsertReportColumn, options?: ApiOptions): Observable<ReportDesign> {
        return this.apiService.request<InsertReportColumn, ReportDesign>("POST", "api/report-design/insert-column", body, options);
    }
    
    public updateReportColumn (body: UpdateReportColumn, options?: ApiOptions): Observable<ReportDesign> {
        return this.apiService.request<UpdateReportColumn, ReportDesign>("POST", "api/report-design/update-column", body, options);
    }
    
    public deleteReportColumn (body: DeleteReportColumn, options?: ApiOptions): Observable<ReportDesign> {
        return this.apiService.request<DeleteReportColumn, ReportDesign>("POST", "api/report-design/delete-column", body, options);
    }
    
    public insertReportFilter (body: InsertReportFilter, options?: ApiOptions): Observable<ReportDesign> {
        return this.apiService.request<InsertReportFilter, ReportDesign>("POST", "api/report-design/insert-filter", body, options);
    }
    
    public updateReportFilter (body: UpdateReportFilter, options?: ApiOptions): Observable<ReportDesign> {
        return this.apiService.request<UpdateReportFilter, ReportDesign>("POST", "api/report-design/update-filter", body, options);
    }
    
    public deleteReportFilter (body: DeleteReportFilter, options?: ApiOptions): Observable<ReportDesign> {
        return this.apiService.request<DeleteReportFilter, ReportDesign>("POST", "api/report-design/delete-filter", body, options);
    }
    
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

