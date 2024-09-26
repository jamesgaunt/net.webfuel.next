import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateProfessionalBackgroundDetail, ProfessionalBackgroundDetail, UpdateProfessionalBackgroundDetail, SortProfessionalBackgroundDetail, QueryProfessionalBackgroundDetail, QueryResult } from './api.types';

@Injectable()
export class ProfessionalBackgroundDetailApi implements IDataSource<ProfessionalBackgroundDetail, QueryProfessionalBackgroundDetail, CreateProfessionalBackgroundDetail, UpdateProfessionalBackgroundDetail> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateProfessionalBackgroundDetail, options?: ApiOptions): Observable<ProfessionalBackgroundDetail> {
        return this.apiService.request<CreateProfessionalBackgroundDetail, ProfessionalBackgroundDetail>("POST", "api/professional-background-detail", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateProfessionalBackgroundDetail, options?: ApiOptions): Observable<ProfessionalBackgroundDetail> {
        return this.apiService.request<UpdateProfessionalBackgroundDetail, ProfessionalBackgroundDetail>("PUT", "api/professional-background-detail", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortProfessionalBackgroundDetail, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortProfessionalBackgroundDetail, any>("PUT", "api/professional-background-detail/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/professional-background-detail/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryProfessionalBackgroundDetail, options?: ApiOptions): Observable<QueryResult<ProfessionalBackgroundDetail>> {
        return this.apiService.request<QueryProfessionalBackgroundDetail, QueryResult<ProfessionalBackgroundDetail>>("POST", "api/professional-background-detail/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

