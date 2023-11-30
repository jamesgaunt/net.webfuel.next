import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ReportApi } from 'api/report.api';

@Component({
  selector: 'report-index',
  templateUrl: './report-index.component.html'
})
export class ReportIndexComponent {
  constructor(
    private router: Router,
    public reportApi: ReportApi,
  ) {
  }
}
