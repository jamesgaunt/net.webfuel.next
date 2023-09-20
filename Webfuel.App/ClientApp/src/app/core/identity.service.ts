import _ from '../shared/underscore';
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, map } from "rxjs";
import { IIdentityToken, ILoginUser } from "../api/api.types";
import { UserApi } from '../api/user.api';
import { Router } from '@angular/router';

@Injectable()
export class IdentityService {

  // Needs to match the key in IdentityToken
  private key = 'IDENTITY_TOKEN';

  constructor(
    private userApi: UserApi,
    private router: Router
  ) {
    var stored = _.getLocalStorage(this.key);
    if (stored && stored.signature) {
      this._setToken(stored);
    }
  }

  login(request: ILoginUser): Observable<boolean> {
    return this.userApi.loginUser(request).pipe(
      map((result) => {
        if (!result.signature)
          return false;
        this._setToken(result);
        return true;
      }));
  }

  logout() {
    this._clearToken();
    this.router.navigateByUrl("/login");
  }

  get isAuthenticated() {
    return this._token.getValue() != null;
  }

  get token() {
    return this._token;
  }
  private _token: BehaviorSubject<IIdentityToken | null> = new BehaviorSubject<IIdentityToken | null>(null);

  private _setToken(token: IIdentityToken) {
    _.setLocalStorage(this.key, token);
    this._token.next(token);
  }

  private _clearToken() {
    _.setLocalStorage(this.key, null);
    this._token.next(null);
  }
}

