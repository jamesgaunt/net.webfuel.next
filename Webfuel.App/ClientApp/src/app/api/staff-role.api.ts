import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateStaffRole, StaffRole, UpdateStaffRole, SortStaffRole, QueryStaffRole, QueryResult } from './api.types';

@Injectable()
export class StaffRoleApi implements IDataSource<StaffRole, QueryStaffRole, CreateStaffRole, UpdateStaffRole> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateStaffRole, options?: ApiOptions): Observable<StaffRole> {
        return this.apiService.request<CreateStaffRole, StaffRole>("POST", "api/staff-role", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateStaffRole, options?: ApiOptions): Observable<StaffRole> {
        return this.apiService.request<UpdateStaffRole, StaffRole>("PUT", "api/staff-role", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortStaffRole, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortStaffRole, any>("PUT", "api/staff-role/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/staff-role/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryStaffRole, options?: ApiOptions): Observable<QueryResult<StaffRole>> {
        return this.apiService.request<QueryStaffRole, QueryResult<StaffRole>>("POST", "api/staff-role/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

