import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryIsCTUAlreadyInvolved, QueryResult, IsCTUAlreadyInvolved } from './api.types';

@Injectable()
export class IsCTUAlreadyInvolvedApi implements IDataSource<IsCTUAlreadyInvolved, QueryIsCTUAlreadyInvolved, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryIsCTUAlreadyInvolved, options?: ApiOptions): Observable<QueryResult<IsCTUAlreadyInvolved>> {
        return this.apiService.request<QueryIsCTUAlreadyInvolved, QueryResult<IsCTUAlreadyInvolved>>("POST", "api/is-ctu-already-involved/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

