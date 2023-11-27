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

  dialogHandle: DialogHandle<true> | null = null;

  generateReport(reportProgress: ReportProgress) {

    if (this.dialogHandle != null)
      return;

    this.dialogData = {
      title: "Generate Report",
      message: "Generating Report..."
    };
  
    this.dialogHandle = this.reportDialog.open(this.dialogData);

    this.reportApi.generate({ taskId: reportProgress.taskId }).subscribe((result) => {


    });
  }

}
