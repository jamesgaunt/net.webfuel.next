import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { ClientConfiguration } from './api.types';

@Injectable()
export class ConfigurationApi {
    constructor(private apiService: ApiService) { }
    
    public getConfiguration (options?: ApiOptions): Observable<ClientConfiguration> {
        return this.apiService.request("GET", "api/configuration", undefined, options);
    }
}

