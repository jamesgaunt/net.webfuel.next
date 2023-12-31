import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { GrowlService } from './growl.service';
import { environment } from '../../environments/environment';

export interface IApiErrorHandler {
  handleError(error: any): void;
}

export interface ApiOptions {
  successGrowl?: string;
  errorHandler?: IApiErrorHandler;
}

@Injectable()
export class ApiService {

  constructor(
    private httpClient: HttpClient,
    private growlService: GrowlService
  ) { }

  public request<TRequest, TResponse>(method: string, url: string, body?: TRequest, options?: ApiOptions): Observable<TResponse> {
    options = options || {};

    var hostedUrl = environment.apiHost + url;

    var observable = <Observable<TResponse>>this.httpClient.request(
      method, hostedUrl, {
      body: body,
      headers: { 'Content-Type': 'application/json' },
      params: { 'r': Math.random() }
    });

    return observable.pipe(
      tap(() => {
          if (options?.successGrowl)
            this.growlService.growlSuccess(options.successGrowl);
      }),
      catchError(err => {
        if (options?.errorHandler)
          options.errorHandler.handleError(err);
        return throwError(() => err);
      })
    );
  }

}
