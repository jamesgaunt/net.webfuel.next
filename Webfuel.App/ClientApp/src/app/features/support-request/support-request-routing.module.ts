import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DeactivateService } from '../../core/deactivate.service';
import { SupportRequestApi } from '../../api/support-request.api';

import { SupportRequestListComponent } from './support-request-list/support-request-list.component';
import { SupportRequestItemComponent } from './support-request-item/support-request-item.component';

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
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SupportRequestRoutingModule { }
