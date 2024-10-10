import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ReportApi } from 'api/report.api';
import { BehaviorSubject, debounceTime } from 'rxjs';
import { ClientConfiguration, Report } from 'api/api.types';
import _ from 'shared/common/underscore';
import { ReportGroupApi } from 'api/report-group.api';
import { StaticDataCache } from 'api/static-data.cache';
import { ConfigurationService } from 'core/configuration.service';
import { ReportLauncherDialog } from 'core/dialogs/report/report-launcher.dialog';
import { UserService } from 'core/user.service';
import { DataSourceLookup } from 'shared/common/data-source';
import { CopyReportDialog } from '../dialogs/copy-report/copy-report.dialog';
import { CreateReportDialog } from '../dialogs/create-report/create-report.dialog';

interface IReportGroup {
  name: string;
  reports: Report[];
}


@Component({
  selector: 'report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.scss']
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
  }

  configuration: BehaviorSubject<ClientConfiguration | null>;

  isDeveloper() {
    return this.configuration.value?.claims.developer === true;
  }

  canEditReport(report: Report) {
    if (this.configuration.value == null)
      return false;
    if (this.configuration.value.claims.developer)
      return true;
    return report.ownerUserId == this.configuration.value.userId;
  }

  get mode() {
    return ReportListComponent._mode;
  }
  set mode(value) {
    if (ReportListComponent._mode == value)
      return;
    ReportListComponent._mode = value;
    this.loadReports();
  }
  static _mode: "my-reports" | "public-reports" | "all-reports" = "my-reports"

  reportGroups: IReportGroup[] | null = null;

  loadReports() {
    var query = _.merge({ skip: 0, take: 100, }, {
      name: "",
      publicReports: this.mode == "public-reports",
      allReports: this.mode == "all-reports"
    });
    var result: IReportGroup[] = [];

    this.reportGroups = null;
    this.reportGroupApi.query({ skip: 0, take: 100 }).subscribe((_reportGroups) => {
      this.reportApi.query(query).subscribe((_reports) => {
        _.forEach(_reportGroups.items, (reportGroup) => {
          var reports = _.filter(_reports.items, (report) => report.reportGroupId == reportGroup.id)
          if (reports.length == 0)
            return;
          var group = {
            name: reportGroup.name,
            reports: reports
          };
          result.push(group);
        });
        this.reportGroups = result;
      });
    })
  }

  reportGroupLookup = new DataSourceLookup(this.reportGroupApi);

  reportProviderLookup = new DataSourceLookup(this.staticDataCache.reportProvider);

  ownReportsOnlyItems = [
    { name: "All Reports", id: "NO" },
    { name: "Own Reports Only", id: "YES" }
  ];

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

  info(report: Report) {
  }
}
