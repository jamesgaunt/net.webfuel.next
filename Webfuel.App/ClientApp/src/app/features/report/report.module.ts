import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { ReportRoutingModule } from './report-routing.module';
import { ReportIndexComponent } from './report-index/report-index.component';
import { ReportRunnerComponent } from './report-runner/report-runner.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReportRoutingModule
  ],
  declarations: [
    ReportIndexComponent,
    ReportRunnerComponent,
  ],
  providers: [

  ]
})
export class ReportModule { }
