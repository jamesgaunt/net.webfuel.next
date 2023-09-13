import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService, ApiOptions } from '../core/api.service';
import { IEventLog, IQuery, IQueryResult } from './api.types';

@Injectable()
export class EventLogApi {
    constructor(private apiService: ApiService) { }
    
    public insert (params: { eventLog: IEventLog }, options?: ApiOptions): Observable<IEventLog> {
        options = options || {};
        return this.apiService.POST("api/EventLog?r=" + Math.random(), params.eventLog, options).pipe(map((res) => <IEventLog>res.body));
    }
    
    public query (params: { query: IQuery }, options?: ApiOptions): Observable<IQueryResult<IEventLog>> {
        options = options || {};
        options.retryCount = options.retryCount || 3;
        return this.apiService.POST("api/EventLog/query?r=" + Math.random(), params.query, options).pipe(map((res) => <IQueryResult<IEventLog>>res.body));
    }
}

