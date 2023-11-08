import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateUserGroup, UserGroup, UpdateUserGroup, UpdateUserGroupClaims, QueryUserGroup, QueryResult } from './api.types';

@Injectable()
export class UserGroupApi implements IDataSource<UserGroup, QueryUserGroup, CreateUserGroup, UpdateUserGroup> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateUserGroup, options?: ApiOptions): Observable<UserGroup> {
        return this.apiService.request<CreateUserGroup, UserGroup>("POST", "api/user-group", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateUserGroup, options?: ApiOptions): Observable<UserGroup> {
        return this.apiService.request<UpdateUserGroup, UserGroup>("PUT", "api/user-group", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public updateClaims (body: UpdateUserGroupClaims, options?: ApiOptions): Observable<UserGroup> {
        return this.apiService.request<UpdateUserGroupClaims, UserGroup>("PUT", "api/user-group/claims", body, options);
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/user-group/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryUserGroup, options?: ApiOptions): Observable<QueryResult<UserGroup>> {
        return this.apiService.request<QueryUserGroup, QueryResult<UserGroup>>("POST", "api/user-group/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<UserGroup> {
        return this.apiService.request<undefined, UserGroup>("GET", "api/user-group/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static userGroupResolver(param: string): ResolveFn<UserGroup> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<UserGroup> => {
            return inject(UserGroupApi).get({id: route.paramMap.get(param)! });
        };
    }
}

