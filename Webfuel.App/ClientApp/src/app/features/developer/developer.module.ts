import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { DeveloperRoutingModule } from './developer-routing.module';

import { TenantListComponent } from './tenant/tenant-list/tenant-list.component';
import { TenantItemComponent } from './tenant/tenant-item/tenant-item.component';
import { TenantCreateDialogComponent } from './tenant/tenant-create-dialog/tenant-create-dialog.component';
import { TenantTabsComponent } from './tenant/tenant-tabs/tenant-tabs.component';
import { TenantDomainsComponent } from './tenant/tenant-domains/tenant-domains.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DeveloperRoutingModule
  ],
  declarations: [
    TenantListComponent,
    TenantItemComponent,
    TenantCreateDialogComponent,
    TenantTabsComponent,
    TenantDomainsComponent
  ]
})
export class DeveloperModule { }
