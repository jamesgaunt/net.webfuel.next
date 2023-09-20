import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler, HttpResponse } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { GrowlService } from '../growl.service';
import _ from '../../shared/underscore';
import { IdentityService } from '../identity.service';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {
  constructor(
    private identityService: IdentityService,
    private growlService: GrowlService) {
  }

  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const token = this.identityService.token.getValue();

    if (token) {
      httpRequest = httpRequest.clone({
        setHeaders: { "IDENTITY_TOKEN" : JSON.stringify(token) }
      })
    }

    return next.handle(httpRequest).pipe(
      tap({
        next: (event) => {
          return event;
        },
        error: (err) => {

          if (_.isString(err.error)) {
            this.growlService.growlDanger(err.error);
          }

          else if (_.isObject(err.error)) {
            if (_.isString(err.error.message))
              this.growlService.growlDanger(err.error.message);
          }

          else if (_.isString(err.message)) {
            this.growlService.growlDanger(err.message);
          }
        }
      }));
  }
}
