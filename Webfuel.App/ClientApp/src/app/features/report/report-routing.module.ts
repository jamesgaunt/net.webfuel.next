import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportIndexComponent } from './report-index/report-index.component';
import { ReportRunnerComponent } from './report-runner/report-runner.component';
import { ReportApi } from '../../api/report.api';

const routes: Routes = [
  {
    path: 'report-index',
    component: ReportIndexComponent,
    data: { activeSideMenu: 'Reports' }
  },
  {
    path: 'report-runner/:id',
    component: ReportRunnerComponent,
    resolve: { report: ReportApi.reportResolver('id') },
    data: { activeSideMenu: 'Reports' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
