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

// Report Filters
import { ReportFilterDateComponent } from './report-filters/date/report-filter-date.component';
import { ReportFilterGroupComponent } from './report-filters/group/report-filter-group.component';
import { ReportFilterNumberComponent } from './report-filters/number/report-filter-number.component';
import { ReportFilterStringComponent } from './report-filters/string/report-filter-string.component';
import { ReportFilterBooleanComponent } from './report-filters/boolean/report-filter-boolean.component';
import { ReportFilterReferenceComponent } from './report-filters/reference/report-filter-reference.component';
import { ReportFilterExpressionComponent } from './report-filters/expression/report-filter-expression.component';

import { CreateReportGroupDialog, CreateReportGroupDialogComponent } from './report-group/dialogs/create-report-group/create-report-group.dialog';
import { CreateReportDialog, CreateReportDialogComponent } from './report/dialogs/create-report/create-report.dialog';
import { ReportDesignService } from './report-design-service/report-design.service';
import { InsertReportColumnDialog, InsertReportColumnDialogComponent } from './report-design-service/dialogs/insert-report-column/insert-report-column.dialog';
import { InsertReportFilterDialog, InsertReportFilterDialogComponent } from './report-design-service/dialogs/insert-report-filter/insert-report-filter.dialog';
import { UpdateReportColumnDialog, UpdateReportColumnDialogComponent } from './report-design-service/dialogs/update-report-column/update-report-column.dialog';
import { DeleteReportColumnDialog, DeleteReportColumnDialogComponent } from './report-design-service/dialogs/delete-report-column/delete-report-column.dialog';
import { DeleteReportFilterDialog, DeleteReportFilterDialogComponent } from './report-design-service/dialogs/delete-report-filter/delete-report-filter.dialog';
import { UpdateReportFilterDialog, UpdateReportFilterDialogComponent } from './report-design-service/dialogs/update-report-filter/update-report-filter.dialog';
import { CopyReportDialog, CopyReportDialogComponent } from './report/dialogs/copy-report/copy-report.dialog';

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

    // Report Filters
    ReportFilterDateComponent,
    ReportFilterGroupComponent,
    ReportFilterNumberComponent,
    ReportFilterStringComponent,
    ReportFilterBooleanComponent,
    ReportFilterReferenceComponent,
    ReportFilterExpressionComponent,

    CreateReportGroupDialogComponent,
    CreateReportDialogComponent,
    CopyReportDialogComponent,

    InsertReportColumnDialogComponent,
    InsertReportFilterDialogComponent,

    UpdateReportColumnDialogComponent,
    UpdateReportFilterDialogComponent,

    DeleteReportColumnDialogComponent,
    DeleteReportFilterDialogComponent,
  ],
  providers: [
    ReportDesignService,

    CreateReportGroupDialog,
    CreateReportDialog,
    CopyReportDialog,

    InsertReportColumnDialog,
    InsertReportFilterDialog,

    UpdateReportColumnDialog,
    UpdateReportFilterDialog,

    DeleteReportColumnDialog,
    DeleteReportFilterDialog,
  ]
})
export class ReportingModule { }
