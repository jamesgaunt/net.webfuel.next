import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { DialogService } from '../../../core/dialog.service';

export interface ConfirmDeleteDialogData {
  title: string;
}

@Injectable()
export class ConfirmDeleteDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open(data: ConfirmDeleteDialogData) {
    return this.dialogService.openComponent<true | undefined, ConfirmDeleteDialogData>(ConfirmDeleteDialogComponent, data);
  }
}

@Component({
  templateUrl: './confirm-delete-dialog.component.html'
})
export class ConfirmDeleteDialogComponent {

  constructor(
    private dialogRef: DialogRef<boolean>,
    @Inject(DIALOG_DATA) public data: ConfirmDeleteDialogData,
  ) {
  }

  delete() {
    this.dialogRef.close(true);
  }

  cancel() {
    this.dialogRef.close();
  }
}
