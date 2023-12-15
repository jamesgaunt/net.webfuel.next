import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Heartbeat } from 'api/api.types';
import { HeartbeatApi } from 'api/heartbeat.api';
import { StaticDataCache } from 'api/static-data.cache';
import { ConfigurationService } from '../../../../core/configuration.service';
import { ReportService } from '../../../../core/report.service';
import { ConfirmDialog } from '../../../../shared/dialogs/confirm/confirm.dialog';

@Component({
  selector: 'heartbeat-list',
  templateUrl: './heartbeat-list.component.html'
})
export class HeartbeatListComponent {
  constructor(
    private router: Router,
    private confirmDialog: ConfirmDialog,
    private confirmDeleteDialog: ConfirmDialog,
    public heartbeatApi: HeartbeatApi,
    public staticDataCache: StaticDataCache,
  ) {
  }

  add() {
    this.confirmDialog.open({ message: "Are you sure you want to add a new heartbeat?", title: "Add Heartbeat" }).subscribe(() => {
      this.heartbeatApi.create({ name: "New Heartbeat" }, { successGrowl: "Heartbeat Added" }).subscribe((result) => {
        this.edit(result)
      });
    });
  }

  edit(item: Heartbeat) {
    this.router.navigateByUrl("/developer/heartbeat-item/" + item.id);
  }

  delete(item: Heartbeat) {
    this.confirmDeleteDialog.open({ message: "Are you sure you want to delete this heartbeat?", title: "Heartbeat" }).subscribe(() => {
      this.heartbeatApi.delete({ id: item.id }, { successGrowl: "Heartbeat Deleted" }).subscribe(() => {
      });
    });
  }

  execute(item: Heartbeat) {
    this.heartbeatApi.execute({ id: item.id }).subscribe(() => {
      this.heartbeatApi.changed.emit();
    })
  }

  schedule(item: Heartbeat) {
    var result = item.schedule;
    if (item.minTime && item.minTime)
      result += " (" + item.minTime + " - " + item.maxTime + ")";
    return result;
  }

  healthPercentage(item: Heartbeat) {
    var total = item.recentExecutionFailureCount + item.recentExecutionSuccessCount;
    if (total <= 0)
      return "---";
    return ((item.recentExecutionSuccessCount * 100) / total).toFixed(2) + "%";
  }

}
