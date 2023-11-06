import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SupportRequest } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { SupportRequestApi } from 'api/support-request.api';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { FormControl, FormGroup } from '@angular/forms';

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

  filterForm = new FormGroup({
    number: new FormControl<string>('', { nonNullable: true }),
    fromDate: new FormControl<string | null>(null),
    toDate: new FormControl<string | null>(null),
    statusId: new FormControl<string | null>(null),
    title: new FormControl<string>('', { nonNullable: true })
  });

  resetFilterForm() {
    this.filterForm.patchValue({
      number: '',
      fromDate: null,
      toDate: null,
      statusId: null,
      title: ''
    })
  }

  add() {
    window.open("/external/support-request", "_blank");
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
