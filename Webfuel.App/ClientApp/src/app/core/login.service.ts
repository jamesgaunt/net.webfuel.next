import { Injectable } from "@angular/core";
import { Router } from '@angular/router';
import { Observable, map } from "rxjs";
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
      map((result) => {
        this.identityService.token = result.value;
        return true;
      }));
  }

  logout() {
    this.identityService.token = null;
    this.router.navigateByUrl("/login");
  }
}

