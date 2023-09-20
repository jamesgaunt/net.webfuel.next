import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { DeveloperRoutingModule } from './developer-routing.module';


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DeveloperRoutingModule
  ],
  declarations: [
  ]
})
export class DeveloperModule { }
