import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateEmailTemplate, EmailTemplate, UpdateEmailTemplate, QueryEmailTemplate, QueryResult } from './api.types';

@Injectable()
export class EmailTemplateApi implements IDataSource<EmailTemplate, QueryEmailTemplate, CreateEmailTemplate, UpdateEmailTemplate> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateEmailTemplate, options?: ApiOptions): Observable<EmailTemplate> {
        return this.apiService.request<CreateEmailTemplate, EmailTemplate>("POST", "api/emailTemplate", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateEmailTemplate, options?: ApiOptions): Observable<EmailTemplate> {
        return this.apiService.request<UpdateEmailTemplate, EmailTemplate>("PUT", "api/emailTemplate", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/emailTemplate/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryEmailTemplate, options?: ApiOptions): Observable<QueryResult<EmailTemplate>> {
        return this.apiService.request<QueryEmailTemplate, QueryResult<EmailTemplate>>("POST", "api/emailTemplate/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<EmailTemplate> {
        return this.apiService.request<undefined, EmailTemplate>("GET", "api/emailTemplate/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static emailTemplateResolver(param: string): ResolveFn<EmailTemplate> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<EmailTemplate> => {
            return inject(EmailTemplateApi).get({id: route.paramMap.get(param)! });
        };
    }
}

