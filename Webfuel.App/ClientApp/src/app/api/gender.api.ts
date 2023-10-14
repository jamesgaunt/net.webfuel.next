import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateGender, Gender, UpdateGender, SortGender, QueryGender, QueryResult } from './api.types';

@Injectable()
export class GenderApi implements IDataSource<Gender, QueryGender, CreateGender, UpdateGender> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateGender, options?: ApiOptions): Observable<Gender> {
        return this.apiService.request<CreateGender, Gender>("POST", "api/gender", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateGender, options?: ApiOptions): Observable<Gender> {
        return this.apiService.request<UpdateGender, Gender>("PUT", "api/gender", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortGender, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortGender, any>("PUT", "api/gender/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/gender/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryGender, options?: ApiOptions): Observable<QueryResult<Gender>> {
        return this.apiService.request<QueryGender, QueryResult<Gender>>("POST", "api/gender/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

