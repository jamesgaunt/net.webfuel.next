import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { Day } from '../../shared/form/date-calendar/Day';

export interface IDatePickerDialogOptions {
  title?: string;
  value?: string;
  callback?: (value: string | null) => void;
}

@Component({
  templateUrl: './date-picker-dialog.component.html'
})
export class DatePickerDialogComponent {

  constructor(
    private dialogRef: DialogRef<Day | null>,
    @Inject(DIALOG_DATA) public data: IDatePickerDialogOptions | undefined,
  ) {
  }

  pick() {
    this.dialogRef.close(null);
  }
}
