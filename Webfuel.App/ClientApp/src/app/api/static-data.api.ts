import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from '../shared/common/data-source';
import { IStaticDataModel } from './api.types';

@Injectable()
export class StaticDataApi {
    constructor(private apiService: ApiService) { }
    
    public getStaticData (options?: ApiOptions): Observable<IStaticDataModel> {
        return this.apiService.request("GET", "api/static-data", undefined, options);
    }
}

