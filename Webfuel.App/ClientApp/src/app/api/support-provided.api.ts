import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateSupportProvided, SupportProvided, UpdateSupportProvided, SortSupportProvided, QuerySupportProvided, QueryResult } from './api.types';

@Injectable()
export class SupportProvidedApi implements IDataSource<SupportProvided, QuerySupportProvided, CreateSupportProvided, UpdateSupportProvided> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateSupportProvided, options?: ApiOptions): Observable<SupportProvided> {
        return this.apiService.request<CreateSupportProvided, SupportProvided>("POST", "api/support-provided", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateSupportProvided, options?: ApiOptions): Observable<SupportProvided> {
        return this.apiService.request<UpdateSupportProvided, SupportProvided>("PUT", "api/support-provided", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortSupportProvided, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortSupportProvided, any>("PUT", "api/support-provided/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/support-provided/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QuerySupportProvided, options?: ApiOptions): Observable<QueryResult<SupportProvided>> {
        return this.apiService.request<QuerySupportProvided, QueryResult<SupportProvided>>("POST", "api/support-provided/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

