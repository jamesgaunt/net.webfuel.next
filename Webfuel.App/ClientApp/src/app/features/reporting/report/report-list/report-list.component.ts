import { Component, DestroyRef, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ReportApi } from 'api/report.api';
import { ClientConfiguration, QueryReport, Report } from '../../../../api/api.types';
import { CreateReportDialog } from '../dialogs/create-report/create-report.dialog';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { ReportGroupApi } from '../../../../api/report-group.api';
import { StaticDataCache } from '../../../../api/static-data.cache';
import { CopyReportDialog } from '../dialogs/copy-report/copy-report.dialog';
import { UserService } from '../../../../core/user.service';
import { FormControl, FormGroup } from '@angular/forms';
import { ReportLauncherDialog } from '../../../../core/dialogs/report/report-launcher.dialog';
import { ConfigurationService } from '../../../../core/configuration.service';
import { BehaviorSubject, debounceTime } from 'rxjs';
import _ from 'shared/common/underscore'
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { DataSourceLookup } from '../../../../shared/common/data-source';
import { ReportProviderApi } from '../../../../api/report-provider.api';

@Component({
  selector: 'report-list',
  templateUrl: './report-list.component.html'
})
export class ReportListComponent {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private router: Router,
    private createReportDialog: CreateReportDialog,
    private copyReportDialog: CopyReportDialog,
    public reportApi: ReportApi,
    public reportGroupApi: ReportGroupApi,
    public userService: UserService,
    public staticDataCache: StaticDataCache,
    public reportLauncherDialog: ReportLauncherDialog,
    public configurationService: ConfigurationService,
  ) {
    this.configuration = configurationService.configuration;

    this.loadReports();

    this.filterForm.valueChanges.pipe(
      debounceTime(250),
      takeUntilDestroyed(this.destroyRef)
    )
    .subscribe(() => this.loadReports());
  }

  configuration: BehaviorSubject<ClientConfiguration | null>;

  canEditReport(report: Report) {
    if (this.configuration.value == null)
      return false;
    return report.ownerUserId == this.configuration.value.userId;
  }

  reports: Report[] = [];

  filterForm = new FormGroup({
    reportGroupId: new FormControl<string | null>(null),
    name: new FormControl<string>('', { nonNullable: true }),
    ownReportsOnly: new FormControl<string>("NO", { nonNullable: true }),
  });

  loadReports() {
    var query = _.merge({ skip: 0, take: 100, }, this.filterForm.getRawValue());
    this.reportApi.query(query).subscribe((result) => this.reports = _.sortBy(result.items, p => p.name));
  }

  reportGroupLookup = new DataSourceLookup(this.reportGroupApi);

  reportProviderLookup = new DataSourceLookup(this.staticDataCache.reportProvider);

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
