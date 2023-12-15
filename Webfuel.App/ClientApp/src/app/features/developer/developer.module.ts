import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { DeveloperRoutingModule } from './developer-routing.module';

import { TestComponent } from './test/test.component';
import { UserLoginComponent } from './user-login/user-login.component';
import { HeartbeatListComponent } from './heartbeat/heartbeat-list/heartbeat-list.component';
import { HeartbeatItemComponent } from './heartbeat/heartbeat-item/heartbeat-item.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DeveloperRoutingModule
  ],
  declarations: [
    TestComponent,
    UserLoginComponent,
    HeartbeatListComponent,
    HeartbeatItemComponent
  ]
})
export class DeveloperModule { }
