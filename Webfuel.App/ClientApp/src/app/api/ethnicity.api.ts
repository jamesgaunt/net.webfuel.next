import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateEthnicity, Ethnicity, UpdateEthnicity, SortEthnicity, QueryEthnicity, QueryResult } from './api.types';

@Injectable()
export class EthnicityApi implements IDataSource<Ethnicity, QueryEthnicity, CreateEthnicity, UpdateEthnicity> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateEthnicity, options?: ApiOptions): Observable<Ethnicity> {
        return this.apiService.request<CreateEthnicity, Ethnicity>("POST", "api/ethnicity", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateEthnicity, options?: ApiOptions): Observable<Ethnicity> {
        return this.apiService.request<UpdateEthnicity, Ethnicity>("PUT", "api/ethnicity", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortEthnicity, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortEthnicity, any>("PUT", "api/ethnicity/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/ethnicity/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryEthnicity, options?: ApiOptions): Observable<QueryResult<Ethnicity>> {
        return this.apiService.request<QueryEthnicity, QueryResult<Ethnicity>>("POST", "api/ethnicity/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

