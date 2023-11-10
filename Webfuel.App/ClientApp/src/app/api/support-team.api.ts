import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateSupportTeam, SupportTeam, UpdateSupportTeam, SortSupportTeam, QuerySupportTeam, QueryResult } from './api.types';

@Injectable()
export class SupportTeamApi implements IDataSource<SupportTeam, QuerySupportTeam, CreateSupportTeam, UpdateSupportTeam> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateSupportTeam, options?: ApiOptions): Observable<SupportTeam> {
        return this.apiService.request<CreateSupportTeam, SupportTeam>("POST", "api/support-team", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateSupportTeam, options?: ApiOptions): Observable<SupportTeam> {
        return this.apiService.request<UpdateSupportTeam, SupportTeam>("PUT", "api/support-team", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortSupportTeam, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortSupportTeam, any>("PUT", "api/support-team/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/support-team/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QuerySupportTeam, options?: ApiOptions): Observable<QueryResult<SupportTeam>> {
        return this.apiService.request<QuerySupportTeam, QueryResult<SupportTeam>>("POST", "api/support-team/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<SupportTeam> {
        return this.apiService.request<undefined, SupportTeam>("GET", "api/support-team/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static supportTeamResolver(param: string): ResolveFn<SupportTeam> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<SupportTeam> => {
            return inject(SupportTeamApi).get({id: route.paramMap.get(param)! });
        };
    }
}

