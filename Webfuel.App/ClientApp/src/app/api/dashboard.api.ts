import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { DashboardModel } from './api.types';

@Injectable()
export class DashboardApi {
    constructor(private apiService: ApiService) { }
    
    public getDashboardModel (options?: ApiOptions): Observable<DashboardModel> {
        return this.apiService.request<undefined, DashboardModel>("GET", "api/dashboard-model", undefined, options);
    }
}

