import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizationInterceptor } from './interceptors/authorization.interceptor';

import { ConfirmDeleteDialogComponent } from './dialogs/confirm-delete-dialog.component';
import { ConfirmDeactivateDialogComponent } from './dialogs/confirm-deactivate-dialog.component';

import { ApiService } from './api.service';
import { DialogService } from './dialog.service';
import { GrowlService } from './growl.service';
import { FormService } from './form.service';
import { IdentityService } from './identity.service';
import { ErrorService } from './error.service';
import { DeactivateService } from './deactivate.service';
import { DatePickerDialogComponent } from './dialogs/date-picker-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
  ],
  declarations: [
    ConfirmDeleteDialogComponent,
    ConfirmDeactivateDialogComponent,
    DatePickerDialogComponent
  ],
  providers: [
    ApiService,
    IdentityService, 
    DialogService,
    GrowlService,
    FormService,
    ErrorService,
    DeactivateService,
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

