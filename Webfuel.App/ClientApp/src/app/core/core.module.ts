import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizationInterceptor } from './interceptors/authorization.interceptor';

import { ApiService } from './api.service';
import { DialogService } from './dialog.service';
import { GrowlService } from './growl.service';
import { FormService } from './form.service';
import { IdentityService } from './identity.service';

import { ConfirmDeleteDialogComponent } from './dialogs/confirm-delete-dialog.component';
import { ErrorService } from './error.service';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
  ],
  declarations: [
    ConfirmDeleteDialogComponent
  ],
  providers: [
    ApiService,
    IdentityService, 
    DialogService,
    GrowlService,
    FormService,
    ErrorService,
    {
      provide: HTTP_INTERCEPTORS, useClass: AuthorizationInterceptor, multi: true
    }
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error(
        'CoreModule is already loaded. Import it in the AppModule only');
    }
  }
}

