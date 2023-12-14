import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportApi } from 'api/report.api';
import { ReportGroupApi } from 'api/report-group.api';
import { Report, ReportGroup } from '../../../api/api.types';
import { ReportService } from '../../../core/report.service';

@Component({
  selector: 'report-runner',
  templateUrl: './report-runner.component.html',
  
})
export class ReportRunnerComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private reportService: ReportService,
    public reportApi: ReportApi,
    public reportGroupApi: ReportGroupApi
  ) {
  }

  ngOnInit(): void {
    this.report = this.route.snapshot.data.report;
  }

  report!: Report;

  runReport() {
    this.reportApi.run({ reportId: this.report.id, arguments: null }).subscribe((reportStep) => {
      this.reportService.runReport(reportStep);
    });
  }

  cancel() {
    this.router.navigateByUrl("/report/report-index");
  }
}
