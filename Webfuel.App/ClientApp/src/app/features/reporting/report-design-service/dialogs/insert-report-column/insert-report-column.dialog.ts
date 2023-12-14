import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';
import { ReportDesign, ReportField, ReportSchema } from '../../../../../api/api.types';
import { ReportDesignApi } from '../../../../../api/report-design.api';

interface Field { id: string, name: string, selected: boolean }

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
    });

    _.forEach(this.data.schema.fields, (f) => {
      this.fields.push({
        id: f.id,
        name: f.name,
        selected: false,
      });
    });
  }

  form = new FormGroup({
    reportProviderId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    design: new FormControl<ReportDesign>(null!, { validators: [Validators.required], nonNullable: true }),
    fieldIds: new FormControl<string[]>([], { nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.form.patchValue({ fieldIds: _.map(_.filter(this.fields, p => p.selected), p => p.id) });
    this.reportDesignApi.insertReportColumn(this.form.getRawValue()).subscribe((design) => this._closeDialog(design));
  }

  cancel() {
    this._cancelDialog();
  }

  toggleField(field: Field) {
    field.selected = !field.selected;
  }

  selectAll() {
    _.forEach(this.fields, (field) => field.selected = true);
  }

  selectNone() {
    _.forEach(this.fields, (field) => field.selected = false);
  }

  fields: Field[] = [];
}
