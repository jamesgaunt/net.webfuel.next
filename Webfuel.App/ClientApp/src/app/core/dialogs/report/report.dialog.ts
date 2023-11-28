import { Component, Injectable } from '@angular/core';
import { DialogBase, DialogComponentBase } from '../../../shared/common/dialog-base';
import { environment } from '../../../../environments/environment';

export interface ReportDialogData {
  title: string;
  downloadUrl?: string;
  progressPercentage?: number;
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

  cancel() {
    this._cancelDialog();
  }

  download() {
    window.open(environment.apiHost + this.data.downloadUrl, "_blank");
    this._closeDialog(true);
  }
}
