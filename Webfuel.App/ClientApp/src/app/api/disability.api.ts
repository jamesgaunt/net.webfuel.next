import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateDisability, Disability, UpdateDisability, SortDisability, QueryDisability, QueryResult } from './api.types';

@Injectable()
export class DisabilityApi implements IDataSource<Disability, QueryDisability, CreateDisability, UpdateDisability> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateDisability, options?: ApiOptions): Observable<Disability> {
        return this.apiService.request<CreateDisability, Disability>("POST", "api/disability", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateDisability, options?: ApiOptions): Observable<Disability> {
        return this.apiService.request<UpdateDisability, Disability>("PUT", "api/disability", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortDisability, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortDisability, any>("PUT", "api/disability/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/disability/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryDisability, options?: ApiOptions): Observable<QueryResult<Disability>> {
        return this.apiService.request<QueryDisability, QueryResult<Disability>>("POST", "api/disability/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

