import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';

export interface IConfirmDialogOptions {
  title?: string;
  message?: string;
  callback?: (result: boolean) => void;
  confirmedCallback?: () => void;
}

@Component({
  templateUrl: './confirm-dialog.component.html'
})
export class ConfirmDialogComponent {

  constructor(
    private dialogRef: DialogRef<boolean>,
    @Inject(DIALOG_DATA) public data: IConfirmDialogOptions | undefined,
  ) {
  }

  confirm() {
    this.dialogRef.close(true);
  }

  cancel() {
    this.dialogRef.close(false);
  }
}
