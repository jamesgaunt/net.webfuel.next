import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateUser, User, UpdateUser, QueryUser, QueryResult, ReportStep } from './api.types';

@Injectable()
export class UserApi implements IDataSource<User, QueryUser, CreateUser, UpdateUser> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateUser, options?: ApiOptions): Observable<User> {
        return this.apiService.request<CreateUser, User>("POST", "api/user", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateUser, options?: ApiOptions): Observable<User> {
        return this.apiService.request<UpdateUser, User>("PUT", "api/user", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/user/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryUser, options?: ApiOptions): Observable<QueryResult<User>> {
        return this.apiService.request<QueryUser, QueryResult<User>>("POST", "api/user/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<User> {
        return this.apiService.request<undefined, User>("GET", "api/user/" + params.id + "", undefined, options);
    }
    
    public export (body: QueryUser, options?: ApiOptions): Observable<ReportStep> {
        return this.apiService.request<QueryUser, ReportStep>("PUT", "api/user/export", body, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static userResolver(param: string): ResolveFn<User> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<User> => {
            return inject(UserApi).get({id: route.paramMap.get(param)! });
        };
    }
}

