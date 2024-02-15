import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';

@Injectable()
export class ProjectAdviserApi {
    constructor(private apiService: ApiService) { }
    
    public selectUserIdsByProjectId (params: { projectId: string }, options?: ApiOptions): Observable<Array<string>> {
        return this.apiService.request<undefined, Array<string>>("PUT", "api/project-adviser/select-by-project-id/" + params.projectId + "", undefined, options);
    }
}

