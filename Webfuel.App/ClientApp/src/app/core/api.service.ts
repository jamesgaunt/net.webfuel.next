import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { GrowlService } from './growl.service';

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

    var observable = <Observable<TResponse>>this.httpClient.request(
      method, url, {
        body: body,
        headers: { 'Content-Type': 'application/json' },
        params: { 'r': Math.random() }
      });

    return observable.pipe(
      map(result => {
        if (options?.successGrowl)
          this.growlService.growlSuccess(options.successGrowl);
        return result;
      }),
      catchError(err => {
        if (options?.errorHandler)
          options.errorHandler.handleError(err);
        return throwError(() => err);
      })
    );
  }
}
