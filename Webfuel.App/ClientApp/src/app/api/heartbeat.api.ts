import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateHeartbeat, Heartbeat, UpdateHeartbeat, QueryHeartbeat, QueryResult } from './api.types';

@Injectable()
export class HeartbeatApi implements IDataSource<Heartbeat, QueryHeartbeat, CreateHeartbeat, UpdateHeartbeat> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateHeartbeat, options?: ApiOptions): Observable<Heartbeat> {
        return this.apiService.request<CreateHeartbeat, Heartbeat>("POST", "api/heartbeat", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateHeartbeat, options?: ApiOptions): Observable<Heartbeat> {
        return this.apiService.request<UpdateHeartbeat, Heartbeat>("PUT", "api/heartbeat", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/heartbeat/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public execute (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("POST", "api/heartbeat/execute/" + params.id + "", undefined, options);
    }
    
    public query (body: QueryHeartbeat, options?: ApiOptions): Observable<QueryResult<Heartbeat>> {
        return this.apiService.request<QueryHeartbeat, QueryResult<Heartbeat>>("POST", "api/heartbeat/query", body, options);
    }
    
    public get (params: { id: string }, options?: ApiOptions): Observable<Heartbeat> {
        return this.apiService.request<undefined, Heartbeat>("GET", "api/heartbeat/" + params.id + "", undefined, options);
    }
    
    changed = new EventEmitter<any>();
    
    // Resolvers
    
    static heartbeatResolver(param: string): ResolveFn<Heartbeat> {
        return (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Heartbeat> => {
            return inject(HeartbeatApi).get({id: route.paramMap.get(param)! });
        };
    }
}

