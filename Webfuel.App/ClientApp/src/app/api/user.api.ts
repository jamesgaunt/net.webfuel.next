import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService, ApiOptions } from '../core/api.service';
import { ICreateUser, IUser, IUpdateUser, IQueryUserListView, IQueryResult, IUserListView, ILoginUser, IIdentityToken } from './api.types';

@Injectable()
export class UserApi {
    constructor(private apiService: ApiService) { }
    
    public createUser (body: ICreateUser, options?: ApiOptions): Observable<IUser> {
        return this.apiService.request("POST", "api/create-user", body, options);
    }
    
    public updateUser (body: IUpdateUser, options?: ApiOptions): Observable<IUser> {
        return this.apiService.request("PUT", "api/update-user", body, options);
    }
    
    public deleteUser (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/delete-user/" + params.id + "", undefined, options);
    }
    
    public queryUserListView (body: IQueryUserListView, options?: ApiOptions): Observable<IQueryResult<IUserListView>> {
        return this.apiService.request("POST", "api/query-user-list-view", body, options);
    }
    
    public resolveUser (params: { id: string }, options?: ApiOptions): Observable<IUser> {
        return this.apiService.request("GET", "api/resolve-user/" + params.id + "", undefined, options);
    }
    
    public loginUser (body: ILoginUser, options?: ApiOptions): Observable<IIdentityToken> {
        return this.apiService.request("POST", "api/login-user", body, options);
    }
}

