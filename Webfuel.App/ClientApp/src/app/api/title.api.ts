import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateTitle, Title, UpdateTitle, SortTitle, QueryTitle, QueryResult } from './api.types';

@Injectable()
export class TitleApi implements IDataSource<Title, QueryTitle, CreateTitle, UpdateTitle> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateTitle, options?: ApiOptions): Observable<Title> {
        return this.apiService.request<CreateTitle, Title>("POST", "api/title", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateTitle, options?: ApiOptions): Observable<Title> {
        return this.apiService.request<UpdateTitle, Title>("PUT", "api/title", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortTitle, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortTitle, any>("PUT", "api/title/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/title/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryTitle, options?: ApiOptions): Observable<QueryResult<Title>> {
        return this.apiService.request<QueryTitle, QueryResult<Title>>("POST", "api/title/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

