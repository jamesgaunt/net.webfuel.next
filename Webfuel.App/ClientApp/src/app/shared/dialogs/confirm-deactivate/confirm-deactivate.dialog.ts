import { Component, Injectable } from '@angular/core';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';

@Injectable()
export class ConfirmDeactivateDialog extends DialogBase<boolean> {
  open() {
    return this._open(ConfirmDeactivateDialogComponent, undefined);
  }
}

@Component({
  selector: 'confirm-deactivate-dialog',
  templateUrl: './confirm-deactivate.dialog.html'
})
export class ConfirmDeactivateDialogComponent extends DialogComponentBase<boolean> {

  constructor(
  ) {
    super();
  }

  discard() {
    this.close(true);
  }

  cancel() {
    this.close(false);
  }
}
