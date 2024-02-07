import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ReportGroupListComponent } from './report-group/report-group-list/report-group-list.component';
import { ReportGroupItemComponent } from './report-group/report-group-item/report-group-item.component';

import { ReportListComponent } from './report/report-list/report-list.component';
import { ReportItemComponent } from './report/report-item/report-item.component';
import { ReportDesignerComponent } from './report-designer/report-designer.component';

import { ReportGroupApi } from '../../api/report-group.api';
import { DeactivateService } from '../../core/deactivate.service';
import { ConfigurationService } from '../../core/configuration.service';
import { ReportApi } from '../../api/report.api';

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
    data: { activeSideMenu: 'Reports' }
  },
  {
    path: 'report-item/:id',
    component: ReportItemComponent,
    resolve: { report: ReportApi.reportResolver('id') },
    canDeactivate: [DeactivateService.isPristine<ReportItemComponent>()],
    data: { activeSideMenu: 'Reports' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportingRoutingModule { }
