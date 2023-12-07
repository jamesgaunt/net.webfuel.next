import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ReportColumn, ReportDesign, ReportSchema } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';
import { ReportDesignApi } from '../../../../../api/report-design.api';

export interface DeleteReportColumnDialogData {
  schema: ReportSchema;
  design: ReportDesign;
  column: ReportColumn;
}

@Injectable()
export class DeleteReportColumnDialog extends DialogBase<ReportDesign, DeleteReportColumnDialogData> {
  open(data: DeleteReportColumnDialogData) {
    return this._open(DeleteReportColumnDialogComponent, data, {
    });
  }
}

@Component({
  selector: 'delete-report-column-dialog',
  templateUrl: './delete-report-column.dialog.html'
})
export class DeleteReportColumnDialogComponent extends DialogComponentBase<ReportDesign, DeleteReportColumnDialogData> {

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
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.reportDesignApi.deleteReportColumn(this.form.getRawValue()).subscribe((design) => this._closeDialog(design));
  }

  cancel() {
    this._cancelDialog();
  }
}
