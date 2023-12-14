import { Component, Injectable } from '@angular/core';
import { DialogBase, DialogComponentBase } from '../../../shared/common/dialog-base';
import { environment } from '../../../../environments/environment';
import { ReportStep } from '../../../api/api.types';

export interface ReportRunnerDialogData {
  title: string;
  reportStep: ReportStep;
  downloadUrl?: string;
  progressPercentage: number | null;
}

@Injectable()
export class ReportRunnerDialog extends DialogBase<true, ReportRunnerDialogData> {
  open(data: ReportRunnerDialogData) {
    return this._open(ReportRunnerDialogComponent, data);
  }
}

@Component({
  selector: 'report-runner-dialog',
  templateUrl: './report-runner.dialog.html'
})
export class ReportRunnerDialogComponent extends DialogComponentBase<true, ReportRunnerDialogData> {

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
