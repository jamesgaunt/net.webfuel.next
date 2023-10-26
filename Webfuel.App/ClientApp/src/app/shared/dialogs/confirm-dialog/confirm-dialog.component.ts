import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { DialogService } from '../../../core/dialog.service';

export interface ConfirmDialogData {
  title: string;
  message: string;
}

@Injectable()
export class ConfirmDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open(data: ConfirmDialogData) {
    return this.dialogService.openComponent<true | undefined, ConfirmDialogData>(ConfirmDialogComponent, data);
  }
}

@Component({
  templateUrl: './confirm-dialog.component.html'
})
export class ConfirmDialogComponent {

  constructor(
    private dialogRef: DialogRef<boolean>,
    @Inject(DIALOG_DATA) public data: ConfirmDialogData,
  ) {
  }

  confirm() {
    this.dialogRef.close(true);
  }

  cancel() {
    this.dialogRef.close();
  }
}
