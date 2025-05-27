import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryOutlineOutcome, QueryResult, OutlineOutcome } from './api.types';

@Injectable()
export class OutlineOutcomeApi implements IDataSource<OutlineOutcome, QueryOutlineOutcome, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryOutlineOutcome, options?: ApiOptions): Observable<QueryResult<OutlineOutcome>> {
        return this.apiService.request<QueryOutlineOutcome, QueryResult<OutlineOutcome>>("POST", "api/outline-outcome/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

