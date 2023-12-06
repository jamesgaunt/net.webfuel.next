import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { ReportSchema, ReportColumn, ReportDesign } from '../../../../../api/api.types';
import { StaticDataCache } from '../../../../../api/static-data.cache';
import _ from 'shared/common/underscore';
import { ReportDesignApi } from '../../../../../api/report-design.api';

export interface AddReportColumnDialogData {
  schema: ReportSchema;
  design: ReportDesign;
}

@Injectable()
export class AddReportColumnDialog extends DialogBase<ReportDesign, AddReportColumnDialogData> {
  open(data: AddReportColumnDialogData) {
    return this._open(AddReportColumnDialogComponent, data);
  }
}

@Component({
  selector: 'add-report-column-dialog',
  templateUrl: './add-report-column.dialog.html'
})
export class AddReportColumnDialogComponent extends DialogComponentBase<ReportDesign, AddReportColumnDialogData> {

  constructor(
    private formService: FormService,
    public reportDesignApi: ReportDesignApi
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

    var design = _.deepClone(this.data.design);
    design.columns.push({
      fieldId: field.id,
      title: field.name,
      width: null,
      format: ""
    });

    this.reportDesignApi.validateDesign({ reportProviderId: this.data.schema.reportProviderId, design: design }).subscribe((design) => {
      this._closeDialog(design);
    })
  }

  cancel() {
    this._cancelDialog();
  }
}
