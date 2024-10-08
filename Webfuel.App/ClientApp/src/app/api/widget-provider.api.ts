import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { ProjectSummaryData, TeamSupportData } from './api.types';

@Injectable()
export class WidgetProviderApi {
    constructor(private apiService: ApiService) { }
    
    public projectSummary (params: { id: string }, options?: ApiOptions): Observable<ProjectSummaryData> {
        return this.apiService.request<undefined, ProjectSummaryData>("GET", "api/widget/project-summary/" + params.id + "", undefined, options);
    }
    
    public teamSupport (params: { id: string }, options?: ApiOptions): Observable<TeamSupportData> {
        return this.apiService.request<undefined, TeamSupportData>("GET", "api/widget/team-support/" + params.id + "", undefined, options);
    }
}

