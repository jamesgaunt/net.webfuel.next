import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReportApi } from 'api/report.api';
import { ReportGroupApi } from 'api/report-group.api';
import { Report, ReportGroup } from '../../../api/api.types';
import { ReportLauncherDialog } from '../../../core/dialogs/report/report-launcher.dialog';
import { UserService } from '../../../core/user.service';

@Component({
  selector: 'report-index',
  templateUrl: './report-index.component.html',
  
})
export class ReportIndexComponent implements OnInit {
  constructor(
    private router: Router,
    public reportApi: ReportApi,
    public reportGroupApi: ReportGroupApi,
    public userService: UserService,
    private reportLauncherDialog: ReportLauncherDialog
  ) {
  }

  ngOnInit() { }

  launchReport(report: Report) {
    this.reportLauncherDialog.open({ reportId: report.id });
  }
}
