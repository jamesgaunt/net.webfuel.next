import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateUserActivity, UserActivity, UpdateUserActivity, QueryUserActivity, QueryResult, ReportStep } from './api.types';

@Injectable()
export class UserActivityApi implements IDataSource<UserActivity, QueryUserActivity, CreateUserActivity, UpdateUserActivity> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateUserActivity, options?: ApiOptions): Observable<UserActivity> {
        return this.apiService.request<CreateUserActivity, UserActivity>("POST", "api/user-activity", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateUserActivity, options?: ApiOptions): Observable<UserActivity> {
        return this.apiService.request<UpdateUserActivity, UserActivity>("PUT", "api/user-activity", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/user-activity/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryUserActivity, options?: ApiOptions): Observable<QueryResult<UserActivity>> {
        return this.apiService.request<QueryUserActivity, QueryResult<UserActivity>>("POST", "api/user-activity/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<UserActivity> {
        return this.apiService.request<undefined, UserActivity>("GET", "api/user-activity/" + params.id + "", undefined, options);
    }
    
    public export (body: QueryUserActivity, options?: ApiOptions): Observable<ReportStep> {
        return this.apiService.request<QueryUserActivity, ReportStep>("PUT", "api/user-activity/export", body, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static userActivityResolver(param: string): ResolveFn<UserActivity> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<UserActivity> => {
            return inject(UserActivityApi).get({id: route.paramMap.get(param)! });
        };
    }
}

