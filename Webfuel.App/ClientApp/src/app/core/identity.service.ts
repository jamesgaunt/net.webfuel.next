import _ from '../shared/underscore';
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, map } from "rxjs";
import { UserApi } from '../api/user.api';
import { Router } from '@angular/router';
import { LoginUser } from '../api/api.types';
import { ConfigurationService } from './configuration.service';
import { HttpRequest } from '@angular/common/http';
import { EventService } from './event.service';

@Injectable()
export class IdentityService {

  // Needs to match the key in IdentityToken
  static key = 'IDENTITY_TOKEN';

  constructor(
    private userApi: UserApi,
    private router: Router,
    private eventService: EventService
  ) {
    var stored = _.getLocalStorage(IdentityService.key);
    if (_.isString(stored)) {
      this._setToken(stored);
    }
  }

  private static _token: string | null = null;

  static applyTokenToRequest(httpRequest: HttpRequest<any>): HttpRequest<any> {
    if (IdentityService._token == null)
      return httpRequest;

    return httpRequest.clone({
      setHeaders: { key: IdentityService._token }
    });
  }

  get isAuthenticated() {
    return IdentityService._token != null;
  }

  getIdentityHeaders(): { [name: string]: string | string[] } {
    return { }
  }

  private _setToken(token: string | null) {
    IdentityService._token = token;
    _.setLocalStorage(IdentityService.key, token);
    this.eventService.onIdentityChanged();
  }
  
  login(request: LoginUser): Observable<boolean> {
    return this.userApi.loginUser(request).pipe(
      map((result) => {
        this._setToken(result.value);
        return true;
      }));
  }

  logout() {
    this._setToken(null);
    this.router.navigateByUrl("/login");
  }
}

