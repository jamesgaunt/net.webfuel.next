import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateProfessionalBackground, ProfessionalBackground, UpdateProfessionalBackground, SortProfessionalBackground, QueryProfessionalBackground, QueryResult } from './api.types';

@Injectable()
export class ProfessionalBackgroundApi implements IDataSource<ProfessionalBackground, QueryProfessionalBackground, CreateProfessionalBackground, UpdateProfessionalBackground> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateProfessionalBackground, options?: ApiOptions): Observable<ProfessionalBackground> {
        return this.apiService.request<CreateProfessionalBackground, ProfessionalBackground>("POST", "api/professional-background", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateProfessionalBackground, options?: ApiOptions): Observable<ProfessionalBackground> {
        return this.apiService.request<UpdateProfessionalBackground, ProfessionalBackground>("PUT", "api/professional-background", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortProfessionalBackground, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortProfessionalBackground, any>("PUT", "api/professional-background/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/professional-background/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryProfessionalBackground, options?: ApiOptions): Observable<QueryResult<ProfessionalBackground>> {
        return this.apiService.request<QueryProfessionalBackground, QueryResult<ProfessionalBackground>>("POST", "api/professional-background/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

