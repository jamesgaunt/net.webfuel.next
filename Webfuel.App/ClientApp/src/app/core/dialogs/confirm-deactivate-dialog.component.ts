import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';

export interface IConfirmDeactivateDialogOptions {
  title?: string;
  callback?: (result: boolean) => void;
}

@Component({
  templateUrl: './confirm-deactivate-dialog.component.html'
})
export class ConfirmDeactivateDialogComponent {

  constructor(
    private dialogRef: DialogRef<boolean>,
    @Inject(DIALOG_DATA) public data: IConfirmDeactivateDialogOptions | undefined,
  ) {
  }

  discard() {
    this.dialogRef.close(true);
  }

  cancel() {
    this.dialogRef.close(false);
  }
}
