import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ReportApi } from 'api/report.api';
import { Report } from '../../../../api/api.types';
import { CreateReportDialog } from '../dialogs/create-report/create-report.dialog';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { ReportGroupApi } from '../../../../api/report-group.api';
import { StaticDataCache } from '../../../../api/static-data.cache';

@Component({
  selector: 'report-list',
  templateUrl: './report-list.component.html'
})
export class ReportListComponent {
  constructor(
    private router: Router,
    private createReportDialog: CreateReportDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public reportApi: ReportApi,
    public reportGroupApi: ReportGroupApi,
    public staticDataCache: StaticDataCache,
  ) {
  }

  add() {
    this.createReportDialog.open();
  }

  edit(item: Report) {
    this.router.navigate(['report/report-item', item.id]);
  }

  delete(item: Report) {
    this.confirmDeleteDialog.open({ title: "Report " }).subscribe(() => {
      this.reportApi.delete({ id: item.id }, { successGrowl: "Report  Deleted" }).subscribe();
    });
  }
}
