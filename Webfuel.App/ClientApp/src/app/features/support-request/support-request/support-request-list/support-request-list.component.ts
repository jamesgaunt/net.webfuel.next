import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'core/dialog.service';
import { SupportRequest } from 'api/api.types';
import { SupportRequestApi } from 'api/support-request.api';
import { StaticDataCache } from 'api/static-data.cache';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { CreateSupportRequestDialog } from '../dialogs/create-support-request/create-support-request.dialog';

@Component({
  selector: 'support-request-list',
  templateUrl: './support-request-list.component.html'
})
export class SupportRequestListComponent {
  constructor(
    private router: Router,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    private createSupportRequestDialog: CreateSupportRequestDialog,
    public supportRequestApi: SupportRequestApi,
    public staticDataCache: StaticDataCache
  ) {
  }

  add() {
    this.createSupportRequestDialog.open();
  }

  edit(item: SupportRequest) {
    this.router.navigate(['support-request/support-request-item', item.id]);
  }

  delete(item: SupportRequest) {
    this.confirmDeleteDialog.open({ title: "Support Request" }).subscribe(() => {
      this.supportRequestApi.delete({ id: item.id }, { successGrowl: "Support Request Deleted" }).subscribe();
    });
  }
}
