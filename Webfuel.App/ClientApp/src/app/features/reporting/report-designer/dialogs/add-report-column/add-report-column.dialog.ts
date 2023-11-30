import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { IReportSchema, ReportColumn, ReportDesign } from '../../../../../api/api.types';
import { StaticDataCache } from '../../../../../api/static-data.cache';
import _ from 'shared/common/underscore';

export interface AddReportColumnDialogData {
  schema: IReportSchema;
  design: ReportDesign;
}

@Injectable()
export class AddReportColumnDialog extends DialogBase<true, AddReportColumnDialogData> {
  open(data: AddReportColumnDialogData) {
    return this._open(AddReportColumnDialogComponent, data);
  }
}

@Component({
  selector: 'add-report-column-dialog',
  templateUrl: './add-report-column.dialog.html'
})
export class AddReportColumnDialogComponent extends DialogComponentBase<true, AddReportColumnDialogData> {

  constructor(
    private formService: FormService,
    public staticDataCache: StaticDataCache,
  ) {
    super();
  }

  form = new FormGroup({
    id: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    var field = _.find(this.data.schema.fields, (p) => p.id == this.form.value.id);
    if (!field)
      return;

    this.data.design.columns.push({
      fieldId: field.id,
      title: field.name
    });
    this._closeDialog(true);
  }

  cancel() {
    this._cancelDialog();
  }
}
