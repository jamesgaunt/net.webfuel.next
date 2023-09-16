import _ from '../shared/underscore';
import { HttpInterceptor, HttpHandler, HttpClient, HttpEvent, HttpResponse, HttpRequest, HttpHeaders } from '@angular/common/http';
import { Observable, Observer, BehaviorSubject, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { GrowlService } from './growl.service';
import { IValidationError } from '../api/api.types';

export interface ApiOptions {
  failureGrowl?: string;
  successGrowl?: string;
}

@Injectable()
export class ApiService {

  constructor(
    private httpClient: HttpClient,
    private growlService: GrowlService
  ) { }

  public COMMAND<TRequest, TResponse>(url: string, data: TRequest, options?: ApiOptions): Observable<TResponse> {
    options = options || {};

    var observable = <Observable<TResponse>>this.httpClient.post(url + "?r=" + Math.random(), data, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });

    return observable.pipe(
      map(result => {
        if (options?.successGrowl)
          this.growlService.growlSuccess(options.successGrowl);

        return result;
      }),
      catchError(err => {
        this.processError(err.error);
        return throwError(() => err);
      })
    );
  }

  growlError(message: string) {
    this.growlService.growlDanger(message);
  }

  processError(error: any) {
    if (!error)
      return;

    if (error.errorType == "Validation Error")
      return this.processValidationError(error);
  }

  processValidationError(error: IValidationError) {
    _.forEach(error.errors, (x) => {
      this.growlError(x.errorMessage);
    });
  }
}
