import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateTitle, Title, UpdateTitle, SortTitle, QueryTitle, QueryResult } from './api.types';

@Injectable()
export class TitleApi {
    constructor(private apiService: ApiService) { }
    
    public createTitle (body: CreateTitle, options?: ApiOptions): Observable<Title> {
        return this.apiService.request("POST", "api/title", body, options);
    }
    
    public updateTitle (body: UpdateTitle, options?: ApiOptions): Observable<Title> {
        return this.apiService.request("PUT", "api/title", body, options);
    }
    
    public sortTitle (body: SortTitle, options?: ApiOptions): Observable<any> {
        return this.apiService.request("PUT", "api/title/sort", body, options);
    }
    
    public deleteTitle (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/title/" + params.id + "", undefined, options);
    }
    
    public queryTitle (body: QueryTitle, options?: ApiOptions): Observable<QueryResult<Title>> {
        return this.apiService.request("POST", "api/title/query", body, options);
    }
    
    // Data Sources
    
    titleDataSource: IDataSource<Title> = {
        fetch: (query) => this.queryTitle(query),
        changed: new EventEmitter()
    }
}

