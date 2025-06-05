import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateFileTag, FileTag, UpdateFileTag, SortFileTag, QueryFileTag, QueryResult } from './api.types';

@Injectable()
export class FileTagApi implements IDataSource<FileTag, QueryFileTag, CreateFileTag, UpdateFileTag> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateFileTag, options?: ApiOptions): Observable<FileTag> {
        return this.apiService.request<CreateFileTag, FileTag>("POST", "api/file-tag", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateFileTag, options?: ApiOptions): Observable<FileTag> {
        return this.apiService.request<UpdateFileTag, FileTag>("PUT", "api/file-tag", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortFileTag, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortFileTag, any>("PUT", "api/file-tag/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/file-tag/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryFileTag, options?: ApiOptions): Observable<QueryResult<FileTag>> {
        return this.apiService.request<QueryFileTag, QueryResult<FileTag>>("POST", "api/file-tag/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

