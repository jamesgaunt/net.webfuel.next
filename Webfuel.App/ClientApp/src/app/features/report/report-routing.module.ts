import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportIndexComponent } from './report-index/report-index.component';

const routes: Routes = [
  {
    path: 'report-index',
    component: ReportIndexComponent,
    data: { activeSideMenu: 'Reports' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
