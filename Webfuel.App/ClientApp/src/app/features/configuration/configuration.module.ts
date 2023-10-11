import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ConfigurationRoutingModule } from './configuration-routing.module';

import { ConfigurationMenuComponent } from './configuration-menu/configuration-menu.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ConfigurationRoutingModule
  ],
  declarations: [
    ConfigurationMenuComponent
  ]
})
export class ConfigurationModule { }
