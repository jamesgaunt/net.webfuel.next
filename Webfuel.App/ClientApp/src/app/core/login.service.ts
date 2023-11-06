import { Injectable } from "@angular/core";
import { Router } from '@angular/router';
import { Observable, map, tap } from "rxjs";
import { LoginUser } from '../api/api.types';
import { IdentityService } from './identity.service';
import { UserLoginApi } from "../api/user-login.api";

@Injectable()
export class LoginService {
  constructor(
    private identityService: IdentityService,
    private userLoginApi: UserLoginApi,
    private router: Router,
  ) {
  }

  login(request: LoginUser): Observable<boolean> {
    return this.userLoginApi.login(request).pipe(
      tap({
        next: (result) => {
          console.log(result);
          if (!result || !result.value)
            throw new Error("Invalid username or password");
        },
        error: (err) => {
          console.log(err);
          throw new Error("Invalid username or password");
        }
      }),
      map((result) => {
        this.identityService.token = result.value;
        return !!(result && result.value);
      }));
  }

  logout() {
    this.identityService.token = null;
    this.router.navigateByUrl("/login");
  }
}

