import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DeactivateService } from '../../core/deactivate.service';
import { SupportRequestApi } from '../../api/support-request.api';

import { SupportRequestListComponent } from './support-request/support-request-list/support-request-list.component';
import { SupportRequestItemComponent } from './support-request/support-request-item/support-request-item.component';
import { SupportRequestFilesComponent } from './support-request/support-request-files/support-request-files.component';
import { SupportRequestResearcherComponent } from './support-request/support-request-researcher/support-request-researcher.component';
import { SupportRequestHistoryComponent } from './support-request/support-request-history/support-request-history.component';

const routes: Routes = [
  {
    path: 'support-request-list',
    component: SupportRequestListComponent,
    data: { activeSideMenu: 'Triage' }
  },
  {
    path: 'support-request-item/:id',
    component: SupportRequestItemComponent,
    resolve: { supportRequest: SupportRequestApi.supportRequestResolver('id') },
    canDeactivate: [DeactivateService.isPristine<SupportRequestItemComponent>()],
    data: { activeSideMenu: 'Triage' }
  },
  {
    path: 'support-request-researcher/:id',
    component: SupportRequestResearcherComponent,
    resolve: { supportRequest: SupportRequestApi.supportRequestResolver('id') },
    canDeactivate: [DeactivateService.isPristine<SupportRequestResearcherComponent>()],
    data: { activeSideMenu: 'Triage' }
  },
  {
    path: 'support-request-files/:id',
    component: SupportRequestFilesComponent,
    resolve: { supportRequest: SupportRequestApi.supportRequestResolver('id') },
    canDeactivate: [DeactivateService.isPristine<SupportRequestFilesComponent>()],
    data: { activeSideMenu: 'Triage' }
  },
  {
    path: 'support-request-history/:id',
    component: SupportRequestHistoryComponent,
    resolve: { supportRequest: SupportRequestApi.supportRequestResolver('id') },
    data: { activeSideMenu: 'Triage' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SupportRequestRoutingModule { }
