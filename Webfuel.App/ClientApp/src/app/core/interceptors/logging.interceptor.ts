import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ErrorService } from '../error.service';

@Injectable()
export class LoggingInterceptor implements HttpInterceptor {
  constructor(
    private errorService: ErrorService) {
  }

  LOGGING = true;

  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (this.LOGGING) {
      var log = httpRequest.method + ":" + httpRequest.url;
      if (!httpRequest.headers.has('IDENTITY_TOKEN'))
        log += " [anonymous]";
      console.log(log);
    }

    return next.handle(httpRequest).pipe(
      tap({
        next: (event) => {

          if (this.LOGGING) {
            if (event && event.type != 0)
              console.log(event);
          }

          return event;
        },
        error: (err: HttpErrorResponse) => {
          console.log(err.error);
          this.errorService.interceptError(err.error);
        }
      }));
  }
}
