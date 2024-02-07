import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ReportApi } from 'api/report.api';
import { ClientConfiguration, Report } from '../../../../api/api.types';
import { CreateReportDialog } from '../dialogs/create-report/create-report.dialog';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { ReportGroupApi } from '../../../../api/report-group.api';
import { StaticDataCache } from '../../../../api/static-data.cache';
import { CopyReportDialog } from '../dialogs/copy-report/copy-report.dialog';
import { UserService } from '../../../../core/user.service';
import { FormControl, FormGroup } from '@angular/forms';
import { ReportLauncherDialog } from '../../../../core/dialogs/report/report-launcher.dialog';
import { IdentityService } from '../../../../core/identity.service';
import { ConfigurationService } from '../../../../core/configuration.service';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'report-list',
  templateUrl: './report-list.component.html'
})
export class ReportListComponent {
  constructor(
    private router: Router,
    private createReportDialog: CreateReportDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    private copyReportDialog: CopyReportDialog,
    public reportApi: ReportApi,
    public reportGroupApi: ReportGroupApi,
    public userService: UserService,
    public staticDataCache: StaticDataCache,
    public reportLauncherDialog: ReportLauncherDialog,
    public configurationService: ConfigurationService,
  ) {
    this.configuration = configurationService.configuration;
  }

  configuration: BehaviorSubject<ClientConfiguration | null>;
  
  canEditReport(report: Report) {
    if (this.configuration.value == null)
      return false;
    return report.ownerUserId == this.configuration.value.userId;
  }

  filterForm = new FormGroup({
    reportGroupId: new FormControl<string | null>(null),
    name: new FormControl<string>('', { nonNullable: true }),
    ownReportsOnly: new FormControl<string>("NO", { nonNullable: true }),
  });

  ownReportsOnlyItems = [
    { name: "All Reports", id: "NO" },
    { name: "Own Reports Only", id: "YES" }
  ];

  resetFilterForm() {
    this.filterForm.patchValue({
      name: '',
      reportGroupId: null,
      ownReportsOnly: "NO",
    });
  }

  add() {
    this.createReportDialog.open().subscribe((report) => {
      this.router.navigate(['reporting/report-item', report.id]);
    });
  }

  copy(item: Report) {
    this.copyReportDialog.open(item).subscribe((report) => {
      this.router.navigate(['reporting/report-item', report.id]);
    });
  }

  edit(item: Report) {
    this.router.navigate(['reporting/report-item', item.id]);
  }

  run(report: Report) {
    this.reportLauncherDialog.open({ reportId: report.id });
  }
}
