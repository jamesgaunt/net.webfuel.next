import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { ReportProgress } from './api.types';

@Injectable()
export class ReportGeneratorApi {
    constructor(private apiService: ApiService) { }
    
    public generateReport (params: { taskId: string }, options?: ApiOptions): Observable<ReportProgress> {
        return this.apiService.request<undefined, ReportProgress>("POST", "api/report-generator/" + params.taskId + "", undefined, options);
    }
    
    public cancelReport (params: { taskId: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/report-generator/" + params.taskId + "", undefined, options);
    }
}

