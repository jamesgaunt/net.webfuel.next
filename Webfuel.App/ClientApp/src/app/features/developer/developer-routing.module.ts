import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TenantListComponent } from './tenant/tenant-list/tenant-list.component';
import { TenantItemComponent, TenantResolver } from './tenant/tenant-item/tenant-item.component';
import { TenantDomainsComponent } from './tenant/tenant-domains/tenant-domains.component';

const routes: Routes = [
  {
    path: 'tenant-list',
    component: TenantListComponent
  },
  {
    path: 'tenant-item/:id',
    component: TenantItemComponent,
    resolve: { tenant: TenantResolver }
  },
  {
    path: 'tenant-domains/:id',
    component: TenantDomainsComponent,
    resolve: { tenant: TenantResolver }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DeveloperRoutingModule { }
