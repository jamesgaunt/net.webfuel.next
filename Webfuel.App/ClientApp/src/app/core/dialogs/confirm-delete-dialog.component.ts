import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';

export interface IConfirmDeleteDialogOptions {
  title?: string;
  confirmedCallback?: () => void;
}

@Component({
  templateUrl: './confirm-delete-dialog.component.html'
})
export class ConfirmDeleteDialogComponent {

  constructor(
    private dialogRef: DialogRef<boolean>,
    @Inject(DIALOG_DATA) public data: IConfirmDeleteDialogOptions | undefined,
  ) {
  }

  delete() {
    this.dialogRef.close(true);
  }

  cancel() {
    this.dialogRef.close(false);
  }
}