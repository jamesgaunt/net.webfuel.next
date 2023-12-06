import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { ReportSchema, ReportFilter, ReportDesign } from '../../../../../api/api.types';
import { StaticDataCache } from '../../../../../api/static-data.cache';
import _ from 'shared/common/underscore';

export interface AddReportFilterDialogData {
  schema: ReportSchema;
  design: ReportDesign;
  filters: ReportFilter[];
}

@Injectable()
export class AddReportFilterDialog extends DialogBase<string, AddReportFilterDialogData> {
  open(data: AddReportFilterDialogData) {
    return this._open(AddReportFilterDialogComponent, data);
  }
}

@Component({
  selector: 'add-report-filter-dialog',
  templateUrl: './add-report-filter.dialog.html'
})
export class AddReportFilterDialogComponent extends DialogComponentBase<string, AddReportFilterDialogData> {

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

    this._closeDialog(this.form.value.id!);
  }

  cancel() {
    this._cancelDialog();
  }
}
