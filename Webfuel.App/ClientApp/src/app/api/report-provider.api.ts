import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryReportProvider, QueryResult, ReportProvider } from './api.types';

@Injectable()
export class ReportProviderApi implements IDataSource<ReportProvider, QueryReportProvider, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryReportProvider, options?: ApiOptions): Observable<QueryResult<ReportProvider>> {
        return this.apiService.request<QueryReportProvider, QueryResult<ReportProvider>>("POST", "api/report-provider/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

