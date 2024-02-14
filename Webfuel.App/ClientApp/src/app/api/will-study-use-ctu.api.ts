import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryWillStudyUseCTU, QueryResult, WillStudyUseCTU } from './api.types';

@Injectable()
export class WillStudyUseCTUApi implements IDataSource<WillStudyUseCTU, QueryWillStudyUseCTU, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryWillStudyUseCTU, options?: ApiOptions): Observable<QueryResult<WillStudyUseCTU>> {
        return this.apiService.request<QueryWillStudyUseCTU, QueryResult<WillStudyUseCTU>>("POST", "api/will-study-use-ctu/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

