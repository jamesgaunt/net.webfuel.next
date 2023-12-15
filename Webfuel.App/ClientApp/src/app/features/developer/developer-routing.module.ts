import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TestComponent } from './test/test.component';
import { UserLoginComponent } from './user-login/user-login.component';
import { HeartbeatListComponent } from './heartbeat/heartbeat-list/heartbeat-list.component';
import { HeartbeatItemComponent } from './heartbeat/heartbeat-item/heartbeat-item.component';
import { HeartbeatApi } from '../../api/heartbeat.api';
import { DeactivateService } from '../../core/deactivate.service';

const routes: Routes = [
  {
    path: 'test',
    component: TestComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'user-login',
    component: UserLoginComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'heartbeat-list',
    component: HeartbeatListComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'heartbeat-item/:id',
    component: HeartbeatItemComponent,
    data: { activeSideMenu: 'Configuration' },
    resolve: { heartbeat: HeartbeatApi.heartbeatResolver('id') },
    canDeactivate: [DeactivateService.isPristine<HeartbeatItemComponent>()],
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DeveloperRoutingModule {

}
