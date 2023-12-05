import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReportApi } from 'api/report.api';
import { ReportGroupApi } from 'api/report-group.api';
import { Report, ReportGroup } from '../../../api/api.types';

@Component({
  selector: 'report-index',
  templateUrl: './report-index.component.html',
  
})
export class ReportIndexComponent implements OnInit {
  constructor(
    private router: Router,
    public reportApi: ReportApi,
    public reportGroupApi: ReportGroupApi
  ) {
  }

  ngOnInit(): void {
    this.reportApi.listHead().subscribe((result) => this.reports = result);
    this.reportGroupApi.query({ skip: 0, take: 1000 }).subscribe((result) => this.reportGroups = result.items);
  }

  reports: Report[] = [];

  reportGroups: ReportGroup[] = [];

  runReport(report: Report) {
    this.router.navigateByUrl("/report/report-runner/" + report.id);
  }
}
