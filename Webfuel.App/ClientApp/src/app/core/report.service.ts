import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReportApi } from '../api/report.api';
import { ReportDialog, ReportDialogData } from './dialogs/report/report.dialog';
import { ReportProgress } from '../api/api.types';
import { DialogHandle } from './dialog.service';

@Injectable()
export class ReportService {

  constructor(
    private router: Router,
    private reportApi: ReportApi,
    private reportDialog: ReportDialog,
  ) { }


  dialogData: ReportDialogData | null = null;

  generateReport(reportProgress: ReportProgress) {

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

    this._generateReport(reportProgress);
  }

  private _generateReport(reportProgress: ReportProgress) {
    if (!this.dialogData)
      return;

    this.dialogData.progressPercentage = reportProgress.progressPercentage;

    this.reportApi.generateReport({ taskId: reportProgress.taskId }).subscribe((result) => {
      if (result.complete) {
        this._generateResult(result);
      } else {
        setTimeout(() => {
          this._generateReport(result);
        }, 250);
      }
    });
  }

  private _generateResult(reportProgress: ReportProgress) {
    if (!this.dialogData)
      return;
    this.dialogData.downloadUrl = "download-report/" + reportProgress.taskId;
  }
}
