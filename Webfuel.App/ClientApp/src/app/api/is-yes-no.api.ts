import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsYesNo, QueryResult, IsYesNo } from './api.types';

@Injectable()
export class IsYesNoApi implements IDataSource<IsYesNo, QueryIsYesNo, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsYesNo, options?: ApiOptions): Observable<QueryResult<IsYesNo>> {
        return this.apiService.request<QueryIsYesNo, QueryResult<IsYesNo>>("POST", "api/is-yes-no/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

