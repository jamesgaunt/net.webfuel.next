import { Component, Injectable } from '@angular/core';
import { DialogBase, DialogComponentBase } from '../../../shared/common/dialog-base';
import { ReportApi } from '../../../api/report.api';
import { Report, ReportDesign, ReportFilter, ReportFilterField, ReportFilterType, ReportFilterGroup, ReportArgument } from '../../../api/api.types';
import { ReportRunnerDialog } from './report-runner.dialog';
import { ReportService } from '../../report.service';
import _ from 'shared/common/underscore';
import { ReportDesignApi } from '../../../api/report-design.api';

export interface ReportLauncherDialogData {
  reportId: string;
}

@Injectable()
export class ReportLauncherDialog extends DialogBase<true, ReportLauncherDialogData> {
  open(data: ReportLauncherDialogData) {
    return this._open(ReportLauncherDialogComponent, data);
  }
}

@Component({
  selector: 'report-launcher-dialog',
  templateUrl: './report-launcher.dialog.html'
})
export class ReportLauncherDialogComponent extends DialogComponentBase<true, ReportLauncherDialogData> {

  constructor(
    private reportApi: ReportApi,
    private reportDesignApi: ReportDesignApi,
    private reportService: ReportService
  ) {
    super();
    this.reportApi.get({ id: this.data.reportId }).subscribe((report) => this.reset(report));
  }

  reset(report: Report) {
    this.report = report;
    this.title = report.name;
    this.reportDesignApi.generateArguments(this.report.design).subscribe((result) => {
      this.arguments = result;
      if (this.arguments.length == 0)
        this.run();
    });
  }

  report: Report | null = null; 

  title = "Initialising...";

  cancel() {
    this._cancelDialog();
  }

  run() {
    if (this.report == null)
      return;

    this.reportApi.run({ reportId: this.report.id, arguments: this.arguments }).subscribe((reportStep) => {
      this._cancelDialog();
      this.reportService.runReport(reportStep);
    });
  }

  arguments: ReportArgument[] | null = null;
}

