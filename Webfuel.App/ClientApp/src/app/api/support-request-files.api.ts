import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { ListSupportRequestFiles, FileStorageEntry } from './api.types';

@Injectable()
export class SupportRequestFilesApi implements IDataSource<SupportRequestFiles, QuerySupportRequestFiles, any, any> {
    constructor(private apiService: ApiService) { }
    
    public listFiles (body: ListSupportRequestFiles, options?: ApiOptions): Observable<Array<FileStorageEntry>> {
        return this.apiService.request<ListSupportRequestFiles, Array<FileStorageEntry>>("POST", "api/support-request/list-files", body, options);
    }
    
    changed = new EventEmitter<any>();
}

