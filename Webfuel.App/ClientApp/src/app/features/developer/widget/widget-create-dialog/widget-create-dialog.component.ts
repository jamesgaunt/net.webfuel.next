import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IWidget } from 'api/api.types';
import { WidgetApi } from 'api/widget.api';
import { GrowlService } from '../../../../core/growl.service';

@Component({
  selector: 'widget-create-dialog-component',
  templateUrl: './widget-create-dialog.component.html'
})
export class WidgetCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<IWidget>,
    private growlService: GrowlService,
    private widgetApi: WidgetApi,
  ) {
  }

  formData = new FormGroup({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    age: new FormControl<number>(null!, { validators: [Validators.required], nonNullable: true })
  });

  save() {
    if (!this.formData.valid) {
      this.formData.markAllAsTouched();
      this.growlService.growlDanger("Please complete all fields");
      return;
    }

    this.widgetApi.createWidget(this.formData.getRawValue(), { successGrowl: "Widget Created" }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
