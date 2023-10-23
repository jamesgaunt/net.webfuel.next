import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'core/dialog.service';
import { SupportRequest } from '../../../api/api.types';
import { SupportRequestApi } from '../../../api/support-request.api';
import { StaticDataCache } from '../../../api/static-data.cache';
import { SupportRequestCreateDialogComponent } from '../support-request-create-dialog/support-request-create-dialog.component';


@Component({
  selector: 'support-request-list',
  templateUrl: './support-request-list.component.html'
})
export class SupportRequestListComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    public supportRequestApi: SupportRequestApi,
    public staticDataCache: StaticDataCache
  ) {
  }

  add() {
    this.dialogService.open(SupportRequestCreateDialogComponent, {
    });
  }

  edit(item: SupportRequest) {
    this.router.navigate(['support-request/support-request-item', item.id]);
  }

  delete(item: SupportRequest) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.supportRequestApi.delete({ id: item.id }, { successGrowl: "Support Request Deleted" }).subscribe((result) => {
        })
      }
    });
  }
}
