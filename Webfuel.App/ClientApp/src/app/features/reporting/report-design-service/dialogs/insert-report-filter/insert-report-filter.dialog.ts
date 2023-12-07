import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { ReportSchema, ReportFilter, ReportDesign, ReportField, ReportFieldType, ReportFilterString, ReportFilterType, ReportFilterStringCondition, ReportFilterNumber, ReportFilterNumberCondition } from '../../../../../api/api.types';
import { StaticDataCache } from '../../../../../api/static-data.cache';
import _ from 'shared/common/underscore';
import { GrowlService } from '../../../../../core/growl.service';
import { ReportDesignApi } from '../../../../../api/report-design.api';

export interface InsertReportFilterDialogData {
  schema: ReportSchema;
  design: ReportDesign;
}

@Injectable()
export class InsertReportFilterDialog extends DialogBase<ReportDesign, InsertReportFilterDialogData> {
  open(data: InsertReportFilterDialogData) {
    return this._open(InsertReportFilterDialogComponent, data);
  }
}

@Component({
  selector: 'insert-report-filter-dialog',
  templateUrl: './insert-report-filter.dialog.html'
})
export class InsertReportFilterDialogComponent extends DialogComponentBase<ReportDesign, InsertReportFilterDialogData> {

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

    this.reportDesignApi.insertReportFilter(this.form.getRawValue()).subscribe((design) => this._closeDialog(design));
  }

  cancel() {
    this._cancelDialog();
  }
}
