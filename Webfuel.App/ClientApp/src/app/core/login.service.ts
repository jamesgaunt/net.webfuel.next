import { Injectable } from "@angular/core";
import { Router } from '@angular/router';
import { Observable, map } from "rxjs";
import { LoginUser } from '../api/api.types';
import { UserApi } from '../api/user.api';
import { IdentityService } from './identity.service';

@Injectable()
export class LoginService {
  constructor(
    private identityService: IdentityService,
    private userApi: UserApi,
    private router: Router,
  ) {
  }
  
  login(request: LoginUser): Observable<boolean> {
    return this.userApi.login(request).pipe(
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

