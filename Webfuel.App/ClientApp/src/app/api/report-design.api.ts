import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { ReportSchema } from './api.types';

@Injectable()
export class ReportDesignApi {
    constructor(private apiService: ApiService) { }
    
    public getReportSchema (params: { reportProviderId: string }, options?: ApiOptions): Observable<ReportSchema> {
        return this.apiService.request<undefined, ReportSchema>("GET", "api/report-design/schema/" + params.reportProviderId + "", undefined, options);
    }
}

