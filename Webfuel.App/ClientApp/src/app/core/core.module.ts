import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizationInterceptor } from './interceptors/authorization.interceptor';
import { LoggingInterceptor } from './interceptors/logging.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';

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
import { UserService } from './user.service';
import { ReportService } from './report.service';
import { ReportRunnerDialog, ReportRunnerDialogComponent } from './dialogs/report/report-runner.dialog';
import { ReportLauncherDialog, ReportLauncherDialogComponent } from './dialogs/report/report-launcher.dialog';
import { ReportArgumentComponent } from './dialogs/report/report-argument.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
  ],
  declarations: [
    ReportRunnerDialogComponent,
    ReportLauncherDialogComponent,
    ReportArgumentComponent,
  ],
  exports: [
    ReportRunnerDialogComponent,
    ReportLauncherDialogComponent,
    ReportArgumentComponent,
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
    UserService,
    ReportService,
    DeactivateService,
    ReportRunnerDialog,
    ReportLauncherDialog,
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
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
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

