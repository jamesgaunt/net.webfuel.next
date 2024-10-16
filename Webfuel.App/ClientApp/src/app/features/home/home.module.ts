import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';

import { HomeComponent } from './home/home.component';
import { MyActivityComponent } from './my-activity/my-activity.component';
import { ManageWidgetDialog, ManageWidgetDialogComponent } from './widget/management/manage-widget-dialog/manage-widget.dialog';

import { ProjectSummaryWidgetComponent } from './widget/rendering/project-summary/project-summary-widget.component';
import { TeamSupportWidgetComponent } from './widget/rendering/team-support/team-support-widget.component';
import { TeamActivityWidgetComponent } from './widget/rendering/team-activity/team-activity-widget.component';

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
    ProjectSummaryWidgetComponent,
    TeamSupportWidgetComponent,
    TeamActivityWidgetComponent,
  ],
  providers: [
    ManageWidgetDialog,
  ]
})
export class HomeModule { }
