import { EventEmitter, Injectable } from "@angular/core";
import _ from '../shared/underscore';

@Injectable()
export class IdentityService {

  private storageKey = 'IDENTITY_TOKEN';

  constructor() {
    var storedToken = _.getLocalStorage(this.storageKey);
    if (_.isString(storedToken)) {
      this.token = storedToken;
    }
  }

  get token() {
    return this._token;
  }
  set token(token: string | null) {
    this._token = token;
    _.setLocalStorage(this.storageKey, token);
    this.identityChanged.emit();
  }
  private _token: string | null = null;

  get isAuthenticated() {
    return this._token != null;
  }

  identityChanged = new EventEmitter<any>();
}

