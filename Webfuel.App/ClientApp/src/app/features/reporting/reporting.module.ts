import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ReportingRoutingModule } from './reporting-routing.module';

import { ReportGroupListComponent } from './report-group/report-group-list/report-group-list.component';
import { ReportGroupItemComponent } from './report-group/report-group-item/report-group-item.component';
import { ReportGroupTabsComponent } from './report-group/report-group-tabs/report-group-tabs.component';

import { ReportDesignerComponent } from './report-designer/report-designer.component';

import { CreateReportGroupDialog, CreateReportGroupDialogComponent } from './report-group/dialogs/create-report-group/create-report-group.dialog';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReportingRoutingModule
  ],
  declarations: [
    ReportGroupListComponent,
    ReportGroupItemComponent,
    ReportGroupTabsComponent,
    ReportDesignerComponent,

    CreateReportGroupDialogComponent,
  ],
  providers: [
    CreateReportGroupDialog,
  ]
})
export class ReportingModule { }
