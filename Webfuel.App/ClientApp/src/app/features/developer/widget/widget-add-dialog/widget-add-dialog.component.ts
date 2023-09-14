import { DialogRef } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IWidget } from 'api/api.types';
import { WidgetApi } from 'api/widget.api';

@Component({
  templateUrl: './widget-add-dialog.component.html'
})
export class WidgetAddDialogComponent {

  constructor(
    private dialogRef: DialogRef<IWidget>,
    private widgetApi: WidgetApi,
  ) {
  }

  attempted = false;

  formData = new FormGroup({
    name: new FormControl('', { validators: [Validators.required] }),
    age: new FormControl<number | null>(null, { validators: [Validators.required] }),
  });

  save() {
    if (!this.formData.valid) {
      this.attempted = true;
      return;
    }

    this.widgetApi.insert({ widget: <any>this.formData.value }).subscribe((result) => {
      this.dialogRef.close();
    })
  }

  cancel() {
    this.dialogRef.close();
  }
}
