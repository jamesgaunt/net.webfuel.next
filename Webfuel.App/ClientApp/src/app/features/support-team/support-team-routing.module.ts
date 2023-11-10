import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SupportTeamListComponent } from './support-team/support-team-list/support-team-list.component';
import { SupportTeamItemComponent } from './support-team/support-team-item/support-team-item.component';

import { SupportTeamApi } from '../../api/support-team.api';
import { DeactivateService } from '../../core/deactivate.service';

const routes: Routes = [
  {
    path: 'support-team-list',
    component: SupportTeamListComponent,
    data: { activeSideMenu: 'Support Teams' }
  },
  {
    path: 'support-team-item/:id',
    component: SupportTeamItemComponent,
    resolve: { supportTeam: SupportTeamApi.supportTeamResolver('id') },
    canDeactivate: [DeactivateService.isPristine<SupportTeamItemComponent>()],
    data: { activeSideMenu: 'Support Teams' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SupportTeamRoutingModule { }
