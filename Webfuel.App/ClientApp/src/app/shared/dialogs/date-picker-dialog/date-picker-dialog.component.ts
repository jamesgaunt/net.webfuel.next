import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Day } from '../../form/date-calendar/Day';
import { DialogService } from '../../../core/dialog.service';

export interface DatePickerDialogData {
  value?: string | null;
}

@Injectable()
export class DatePickerDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open(data: DatePickerDialogData) {
    return this.dialogService.openComponent<string | null, DatePickerDialogData>(DatePickerDialogComponent, data, {
      width: 'auto'
    });
  }
}

@Component({
  templateUrl: './date-picker-dialog.component.html'
})
export class DatePickerDialogComponent {

  formControl = new FormControl<string | null>(null);

  constructor(
    private dialogRef: DialogRef<string | null>,
    @Inject(DIALOG_DATA) public data: DatePickerDialogData,
  ) {
    this.formControl.setValue(data?.value || null);

    this.formControl.valueChanges.subscribe((value) => {
      this.dialogRef.close(value);
    });
  }
}
