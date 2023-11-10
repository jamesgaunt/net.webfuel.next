import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryFileStorageEntry, QueryResult, FileStorageEntry } from './api.types';

@Injectable()
export class FileStorageEntryApi implements IDataSource<FileStorageEntry, QueryFileStorageEntry, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryFileStorageEntry, options?: ApiOptions): Observable<QueryResult<FileStorageEntry>> {
        return this.apiService.request<QueryFileStorageEntry, QueryResult<FileStorageEntry>>("POST", "api/file-storage-entry/query", body, options);
    }
    
    public upload (options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("POST", "api/file-storage-entry/upload", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/file-storage-entry/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    changed = new EventEmitter<any>();
}

