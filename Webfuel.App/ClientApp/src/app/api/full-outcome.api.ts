import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryFullOutcome, QueryResult, FullOutcome } from './api.types';

@Injectable()
export class FullOutcomeApi implements IDataSource<FullOutcome, QueryFullOutcome, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryFullOutcome, options?: ApiOptions): Observable<QueryResult<FullOutcome>> {
        return this.apiService.request<QueryFullOutcome, QueryResult<FullOutcome>>("POST", "api/full-outcome/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

