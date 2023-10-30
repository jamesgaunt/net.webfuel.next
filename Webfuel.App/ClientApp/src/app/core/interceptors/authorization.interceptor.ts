import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { IdentityService } from '../identity.service';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {
  constructor() {
  }

  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const identityService = inject(IdentityService);

    const token = identityService.token;
    if (token == null)
      return next.handle(httpRequest);

    const authRequest = httpRequest.clone({
      headers: httpRequest.headers.set('identity-token', token)
    });

    return next.handle(authRequest);
  }
}
