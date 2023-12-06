import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { ReportSchema, ReportFilter, ReportDesign, ReportField, ReportFieldType, ReportFilterString, ReportFilterType, ReportFilterStringCondition, ReportFilterNumber, ReportFilterNumberCondition } from '../../../../../api/api.types';
import { StaticDataCache } from '../../../../../api/static-data.cache';
import _ from 'shared/common/underscore';
import { GrowlService } from '../../../../../core/growl.service';
import { ReportDesignApi } from '../../../../../api/report-design.api';

export interface AddReportFilterDialogData {
  schema: ReportSchema;
  design: ReportDesign;
  filters: ReportFilter[];
}

@Injectable()
export class AddReportFilterDialog extends DialogBase<ReportDesign, AddReportFilterDialogData> {
  open(data: AddReportFilterDialogData) {
    return this._open(AddReportFilterDialogComponent, data);
  }
}

@Component({
  selector: 'add-report-filter-dialog',
  templateUrl: './add-report-filter.dialog.html'
})
export class AddReportFilterDialogComponent extends DialogComponentBase<ReportDesign, AddReportFilterDialogData> {

  constructor(
    private formService: FormService,
    private reportDesignApi: ReportDesignApi,
    private growlService: GrowlService
  ) {
    super();
  }

  form = new FormGroup({
    id: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    var field = this._getField(this.form.value.id!);
    if (!field)
      return;

    var filter = this._initialiseFilterForField(field);
    if (!filter)
      return;

    this.data.filters.push(filter);

    this.reportDesignApi.validateDesign({ reportProviderId: this.data.schema.reportProviderId, design: this.data.design }).subscribe((design) => {
      this._closeDialog(design);
    });
  }

  cancel() {
    this._cancelDialog();
  }

  // Helpers

  private _getField(fieldId: string) {
    var field = this.data.schema.fields.find(p => p.id == fieldId);
    if (!field)
      this.growlService.growlDanger("Field not found: " + fieldId);
    return field;
  }

  private _initialiseFilterForField(field: ReportField): ReportFilter | undefined {
    switch (field.fieldType) {

      case ReportFieldType.String:
        return <ReportFilterString>{
          filterType: ReportFilterType.String,
          fieldId: field.id,
          condition: ReportFilterStringCondition.Contains,
        };

      case ReportFieldType.Number:
        return <ReportFilterNumber>{
          filterType: ReportFilterType.Number,
          fieldId: field.id,
          condition: ReportFilterNumberCondition.EqualTo,
        }

      default:
        this.growlService.growlDanger("Unrecognised field type: " + field.fieldType);
        return undefined;
    }
  }

}
