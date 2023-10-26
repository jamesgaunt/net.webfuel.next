import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { DialogService } from '../../../core/dialog.service';

@Injectable()
export class ConfirmDeactivateDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open() {
    return this.dialogService.openComponent<boolean>(ConfirmDeactivateDialogComponent);
  }
}

@Component({
  templateUrl: './confirm-deactivate-dialog.component.html'
})
export class ConfirmDeactivateDialogComponent {

  constructor(
    private dialogRef: DialogRef<boolean>,
  ) {
  }

  discard() {
    this.dialogRef.close(true);
  }

  cancel() {
    this.dialogRef.close(false);
  }
}
