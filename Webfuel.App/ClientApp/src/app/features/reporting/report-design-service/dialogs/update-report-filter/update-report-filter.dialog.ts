import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReportFilter, ReportDesign, ReportSchema } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';
import { ReportDesignApi } from '../../../../../api/report-design.api';
import { ReportFilterType } from '../../../../../api/api.enums';

export interface UpdateReportFilterDialogData {
  schema: ReportSchema;
  design: ReportDesign;
  filter: ReportFilter;
}

@Injectable()
export class UpdateReportFilterDialog extends DialogBase<ReportDesign, UpdateReportFilterDialogData> {
  open(data: UpdateReportFilterDialogData) {
    return this._open(UpdateReportFilterDialogComponent, data, {
    });
  }
}

@Component({
  selector: 'update-report-filter-dialog',
  templateUrl: './update-report-filter.dialog.html'
})
export class UpdateReportFilterDialogComponent extends DialogComponentBase<ReportDesign, UpdateReportFilterDialogData> {

  constructor(
    private formService: FormService,
    private reportDesignApi: ReportDesignApi,
  ) {
    super();
    this.form.patchValue({
      reportProviderId: this.data.schema.reportProviderId,
      design: this.data.design,
      filter: this.data.filter,
    });

    if ((<any>this.data.filter).fieldName)
      this.fieldName = (<any>this.data.filter).fieldName;
  }

  ReportFilterType = ReportFilterType;

  form = new FormGroup({
    reportProviderId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    design: new FormControl<ReportDesign>(null!, { validators: [Validators.required], nonNullable: true }),
    filter: new FormControl<ReportFilter>(null!, { validators: [Validators.required], nonNullable: true }),
  });

  fieldName: string | null = null;

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.reportDesignApi.updateReportFilter(this.form.getRawValue()).subscribe((design) => this._closeDialog(design));
  }

  cancel() {
    this._cancelDialog();
  }
}
