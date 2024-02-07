import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReportApi } from '../api/report.api';
import { ReportRunnerDialog, ReportRunnerDialogData } from './dialogs/report/report-runner.dialog';
import { ReportStep } from '../api/api.types';
import { DialogHandle } from './dialog.service';
import { ReportGeneratorApi } from '../api/report-generator.api';

@Injectable()
export class ReportService {

  constructor(
    private router: Router,
    private reportGeneratorApi: ReportGeneratorApi,
    private reportDialog: ReportRunnerDialog,
  ) { }
  
  dialogData: ReportRunnerDialogData | null = null;

  runReport(reportStep: ReportStep) {

    if (this.dialogData != null)
      return;

    this.dialogData = {
      title: "Running Report",
      reportStep: reportStep,
      stageIndex: 0,
      progressPercentage: null
    };

    this.reportDialog.open(this.dialogData).subscribe({
      complete: () => {
        this.dialogData = null;
      }
    });

    this._runReport(reportStep);
  }

  private setProgress(count: number, total: number) {
    var pct = total > 0 ? count * 100.0 / total : 0;
    this.dialogData!.progressPercentage = pct;
  }

  private _runReport(reportStep: ReportStep) {
    if (!this.dialogData)
      return;

    this.dialogData.reportStep = reportStep;
    this.setProgress(reportStep.stageCount, reportStep.stageTotal);

    this.reportGeneratorApi.generateReport({ taskId: reportStep.taskId }).subscribe((result) => {
      if (result.complete) {
        this.dialogData!.progressPercentage = 100;
        setTimeout(() => {
          this._renderReport(result);
        }, 1000);
      } else {
        if (reportStep.stage == result.stage) {
          setTimeout(() => {
            this._runReport(result);
          }, 250);
        } else {
          this.setProgress(100, 100);
          setTimeout(() => {
            this.dialogData!.reportStep.stage = result.stage;
            this.dialogData!.progressPercentage = null;
            this.dialogData!.stageIndex++;
            setTimeout(() => {
              this._runReport(result);
            }, 1000);
          }, 1000);
        }
      }
    });
  }

  private _renderReport(reportStep: ReportStep) {
    if (!this.dialogData)
      return;

    console.log("Report Metrics: ", reportStep.metrics);
    this.dialogData.downloadUrl = "download-report/" + reportStep.taskId;
  }
}
