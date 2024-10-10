import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { ReportApi } from 'api/report.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { Report } from 'api/api.types';
import { ReportGroupApi } from 'api/report-group.api';
import { StaticDataCache } from 'api/static-data.cache';

@Injectable()
export class CopyReportDialog extends DialogBase<Report, Report> {
  open(report: Report) {
    return this._open(CopyReportDialogComponent, report);
  }
}

@Component({
  selector: 'copy-report-dialog',
  templateUrl: './copy-report.dialog.html'
})
export class CopyReportDialogComponent extends DialogComponentBase<Report, Report> {

  constructor(
    private formService: FormService,
    private reportApi: ReportApi,
    public reportGroupApi: ReportGroupApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue({ id: this.data.id, name: this.data.name });
  }

  form = new FormGroup({
    id: new FormControl('', { nonNullable: true }),
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.reportApi.copy(this.form.getRawValue(), { successGrowl: "Report  Copied" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
