import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IQueryTenant, ITenant } from 'api/api.types';
import { TenantApi } from 'api/tenant.api';
import { DialogService } from 'core/dialog.service';
import { DataSource } from '../../../../shared/data-source';
import { TenantCreateDialogComponent } from '../tenant-create-dialog/tenant-create-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'tenant-list',
  templateUrl: './tenant-list.component.html'
})
export class TenantListComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    private tenantApi: TenantApi,
  ) {
  }

  filterForm = new FormGroup({
    search: new FormControl('')
  });

  dataSource = new DataSource<ITenant, IQueryTenant>({
    fetch: (query) => this.tenantApi.queryTenant(query),
    filterGroup: this.filterForm
  });

  add() {
    this.dialogService.open(TenantCreateDialogComponent, {
      callback: () => this.dataSource.fetch()
    });
  }

  edit(item: ITenant) {
    this.router.navigate(['developer/tenant-item', item.id]);
  }

  delete(item: ITenant) {
    this.dialogService.confirmDelete({
      title: item.name,
      confirmedCallback: () => {
        this.tenantApi.deleteTenant({ id: item.id }, { successGrowl: "Tenant Deleted" }).subscribe((result) => {
          this.dataSource.fetch();
        })
      }
    });
  }
}
