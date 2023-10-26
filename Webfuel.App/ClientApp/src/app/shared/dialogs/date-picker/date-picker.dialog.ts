import { Component, Injectable } from '@angular/core';
import { FormControl } from '@angular/forms';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';

export interface DatePickerDialogData {
  value?: string | null;
}

@Injectable()
export class DatePickerDialog extends DialogBase<string | null, DatePickerDialogData> {
  open(data: DatePickerDialogData) {
    return this._open(DatePickerDialogComponent, data);
  }
}

@Component({
  selector: 'date-picker-dialog',
  templateUrl: './date-picker.dialog.html'
})
export class DatePickerDialogComponent extends DialogComponentBase<string | null, DatePickerDialogData>  {

  formControl = new FormControl<string | null>(null);

  constructor(
  ) {
    super();
    this.formControl.setValue(this.data.value || null);
    this.formControl.valueChanges.subscribe((value) => {
      this.close(value);
    });
  }
}
