import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from '../shared/data-source/data-source';
import { CreateGender, Gender, UpdateGender, SortGender, QueryGender, QueryResult } from './api.types';

@Injectable()
export class GenderApi {
    constructor(private apiService: ApiService) { }
    
    public createGender (body: CreateGender, options?: ApiOptions): Observable<Gender> {
        return this.apiService.request("POST", "api/gender", body, options);
    }
    
    public updateGender (body: UpdateGender, options?: ApiOptions): Observable<Gender> {
        return this.apiService.request("PUT", "api/gender", body, options);
    }
    
    public sortGender (body: SortGender, options?: ApiOptions): Observable<any> {
        return this.apiService.request("PUT", "api/gender/sort", body, options);
    }
    
    public deleteGender (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/gender/" + params.id + "", undefined, options);
    }
    
    public queryGender (body: QueryGender, options?: ApiOptions): Observable<QueryResult<Gender>> {
        return this.apiService.request("POST", "api/gender/query", body, options);
    }
    
    // Data Sources
    
    genderDataSource: IDataSource<Gender> = {
        fetch: (query) => this.queryGender(query),
        changed: new EventEmitter()
    }
}

