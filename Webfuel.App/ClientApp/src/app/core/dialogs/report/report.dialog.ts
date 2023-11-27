import { Component, Injectable } from '@angular/core';
import { DialogBase, DialogComponentBase } from '../../../shared/common/dialog-base';

export interface ReportDialogData {
  title: string;
  message: string;
}

@Injectable()
export class ReportDialog extends DialogBase<true, ReportDialogData> {
  open(data: ReportDialogData) {
    return this._open(ReportDialogComponent, data);
  }
}

@Component({
  selector: 'report-dialog',
  templateUrl: './report.dialog.html'
})
export class ReportDialogComponent extends DialogComponentBase<true, ReportDialogData> {

  constructor(
  ) {
    super();
  }

  report() {
    this._closeDialog(true);
  }

  cancel() {
    this._cancelDialog();
  }
}
