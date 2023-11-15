import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { DeveloperRoutingModule } from './developer-routing.module';

import { TestComponent } from './test/test.component';
import { UserLoginComponent } from './user-login/user-login.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DeveloperRoutingModule
  ],
  declarations: [
    TestComponent,
    UserLoginComponent
  ]
})
export class DeveloperModule { }
