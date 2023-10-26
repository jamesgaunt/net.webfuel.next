import { Component, Injectable } from '@angular/core';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';

export interface ConfirmDialogData {
  title: string;
  message: string;
}

@Injectable()
export class ConfirmDialog extends DialogBase<true, ConfirmDialogData> {
  open(data: ConfirmDialogData) {
    return this._open(ConfirmDialogComponent, data);
  }
}

@Component({
  selector: 'confirm-dialog',
  templateUrl: './confirm.dialog.html'
})
export class ConfirmDialogComponent extends DialogComponentBase<true, ConfirmDialogData> {

  constructor(
  ) {
    super();
  }

  confirm() {
    this._closeDialog(true);
  }

  cancel() {
    this._cancelDialog();
  }
}
