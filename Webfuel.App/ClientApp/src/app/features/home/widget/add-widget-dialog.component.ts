import { DialogRef } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IWidget } from '../../../api/api.types';
import { WidgetApi } from '../../../api/widget.api';

@Component({
  templateUrl: './add-widget-dialog.component.html'
})
export class AddWidgetDialogComponent {

  constructor(
    private dialogRef: DialogRef<IWidget>,
    private widgetApi: WidgetApi,
  ) {
  }

  formData = new FormGroup({
    name: new FormControl('', { nonNullable: true }),
    age: new FormControl(null),
  });

  save() {
    this.dialogRef.close();
  }

  cancel() {
    this.dialogRef.close();
  }
}
