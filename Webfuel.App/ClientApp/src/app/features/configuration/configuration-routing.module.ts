import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigurationMenuComponent } from './configuration-menu/configuration-menu.component';

const routes: Routes = [
  {
    path: 'configuration-menu',
    component: ConfigurationMenuComponent,
    data: { activeSideMenu: 'Configuration' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationRoutingModule { }
