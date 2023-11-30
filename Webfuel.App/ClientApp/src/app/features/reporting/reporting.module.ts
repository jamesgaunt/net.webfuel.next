import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ReportingRoutingModule } from './reporting-routing.module';

import { ReportGroupListComponent } from './report-group/report-group-list/report-group-list.component';
import { ReportGroupItemComponent } from './report-group/report-group-item/report-group-item.component';
import { ReportGroupTabsComponent } from './report-group/report-group-tabs/report-group-tabs.component';

import { ReportListComponent } from './report/report-list/report-list.component';
import { ReportItemComponent } from './report/report-item/report-item.component';

import { ReportDesignerComponent } from './report-designer/report-designer.component';

import { CreateReportGroupDialog, CreateReportGroupDialogComponent } from './report-group/dialogs/create-report-group/create-report-group.dialog';
import { CreateReportDialog, CreateReportDialogComponent } from './report/dialogs/create-report/create-report.dialog';
import { AddReportColumnDialog, AddReportColumnDialogComponent } from './report-designer/dialogs/add-report-column/add-report-column.dialog';

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
    ReportListComponent,
    ReportItemComponent,
    ReportDesignerComponent,

    CreateReportGroupDialogComponent,
    CreateReportDialogComponent,
    AddReportColumnDialogComponent,
  ],
  providers: [
    CreateReportGroupDialog,
    CreateReportDialog,
    AddReportColumnDialog,
  ]
})
export class ReportingModule { }
