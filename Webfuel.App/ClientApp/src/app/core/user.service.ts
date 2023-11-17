import { Injectable } from "@angular/core";
import { Query, QueryFilter, QueryResult, QuerySort, User } from "../api/api.types";
import { QueryOp } from "../api/api.enums";
import { BehaviorSubject, Observable } from "rxjs";
import _ from 'shared/common/underscore';
import { UserApi } from "../api/user.api";

@Injectable()
export class UserService {
  constructor(
    private userApi: UserApi
  ) {
  }

  formatUser(id?: string | null): Observable<string> {
    if (!id)
      return this._formatUserEmpty;

    if (this._formatUserCache[id] == undefined) {
      this._formatUserCache[id] = new BehaviorSubject("");

      this.userApi.get({ id: id }).subscribe((result) => {
        this._formatUserCache[id].next(this._formatUser(result));
      })
    }
    return this._formatUserCache[id];
  }

  private _formatUserEmpty = new BehaviorSubject<string>("");

  private _formatUserCache: { [key: string]: BehaviorSubject<string> } = {}; // Preserved for lifetime of client application

  private _formatUser(user: User) {
    return user.firstName + " " + user.lastName;
  }
}

