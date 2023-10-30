import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { SharedModule } from './shared/shared.module';
import { ApiModule } from './api/api.module';
import { CoreModule } from './core/core.module';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { LoginComponent } from './login/login/login.component';
import { ForgottenPasswordComponent } from './login/forgotten-password/forgotten-password.component';
import { ResetPasswordComponent } from './login/reset-password/reset-password.component';
import { SupportRequestFormComponent } from './external/support-request-form.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ForgottenPasswordComponent,
    ResetPasswordComponent,
    SupportRequestFormComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    SharedModule,
    AppRoutingModule,
    ApiModule,
    CoreModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
