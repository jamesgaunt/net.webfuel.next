import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ErrorService } from '../error.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private errorService: ErrorService) {
  }

  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(httpRequest).pipe(
      tap({
        error: (err: HttpErrorResponse) => {
          this.errorService.interceptError(err);
        }
      }));
  }
}
