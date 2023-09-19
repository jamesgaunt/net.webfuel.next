import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler, HttpResponse } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { GrowlService } from '../growl.service';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {
  constructor(private growlService: GrowlService) { }

  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(httpRequest).pipe(
      tap({
        next: (event) => {
          return event;
        },
        error: (error) => {
          console.log(error);

          if (error.status === 401) {
            this.growlService.growlDanger("Request not authorised");
          }

          if (error.status === 500 && error.error) {
            this.growlService.growlDanger(error.error);
          }
        }
      }));
  }
}
