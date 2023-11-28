import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { ProjectExportRequest, ReportProgress } from './api.types';

@Injectable()
export class ProjectExportApi {
    constructor(private apiService: ApiService) { }
    
    public initialiseReport (body: ProjectExportRequest, options?: ApiOptions): Observable<ReportProgress> {
        return this.apiService.request<ProjectExportRequest, ReportProgress>("PUT", "api/project-export", body, options);
    }
}

