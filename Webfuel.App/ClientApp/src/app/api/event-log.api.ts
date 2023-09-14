import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService, ApiOptions } from '../core/api.service';
import { IEventLog, IRepositoryQuery, IRepositoryQueryResult } from './api.types';

@Injectable()
export class EventLogApi {
    constructor(private apiService: ApiService) { }
    
    public insert (params: { eventLog: IEventLog }, options?: ApiOptions): Observable<IEventLog> {
        options = options || {};
        return this.apiService.POST("api/EventLog?r=" + Math.random(), params.eventLog, options).pipe(map((res) => <IEventLog>res.body));
    }
    
    public query (params: { query: IRepositoryQuery }, options?: ApiOptions): Observable<IRepositoryQueryResult<IEventLog>> {
        options = options || {};
        options.retryCount = options.retryCount || 3;
        return this.apiService.POST("api/EventLog/query?r=" + Math.random(), params.query, options).pipe(map((res) => <IRepositoryQueryResult<IEventLog>>res.body));
    }
}

