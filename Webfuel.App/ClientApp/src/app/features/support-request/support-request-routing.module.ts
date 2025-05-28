import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SupportRequestApi } from '../../api/support-request.api';
import { DeactivateService } from '../../core/deactivate.service';

import { SupportRequestFilesComponent } from './support-request/support-request-files/support-request-files.component';
import { SupportRequestHistoryComponent } from './support-request/support-request-history/support-request-history.component';
import { SupportRequestItemComponent } from './support-request/support-request-item/support-request-item.component';
import { SupportRequestListComponent } from './support-request/support-request-list/support-request-list.component';
import { SupportRequestPrintoutComponent } from './support-request/support-request-printout/support-request-printout.component';
import { SupportRequestResearcherComponent } from './support-request/support-request-researcher/support-request-researcher.component';

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
    path: 'support-request-printout/:id',
    component: SupportRequestPrintoutComponent,
    resolve: { supportRequest: SupportRequestApi.supportRequestResolver('id') },
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
