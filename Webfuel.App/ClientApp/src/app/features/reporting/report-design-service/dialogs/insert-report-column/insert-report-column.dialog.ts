import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';
import { ReportDesign, ReportSchema } from '../../../../../api/api.types';
import { ReportDesignApi } from '../../../../../api/report-design.api';

export interface InsertReportColumnDialogData {
  schema: ReportSchema;
  design: ReportDesign;
}

@Injectable()
export class InsertReportColumnDialog extends DialogBase<ReportDesign, InsertReportColumnDialogData> {
  open(data: InsertReportColumnDialogData) {
    return this._open(InsertReportColumnDialogComponent, data);
  }
}

@Component({
  selector: 'insert-report-column-dialog',
  templateUrl: './insert-report-column.dialog.html'
})
export class InsertReportColumnDialogComponent extends DialogComponentBase<ReportDesign, InsertReportColumnDialogData> {

  constructor(
    private formService: FormService,
    private reportDesignApi: ReportDesignApi,
  ) {
    super();

    this.form.patchValue({
      reportProviderId: this.data.schema.reportProviderId,
      design: this.data.design,
    })
  }

  form = new FormGroup({
    reportProviderId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    design: new FormControl<ReportDesign>(null!, { validators: [Validators.required], nonNullable: true }),
    fieldId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.reportDesignApi.insertReportColumn(this.form.getRawValue()).subscribe((design) => this._closeDialog(design));
  }

  cancel() {
    this._cancelDialog();
  }
}
