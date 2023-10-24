import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateUserDiscipline, UserDiscipline, UpdateUserDiscipline, SortUserDiscipline, QueryUserDiscipline, QueryResult } from './api.types';

@Injectable()
export class UserDisciplineApi implements IDataSource<UserDiscipline, QueryUserDiscipline, CreateUserDiscipline, UpdateUserDiscipline> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateUserDiscipline, options?: ApiOptions): Observable<UserDiscipline> {
        return this.apiService.request<CreateUserDiscipline, UserDiscipline>("POST", "api/user-discipline", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateUserDiscipline, options?: ApiOptions): Observable<UserDiscipline> {
        return this.apiService.request<UpdateUserDiscipline, UserDiscipline>("PUT", "api/user-discipline", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortUserDiscipline, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortUserDiscipline, any>("PUT", "api/user-discipline/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/user-discipline/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryUserDiscipline, options?: ApiOptions): Observable<QueryResult<UserDiscipline>> {
        return this.apiService.request<QueryUserDiscipline, QueryResult<UserDiscipline>>("POST", "api/user-discipline/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

