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
      title: "Generate Report",
      reportStep: reportStep,
      progressPercentage: null
    };

    this.reportDialog.open(this.dialogData).subscribe({
      complete: () => {
        this.dialogData = null;
      }
    });

    this._runReport(reportStep);
  }

  private _runReport(reportStep: ReportStep) {
    if (!this.dialogData)
      return;

    this.dialogData.reportStep = reportStep;
    this.dialogData.progressPercentage = reportStep.stageTotal > 0 ? reportStep.stageCount * 100.0 / reportStep.stageTotal : 0; 

    this.reportGeneratorApi.generateReport({ taskId: reportStep.taskId }).subscribe((result) => {
      if (result.complete) {
        this._renderReport(result);
      } else {
        setTimeout(() => {
          this._runReport(result);
        }, 250);
      }
    });
  }

  private _renderReport(reportStep: ReportStep) {
    if (!this.dialogData)
      return;

    this.dialogData.progressPercentage = null;

    console.log("Report Metrics: ", reportStep.metrics);
    this.dialogData.downloadUrl = "download-report/" + reportStep.taskId;
  }
}
