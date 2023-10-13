import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateUserGroup, UserGroup, UpdateUserGroup, QueryUserGroup, QueryResult } from './api.types';

@Injectable()
export class UserGroupApi {
    constructor(private apiService: ApiService) { }
    
    public createUserGroup (body: CreateUserGroup, options?: ApiOptions): Observable<UserGroup> {
        return this.apiService.request("POST", "api/user-group", body, options);
    }
    
    public updateUserGroup (body: UpdateUserGroup, options?: ApiOptions): Observable<UserGroup> {
        return this.apiService.request("PUT", "api/user-group", body, options);
    }
    
    public deleteUserGroup (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/user-group/" + params.id + "", undefined, options);
    }
    
    public queryUserGroup (body: QueryUserGroup, options?: ApiOptions): Observable<QueryResult<UserGroup>> {
        return this.apiService.request("POST", "api/user-group/query", body, options);
    }
    
    public resolveUserGroup (params: { id: string }, options?: ApiOptions): Observable<UserGroup> {
        return this.apiService.request("GET", "api/user-group/" + params.id + "", undefined, options);
    }
    
    // Resolvers
    
    static userGroupResolver(param: string): ResolveFn<UserGroup> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<UserGroup> => {
            return inject(UserGroupApi).resolveUserGroup({id: route.paramMap.get(param)! });
        };
    }
    
    // Data Sources
    
    userGroupDataSource: IDataSource<UserGroup> = {
        fetch: (query) => this.queryUserGroup(query),
        changed: new EventEmitter()
    }
}

