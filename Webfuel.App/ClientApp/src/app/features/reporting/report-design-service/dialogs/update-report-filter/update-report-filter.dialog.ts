import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReportFilter, ReportDesign, ReportSchema } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';
import { ReportDesignApi } from '../../../../../api/report-design.api';

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
    });
    this.form.patchValue(this.data.filter);
  }

  form = new FormGroup({
    reportProviderId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    design: new FormControl<ReportDesign>(null!, { validators: [Validators.required], nonNullable: true }),
    id: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl<string>('', { nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    //this.reportDesignApi.updateReportFilter(this.form.getRawValue()).subscribe((design) => this._closeDialog(design));
  }

  cancel() {
    this._cancelDialog();
  }
}
