import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { PingResponse } from './api.types';

@Injectable()
export class PingApi {
    constructor(private apiService: ApiService) { }
    
    public ping (options?: ApiOptions): Observable<PingResponse> {
        return this.apiService.request<undefined, PingResponse>("GET", "api/ping", undefined, options);
    }
}

