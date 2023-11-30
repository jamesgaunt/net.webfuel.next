import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { ReportRoutingModule } from './report-routing.module';
import { ReportIndexComponent } from './report-index/report-index.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReportRoutingModule
  ],
  declarations: [
    ReportIndexComponent
  ],
  providers: [

  ]
})
export class ReportModule { }
