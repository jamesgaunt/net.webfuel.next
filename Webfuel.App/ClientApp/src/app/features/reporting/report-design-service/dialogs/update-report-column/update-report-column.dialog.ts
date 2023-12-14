import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReportColumn, ReportDesign, ReportSchema } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';
import { ReportDesignApi } from '../../../../../api/report-design.api';

export interface UpdateReportColumnDialogData {
  schema: ReportSchema;
  design: ReportDesign;
  column: ReportColumn;
}

@Injectable()
export class UpdateReportColumnDialog extends DialogBase<ReportDesign, UpdateReportColumnDialogData> {
  open(data: UpdateReportColumnDialogData) {
    return this._open(UpdateReportColumnDialogComponent, data, {
    });
  }
}

@Component({
  selector: 'update-report-column-dialog',
  templateUrl: './update-report-column.dialog.html'
})
export class UpdateReportColumnDialogComponent extends DialogComponentBase<ReportDesign, UpdateReportColumnDialogData> {

  constructor(
    private formService: FormService,
    private reportDesignApi: ReportDesignApi,
  ) {
    super();
    this.form.patchValue({
      reportProviderId: this.data.schema.reportProviderId,
      design: this.data.design,
    });
    this.form.patchValue(this.data.column);
  }

  form = new FormGroup({
    reportProviderId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    design: new FormControl<ReportDesign>(null!, { validators: [Validators.required], nonNullable: true }),
    id: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    title: new FormControl<string>('', { nonNullable: true }),
    bold: new FormControl<boolean>(false, { nonNullable: true }),
    width: new FormControl<number | null>(null)
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.reportDesignApi.updateReportColumn(this.form.getRawValue()).subscribe((design) => this._closeDialog(design));
  }

  cancel() {
    this._cancelDialog();
  }
}
