import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IWidget } from 'api/api.types';
import { WidgetApi } from 'api/widget.api';

@Component({
  templateUrl: './widget-update-dialog.component.html'
})
export class WidgetUpdateDialogComponent {

  constructor(
    private dialogRef: DialogRef<IWidget>,
    private widgetApi: WidgetApi,
    @Inject(DIALOG_DATA) widget: IWidget,
  ) {
    this.formData.patchValue(widget);
  }

  formData = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    age: new FormControl<number>(null!, { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (!this.formData.valid) {
      this.formData.markAllAsTouched();
      return;
    }

    var id = this.formData.value.id;

    this.widgetApi.updateWidget(this.formData.getRawValue()).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
