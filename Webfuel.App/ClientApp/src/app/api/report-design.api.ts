import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { InsertReportColumn, ReportDesign, UpdateReportColumn, DeleteReportColumn, InsertReportFilter, UpdateReportFilter, DeleteReportFilter, ReportSchema, LookupReferenceField, QueryResult, ReportMapEntity, ReportArgument } from './api.types';

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
    
    public lookupReferenceField (body: LookupReferenceField, options?: ApiOptions): Observable<QueryResult<ReportMapEntity>> {
        return this.apiService.request<LookupReferenceField, QueryResult<ReportMapEntity>>("POST", "api/report-design/schema/lookup-reference-field", body, options);
    }
    
    public generateArguments (body: ReportDesign, options?: ApiOptions): Observable<Array<ReportArgument>> {
        return this.apiService.request<ReportDesign, Array<ReportArgument>>("POST", "api/report-design/generate-arguments", body, options);
    }
}

