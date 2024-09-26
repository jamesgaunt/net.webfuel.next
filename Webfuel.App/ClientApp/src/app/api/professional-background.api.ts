import { EventEmitter, Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ApiService, ApiOptions } from '../core/api.service';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { IDataSource } from 'shared/common/data-source';
import { QueryProfessionalBackground, QueryResult, ProfessionalBackground } from './api.types';

@Injectable()
export class ProfessionalBackgroundApi implements IDataSource<ProfessionalBackground, QueryProfessionalBackground, any, any> {
    constructor(private apiService: ApiService) { }
    
    public query (body: QueryProfessionalBackground, options?: ApiOptions): Observable<QueryResult<ProfessionalBackground>> {
        return this.apiService.request<QueryProfessionalBackground, QueryResult<ProfessionalBackground>>("POST", "api/professional-background/query", body, options);
    }
    
    changed = new EventEmitter<any>();
}

