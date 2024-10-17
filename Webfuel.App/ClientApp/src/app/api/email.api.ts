import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { SendEmailRequest } from './api.types';

@Injectable()
export class EmailApi {
    constructor(private apiService: ApiService) { }
    
    public send (body: SendEmailRequest, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SendEmailRequest, any>("POST", "api/email/send", body, options);
    }
}

