import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReportApi } from '../api/report.api';
import { ReportDialog, ReportDialogData } from './dialogs/report/report.dialog';
import { ReportStep } from '../api/api.types';
import { DialogHandle } from './dialog.service';
import { ReportGeneratorApi } from '../api/report-generator.api';

@Injectable()
export class ReportService {

  constructor(
    private router: Router,
    private reportGeneratorApi: ReportGeneratorApi,
    private reportDialog: ReportDialog,
  ) { }
  
  dialogData: ReportDialogData | null = null;

  generateReport(reportStep: ReportStep) {

    if (this.dialogData != null)
      return;

    this.dialogData = {
      title: "Generate Report",
    };

    this.reportDialog.open(this.dialogData).subscribe({
      complete: () => {
        this.dialogData = null;
      }
    });

    this._generateReport(reportStep);
  }

  private _generateReport(reportStep: ReportStep) {
    if (!this.dialogData)
      return;

    this.dialogData.progressPercentage = reportStep.progressPercentage;

    this.reportGeneratorApi.generateReport({ taskId: reportStep.taskId }).subscribe((result) => {
      if (result.complete) {
        this._renderReport(result);
      } else {
        setTimeout(() => {
          this._generateReport(result);
        }, 250);
      }
    });
  }

  private _renderReport(reportStep: ReportStep) {
    if (!this.dialogData)
      return;
    this.dialogData.downloadUrl = "download-report/" + reportStep.taskId;
  }
}
