import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { CreateUserGroup, UserGroup, UpdateUserGroup, QueryUserGroup, QueryResult } from './api.types';

@Injectable()
export class UserGroupApi {
    constructor(private apiService: ApiService) { }
    
    public createUserGroup (body: CreateUserGroup, options?: ApiOptions): Observable<UserGroup> {
        return this.apiService.request("POST", "api/create-user-group", body, options);
    }
    
    public updateUserGroup (body: UpdateUserGroup, options?: ApiOptions): Observable<UserGroup> {
        return this.apiService.request("PUT", "api/update-user-group", body, options);
    }
    
    public deleteUserGroup (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/delete-user-group/" + params.id + "", undefined, options);
    }
    
    public queryUserGroup (body: QueryUserGroup, options?: ApiOptions): Observable<QueryResult<UserGroup>> {
        return this.apiService.request("POST", "api/query-user-group", body, options);
    }
    
    public resolveUserGroup (params: { id: string }, options?: ApiOptions): Observable<UserGroup> {
        return this.apiService.request("GET", "api/resolve-user-group/" + params.id + "", undefined, options);
    }
    
    static userGroupResolver(param: string): ResolveFn<UserGroup> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<UserGroup> => {
            return inject(UserGroupApi).resolveUserGroup({id: route.paramMap.get(param)! });
        };
    }
}

