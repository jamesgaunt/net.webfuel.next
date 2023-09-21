import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { CreateUser, User, UpdateUser, QueryUser, QueryResult, LoginUser, IdentityToken } from './api.types';
import { FormControl, Validators } from '@angular/forms';

@Injectable()
export class UserApi {
    constructor(private apiService: ApiService) { }
    
    public createUser (body: CreateUser, options?: ApiOptions): Observable<User> {
        return this.apiService.request("POST", "api/create-user", body, options);
    }
    
    public updateUser (body: UpdateUser, options?: ApiOptions): Observable<User> {
        return this.apiService.request("PUT", "api/update-user", body, options);
    }
    
    public deleteUser (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request("DELETE", "api/delete-user/" + params.id + "", undefined, options);
    }
    
    public queryUser (body: QueryUser, options?: ApiOptions): Observable<QueryResult<User>> {
        return this.apiService.request("POST", "api/query-user", body, options);
    }
    
    public resolveUser (params: { id: string }, options?: ApiOptions): Observable<User> {
        return this.apiService.request("GET", "api/resolve-user/" + params.id + "", undefined, options);
    }
    
    public loginUser (body: LoginUser, options?: ApiOptions): Observable<IdentityToken> {
        return this.apiService.request("POST", "api/login-user", body, options);
    }
    
    static userResolver(param: string): ResolveFn<User> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<User> => {
            return inject(UserApi).resolveUser({id: route.paramMap.get(param)! });
        };
    }
}

