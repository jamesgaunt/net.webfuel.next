import { Component, Injectable } from '@angular/core';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';

export interface ConfirmDeleteDialogData {
  title: string;
}

@Injectable()
export class ConfirmDeleteDialog extends DialogBase<true, ConfirmDeleteDialogData> {
  open(data: ConfirmDeleteDialogData) {
    return this._open(ConfirmDeleteDialogComponent, data);
  }
}

@Component({
  selector: 'confirm-delete-dialog',
  templateUrl: './confirm-delete.dialog.html'
})
export class ConfirmDeleteDialogComponent extends DialogComponentBase<true, ConfirmDeleteDialogData> {

  constructor(
  ) {
    super();
  }

  delete() {
    this._closeDialog(true);
  }

  cancel() {
    this._cancelDialog();
  }
}
