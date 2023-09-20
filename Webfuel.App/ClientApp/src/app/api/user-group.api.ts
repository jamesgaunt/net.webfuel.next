import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService, ApiOptions } from '../core/api.service';
import { ICreateUserGroup, IUserGroup, IUpdateUserGroup, IQueryUserGroupListView, IQueryResult, IUserGroupListView } from './api.types';

@Injectable()
export class UserGroupApi {
    constructor(private apiService: ApiService) { }
    
    public createUserGroup (body: ICreateUserGroup, options?: ApiOptions): Observable<IUserGroup> {
        return this.apiService.request("POST", "api/create-user-group", body, options);
    }
    
    public updateUserGroup (body: IUpdateUserGroup, options?: ApiOptions): Observable<IUserGroup> {
        return this.apiService.request("PUT", "api/update-user-group", body, options);
    }
    
    public deleteUserGroup (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/delete-user-group/" + params.id + "", undefined, options);
    }
    
    public queryUserGroupListView (body: IQueryUserGroupListView, options?: ApiOptions): Observable<IQueryResult<IUserGroupListView>> {
        return this.apiService.request("POST", "api/query-user-group-list-view", body, options);
    }
    
    public resolveUserGroup (params: { id: string }, options?: ApiOptions): Observable<IUserGroup> {
        return this.apiService.request("GET", "api/resolve-user-group/" + params.id + "", undefined, options);
    }
}

