import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { LoginUser, StringResult, ChangeUserPassword, SendUserPasswordResetEmail, ResetUserPassword } from './api.types';

@Injectable()
export class UserLoginApi {
    constructor(private apiService: ApiService) { }
    
    public login (body: LoginUser, options?: ApiOptions): Observable<StringResult> {
        return this.apiService.request<LoginUser, StringResult>("POST", "api/user/login", body, options);
    }
    
    public changePassword (body: ChangeUserPassword, options?: ApiOptions): Observable<any> {
        return this.apiService.request<ChangeUserPassword, any>("POST", "api/user/change-password", body, options);
    }
    
    public sendPasswordResetEmail (body: SendUserPasswordResetEmail, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SendUserPasswordResetEmail, any>("POST", "api/user/send-password-reset-email", body, options);
    }
    
    public resetPassword (body: ResetUserPassword, options?: ApiOptions): Observable<any> {
        return this.apiService.request<ResetUserPassword, any>("POST", "api/user/reset-password", body, options);
    }
}
