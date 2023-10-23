import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { CreateHowDidYouFindUs, HowDidYouFindUs, UpdateHowDidYouFindUs, SortHowDidYouFindUs, QueryHowDidYouFindUs, QueryResult } from './api.types';

@Injectable()
export class HowDidYouFindUsApi implements IDataSource<HowDidYouFindUs, QueryHowDidYouFindUs, CreateHowDidYouFindUs, UpdateHowDidYouFindUs> {
    constructor(private apiService: ApiService) { }
    
    public create (body: CreateHowDidYouFindUs, options?: ApiOptions): Observable<HowDidYouFindUs> {
        return this.apiService.request<CreateHowDidYouFindUs, HowDidYouFindUs>("POST", "api/how-did-you-find-us", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public update (body: UpdateHowDidYouFindUs, options?: ApiOptions): Observable<HowDidYouFindUs> {
        return this.apiService.request<UpdateHowDidYouFindUs, HowDidYouFindUs>("PUT", "api/how-did-you-find-us", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public sort (body: SortHowDidYouFindUs, options?: ApiOptions): Observable<any> {
        return this.apiService.request<SortHowDidYouFindUs, any>("PUT", "api/how-did-you-find-us/sort", body, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public delete (params: { id: string }, options?: ApiOptions): Observable<any> {
        return this.apiService.request<undefined, any>("DELETE", "api/how-did-you-find-us/" + params.id + "", undefined, options).pipe(tap(_ => this.changed.emit()));
    }
    
    public query (body: QueryHowDidYouFindUs, options?: ApiOptions): Observable<QueryResult<HowDidYouFindUs>> {
        return this.apiService.request<QueryHowDidYouFindUs, QueryResult<HowDidYouFindUs>>("POST", "api/how-did-you-find-us/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

