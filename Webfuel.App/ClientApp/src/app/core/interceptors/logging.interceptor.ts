import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ErrorService } from '../error.service';

@Injectable()
export class LoggingInterceptor implements HttpInterceptor {
  constructor(
    private errorService: ErrorService) {
  }

  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    var requestTitle = httpRequest.method + ":" + httpRequest.url;
    if (!httpRequest.headers.has('IDENTITY_TOKEN'))
      requestTitle += " [anonymous]";

    // console.debug(requestTitle); // enable this to track when the request starts

    return next.handle(httpRequest).pipe(
      tap({
        next: (event) => {

          if (event && event.type != 0) {
            console.log(requestTitle, httpRequest.body, event);
          }

          return event;
        },
        error: (err: HttpErrorResponse) => {
          console.warn(requestTitle, err.error);
          this.errorService.interceptError(err.error);
        }
      }));
  }
}
