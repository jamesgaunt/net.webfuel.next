import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateAgeRange, AgeRange, UpdateAgeRange, SortAgeRange, QueryAgeRange, QueryResult } from './api.types';

@Injectable()
export class AgeRangeApi implements IDataSource<AgeRange, QueryAgeRange, CreateAgeRange, UpdateAgeRange> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateAgeRange, options?: ApiOptions): Observable<AgeRange> {
        return this.apiService.request<CreateAgeRange, AgeRange>("POST", "api/age-range", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateAgeRange, options?: ApiOptions): Observable<AgeRange> {
        return this.apiService.request<UpdateAgeRange, AgeRange>("PUT", "api/age-range", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortAgeRange, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortAgeRange, any>("PUT", "api/age-range/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/age-range/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryAgeRange, options?: ApiOptions): Observable<QueryResult<AgeRange>> {
        return this.apiService.request<QueryAgeRange, QueryResult<AgeRange>>("POST", "api/age-range/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

