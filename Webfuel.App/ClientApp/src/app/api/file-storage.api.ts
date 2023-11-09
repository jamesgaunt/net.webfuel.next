import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { FileStorageEntry, StringResult } from './api.types';

@Injectable()
export class FileStorageApi {
    constructor(private apiService: ApiService) { }
    
    public listFiles (params: { fileStorageGroupId: string }, options?: ApiOptions): Observable<Array<FileStorageEntry>> {
        return this.apiService.request<undefined, Array<FileStorageEntry>>("GET", "api/file-storage/files/" + params.fileStorageGroupId + "", undefined, options);
    }
    
    public generateFileSasUri (params: { fileStorageEntryId: string }, options?: ApiOptions): Observable<StringResult> {
        return this.apiService.request<undefined, StringResult>("GET", "api/file-storage/sas-uri/" + params.fileStorageEntryId + "", undefined, options);
    }
    
    public deleteFiles (params: { fileStorageEntryId: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/file-storage/files/" + params.fileStorageEntryId + "", undefined, options);
    }
}

