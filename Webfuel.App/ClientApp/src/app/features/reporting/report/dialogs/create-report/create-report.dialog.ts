import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { ReportApi } from 'api/report.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { Report } from '../../../../../api/api.types';
import { ReportGroupApi } from '../../../../../api/report-group.api';
import { StaticDataCache } from '../../../../../api/static-data.cache';

@Injectable()
export class CreateReportDialog extends DialogBase<Report> {
  open() {
    return this._open(CreateReportDialogComponent, undefined);
  }
}

@Component({
  selector: 'create-report-dialog',
  templateUrl: './create-report.dialog.html'
})
export class CreateReportDialogComponent extends DialogComponentBase<Report> {

  constructor(
    private formService: FormService,
    private reportApi: ReportApi,
    public reportGroupApi: ReportGroupApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
  }

  form = new FormGroup({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    reportGroupId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    reportProviderId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.reportApi.create(this.form.getRawValue(), { successGrowl: "Report  Created" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
