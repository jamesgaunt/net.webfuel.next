import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SupportRequest } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { SupportRequestApi } from 'api/support-request.api';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';

@Component({
  selector: 'support-request-list',
  templateUrl: './support-request-list.component.html'
})
export class SupportRequestListComponent {
  constructor(
    private router: Router,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public supportRequestApi: SupportRequestApi,
    public staticDataCache: StaticDataCache
  ) {
  }

  add() {
    window.open("https://www.webfuel.com/test-support-request-form-icl", "_blank");
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
