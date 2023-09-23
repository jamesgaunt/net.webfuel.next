import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { GrowlService } from '../growl.service';
import _ from '../../shared/underscore';
import { IdentityService } from '../identity.service';
import { ErrorService } from '../error.service';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {
  constructor(
    private errorService: ErrorService) {
  }

  LOGGING = true;

  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    httpRequest = IdentityService.applyTokenToRequest(httpRequest);

    if (this.LOGGING) {
      var log = httpRequest.method + ":" + httpRequest.url;
      if (httpRequest.headers.has(IdentityService.key))
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
