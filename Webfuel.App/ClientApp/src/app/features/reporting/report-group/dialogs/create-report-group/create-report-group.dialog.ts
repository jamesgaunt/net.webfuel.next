import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReportGroupApi } from 'api/report-group.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { ReportGroup } from '../../../../../api/api.types';

@Injectable()
export class CreateReportGroupDialog extends DialogBase<ReportGroup> {
  open() {
    return this._open(CreateReportGroupDialogComponent, undefined);
  }
}

@Component({
  selector: 'create-report-group-dialog',
  templateUrl: './create-report-group.dialog.html'
})
export class CreateReportGroupDialogComponent extends DialogComponentBase<ReportGroup> {

  constructor(
    private formService: FormService,
    private reportGroupApi: ReportGroupApi,
  ) {
    super();
  }

  form = new FormGroup({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.reportGroupApi.create(this.form.getRawValue(), { successGrowl: "Report Group Created" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
