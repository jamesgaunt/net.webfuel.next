import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateTriageTemplate, TriageTemplate, UpdateTriageTemplate, SortTriageTemplate, GenerateTriageTemplateEmail, SendEmailRequest, QueryTriageTemplate, QueryResult } from './api.types';

@Injectable()
export class TriageTemplateApi implements IDataSource<TriageTemplate, QueryTriageTemplate, CreateTriageTemplate, UpdateTriageTemplate> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateTriageTemplate, options?: ApiOptions): Observable<TriageTemplate> {
        return this.apiService.request<CreateTriageTemplate, TriageTemplate>("POST", "api/triage-template", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateTriageTemplate, options?: ApiOptions): Observable<TriageTemplate> {
        return this.apiService.request<UpdateTriageTemplate, TriageTemplate>("PUT", "api/triage-template", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortTriageTemplate, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortTriageTemplate, any>("PUT", "api/triage-template/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/triage-template/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public generateEmail (body: GenerateTriageTemplateEmail, options?: ApiOptions): Observable<SendEmailRequest> {
        return this.apiService.request<GenerateTriageTemplateEmail, SendEmailRequest>("PUT", "api/triage-template/generate-email", body, options);
    }
    
    public query (body: QueryTriageTemplate, options?: ApiOptions): Observable<QueryResult<TriageTemplate>> {
        return this.apiService.request<QueryTriageTemplate, QueryResult<TriageTemplate>>("POST", "api/triage-template/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<TriageTemplate> {
        return this.apiService.request<undefined, TriageTemplate>("GET", "api/triage-template/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static triageTemplateResolver(param: string): ResolveFn<TriageTemplate> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<TriageTemplate> => {
            return inject(TriageTemplateApi).get({id: route.paramMap.get(param)! });
        };
    }
}

