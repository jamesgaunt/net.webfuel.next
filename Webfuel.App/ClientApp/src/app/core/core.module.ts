import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizationInterceptor } from './interceptors/authorization.interceptor';
import { LoggingInterceptor } from './interceptors/logging.interceptor';

import { ConfirmDialogComponent } from './dialogs/confirm-dialog.component';
import { ConfirmDeleteDialogComponent } from './dialogs/confirm-delete-dialog.component';
import { ConfirmDeactivateDialogComponent } from './dialogs/confirm-deactivate-dialog.component';
import { DatePickerDialogComponent } from './dialogs/date-picker-dialog.component';

import { ApiService } from './api.service';
import { DialogService } from './dialog.service';
import { GrowlService } from './growl.service';
import { FormService } from './form.service';
import { IdentityService } from './identity.service';
import { ErrorService } from './error.service';
import { DeactivateService } from './deactivate.service';
import { StaticDataService } from './static-data.service';
import { ConfigurationService } from './configuration.service';
import { LoginService } from './login.service';
import { QueryService } from './query.service';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
  ],
  declarations: [
    ConfirmDialogComponent,
    ConfirmDeleteDialogComponent,
    ConfirmDeactivateDialogComponent,
    DatePickerDialogComponent
  ],
  providers: [
    ApiService,
    LoginService,
    IdentityService,
    StaticDataService,
    ConfigurationService,
    DialogService,
    GrowlService,
    FormService,
    ErrorService,
    QueryService,
    DeactivateService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizationInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoggingInterceptor,
      multi: true
    },
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

