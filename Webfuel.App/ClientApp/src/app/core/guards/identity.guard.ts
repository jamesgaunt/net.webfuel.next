import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { IdentityService } from '../identity.service';

export const isAuthenticated: CanActivateFn =
  (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    if (inject(IdentityService).isAuthenticated)
      return true;
    inject(Router).navigateByUrl("/login");
    return false;
  };
