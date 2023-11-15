import { Component, Injectable } from '@angular/core';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';

export interface AlertDialogData {
  title: string;
  message: string;
}

@Injectable()
export class AlertDialog extends DialogBase<true, AlertDialogData> {
  open(data: AlertDialogData) {
    return this._open(AlertDialogComponent, data);
  }
}

@Component({
  selector: 'alert-dialog',
  templateUrl: './alert.dialog.html'
})
export class AlertDialogComponent extends DialogComponentBase<true, AlertDialogData> {

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
