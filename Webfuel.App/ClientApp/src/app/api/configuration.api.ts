import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { ClientConfiguration } from './api.types';

@Injectable()
export class ConfigurationApi {
    constructor(private apiService: ApiService) { }
    
    public getConfiguration (options?: ApiOptions): Observable<ClientConfiguration> {
        return this.apiService.request<undefined, ClientConfiguration>("GET", "api/configuration", undefined, options);
    }
}

