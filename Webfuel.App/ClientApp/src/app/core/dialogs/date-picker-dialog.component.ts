import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { Day } from '../../shared/form/date-calendar/Day';
import { FormControl } from '@angular/forms';

export interface IDatePickerDialogOptions {
  title?: string;
  value?: string | null;
  callback?: (value: string | null) => void;
}

@Component({
  templateUrl: './date-picker-dialog.component.html'
})
export class DatePickerDialogComponent {

  formControl = new FormControl<string | null>(null);

  constructor(
    private dialogRef: DialogRef<string | null>,
    @Inject(DIALOG_DATA) public data: IDatePickerDialogOptions | undefined,
  ) {
    this.formControl.setValue(data?.value || null);

    this.formControl.valueChanges.subscribe((value) => {
      this.dialogRef.close(value);
    });
  }
}
