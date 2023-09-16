import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { DeveloperRoutingModule } from './developer-routing.module';

import { WidgetListComponent } from './widget/widget-list/widget-list.component';
import { WidgetCreateDialogComponent } from './widget/widget-create-dialog/widget-create-dialog.component';
import { WidgetUpdateDialogComponent } from './widget/widget-update-dialog/widget-update-dialog.component';


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DeveloperRoutingModule
  ],
  declarations: [
    WidgetListComponent,
    WidgetCreateDialogComponent,
    WidgetUpdateDialogComponent
  ]
})
export class DeveloperModule { }
