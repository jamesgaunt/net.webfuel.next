import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DeactivateService } from '../../core/deactivate.service';
import { SupportRequestApi } from '../../api/support-request.api';

import { SupportRequestListComponent } from './support-request/support-request-list/support-request-list.component';
import { SupportRequestItemComponent } from './support-request/support-request-item/support-request-item.component';
import { SupportRequestFilesComponent } from './support-request/support-request-files/support-request-files.component';

const routes: Routes = [
  {
    path: 'support-request-list',
    component: SupportRequestListComponent,
    data: { activeSideMenu: 'SupportRequests' }
  },
  {
    path: 'support-request-item/:id',
    component: SupportRequestItemComponent,
    resolve: { supportRequest: SupportRequestApi.supportRequestResolver('id') },
    canDeactivate: [DeactivateService.isPristine<SupportRequestItemComponent>()],
    data: { activeSideMenu: 'Requests' }
  },
  {
    path: 'support-request-files/:id',
    component: SupportRequestFilesComponent,
    resolve: { supportRequest: SupportRequestApi.supportRequestResolver('id') },
    canDeactivate: [DeactivateService.isPristine<SupportRequestFilesComponent>()],
    data: { activeSideMenu: 'Requests' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SupportRequestRoutingModule { }
