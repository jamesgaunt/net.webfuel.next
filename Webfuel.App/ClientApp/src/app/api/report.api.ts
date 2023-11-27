import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { ReportProgress } from './api.types';

@Injectable()
export class ReportApi implements IDataSource<Report, QueryReport, any, any> {
    constructor(private apiService: ApiService) { }
    
    public generate (params: { taskId: string }, options?: ApiOptions): Observable<ReportProgress> {
        return this.apiService.request<undefined, ReportProgress>("PUT", "api/report/" + params.taskId + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
}

