import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';

import { HomeComponent } from './home/home.component';
import { MyActivityComponent } from './my-activity/my-activity.component';
import { ManageWidgetDialog, ManageWidgetDialogComponent } from './widget/management/manage-widget-dialog/manage-widget.dialog';
import { AddWidgetDialog, AddWidgetDialogComponent } from './widget/management/add-widget-dialog/add-widget.dialog';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    HomeRoutingModule
  ],
  declarations: [
    HomeComponent,
    MyActivityComponent,

    // Widget Management

    ManageWidgetDialogComponent,
    AddWidgetDialogComponent,
  ],
  providers: [

    // Widget Management

    ManageWidgetDialog,
    AddWidgetDialog,
  ]
})
export class HomeModule { }
