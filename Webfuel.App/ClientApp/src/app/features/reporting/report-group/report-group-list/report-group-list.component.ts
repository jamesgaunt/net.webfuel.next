import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ReportGroupApi } from 'api/report-group.api';
import { ReportGroup } from '../../../../api/api.types';
import { CreateReportGroupDialog } from '../dialogs/create-report-group/create-report-group.dialog';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';

@Component({
  selector: 'report-group-list',
  templateUrl: './report-group-list.component.html'
})
export class ReportGroupListComponent {
  constructor(
    private router: Router,
    private createReportGroupDialog: CreateReportGroupDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public reportGroupApi: ReportGroupApi,
  ) {
  }

  add() {
    this.createReportGroupDialog.open();
  }

  edit(item: ReportGroup) {
    this.router.navigate(['reporting/report-group-item', item.id]);
  }

  delete(item: ReportGroup) {
    this.confirmDeleteDialog.open({ title: "Report Group" }).subscribe(() => {
      this.reportGroupApi.delete({ id: item.id }, { successGrowl: "Report Group Deleted" }).subscribe();
    });
  }
}
