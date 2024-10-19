import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';

import { HomeComponent } from './home/home.component';
import { MyActivityComponent } from './my-activity/my-activity.component';
import { ManageWidgetDialog, ManageWidgetDialogComponent } from './widget/management/manage-widget-dialog/manage-widget.dialog';

import { TeamActivityWidgetComponent } from './widget/rendering/team-activity/team-activity-widget.component';
import { MetricsSummaryWidgetComponent } from './widget/rendering/metrics-summary/metrics-summary-widget.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    HomeRoutingModule
  ],
  declarations: [
    HomeComponent,
    MyActivityComponent,
    ManageWidgetDialogComponent,

    // Widgets
    MetricsSummaryWidgetComponent,
    TeamActivityWidgetComponent,
  ],
  providers: [
    ManageWidgetDialog,
  ]
})
export class HomeModule { }
