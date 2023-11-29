import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ReportGroupListComponent } from './report-group/report-group-list/report-group-list.component';
import { ReportGroupItemComponent } from './report-group/report-group-item/report-group-item.component';

import { ReportDesignerComponent } from './report-designer/report-designer.component';

import { ReportGroupApi } from '../../api/report-group.api';
import { DeactivateService } from '../../core/deactivate.service';
import { ConfigurationService } from '../../core/configuration.service';
import { ReportListComponent } from './report/report-list/report-list.component';

const routes: Routes = [
  {
    path: 'report-group-list',
    component: ReportGroupListComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'report-group-item/:id',
    component: ReportGroupItemComponent,
    resolve: { reportGroup: ReportGroupApi.reportGroupResolver('id') },
    canDeactivate: [DeactivateService.isPristine<ReportGroupItemComponent>()],
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'report-list',
    component: ReportListComponent,
    data: { activeSideMenu: 'Configuration' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportingRoutingModule { }
