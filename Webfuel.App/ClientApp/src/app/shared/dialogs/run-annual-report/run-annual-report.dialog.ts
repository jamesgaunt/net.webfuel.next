import { Component, Injectable } from '@angular/core';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';
import { FormControl, FormGroup } from '@angular/forms';
import { ReportService } from '../../../core/report.service';
import { ReportApi } from '../../../api/report.api';

export interface RunAnnualReportData {
}

@Injectable()
export class RunAnnualReportDialog extends DialogBase<true, RunAnnualReportData> {
  open(data: RunAnnualReportData) {
    return this._open(RunAnnualReportDialogComponent, data);
  }
}

@Component({
  selector: 'run-annual-report-dialog',
  templateUrl: './run-annual-report.dialog.html'
})
export class RunAnnualReportDialogComponent extends DialogComponentBase<true, RunAnnualReportData> {

  constructor(
    private reportApi: ReportApi,
    private reportService: ReportService,
  ) {
    super();
  }

  form = new FormGroup({
    fromDate: new FormControl<string | null>(null),
    toDate: new FormControl<string | null>(null),
    highlightInvalidCells: new FormControl<boolean>(false, { nonNullable: true }),
  });

  confirm() {
    this.reportApi.runAnnualReport(this.form.getRawValue()).subscribe((result) => {
      this._closeDialog(true);
      this.reportService.runReport(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
