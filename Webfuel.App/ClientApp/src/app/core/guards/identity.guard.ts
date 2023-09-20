import { Injectable, inject } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpRequest, HttpHandler, HttpResponse } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { GrowlService } from '../growl.service';
import _ from '../../shared/underscore';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { IdentityService } from '../identity.service';

export const isAuthenticated: CanActivateFn =
  (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    if (inject(IdentityService).isAuthenticated)
      return true;
    inject(Router).navigateByUrl("/login");
    return false;
  };
