import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReportColumn, ReportDesign, ReportSchema } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';

export interface EditReportColumnDialogData {
  schema: ReportSchema;
  design: ReportDesign;
  column: ReportColumn;
}

@Injectable()
export class EditReportColumnDialog extends DialogBase<true, EditReportColumnDialogData> {
  open(data: EditReportColumnDialogData) {
    return this._open(EditReportColumnDialogComponent, data, {
    });
  }
}

@Component({
  selector: 'edit-report-column-dialog',
  templateUrl: './edit-report-column.dialog.html'
})
export class EditReportColumnDialogComponent extends DialogComponentBase<true, EditReportColumnDialogData> {

  constructor(
    private formService: FormService,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue(this.data.column);
  }

  form = new FormGroup({
    title: new FormControl<string>('', { nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    var result = _.merge(_.deepClone(this.data.column), this.form.getRawValue())
    this.data.design.columns[this.data.design.columns.findIndex(p => p === this.data.column)] = result;
    this._closeDialog(true);
  }

  cancel() {
    this._cancelDialog();
  }
}
