import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { IWidget } from 'api/api.types';
import { WidgetApi } from 'api/widget.api';

@Component({
  templateUrl: './widget-dialog.component.html'
})
export class WidgetDialogComponent {

  constructor(
    private dialogRef: DialogRef<IWidget>,
    private widgetApi: WidgetApi,
    @Inject(DIALOG_DATA) widget: IWidget | undefined,
  ) {
    if (widget) {
      this.editing = true;
      this.formData.addControl("id", new UntypedFormControl(undefined));
      this.formData.setValue(widget);
    }
  }

  editing = false;

  validating = false;

  formData = new UntypedFormGroup({
    name: new UntypedFormControl('', { validators: [Validators.required] }),
    age: new UntypedFormControl(null, { validators: [Validators.required] }),
  });

  save() {
    if (!this.formData.valid) {
      this.validating = true;
      return;
    }

    if (this.editing) {
      this.widgetApi.update({ widget: this.formData.value }).subscribe((result) => {
        this.dialogRef.close();
      });
    } else {
      this.widgetApi.insert({ widget: this.formData.value }).subscribe((result) => {
        this.dialogRef.close();
      });
    }
  }

  cancel() {
    this.dialogRef.close();
  }
}
