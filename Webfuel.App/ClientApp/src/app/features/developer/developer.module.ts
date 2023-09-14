import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { DeveloperRoutingModule } from './developer-routing.module';

import { WidgetListComponent } from './widget/widget-list/widget-list.component';
import { WidgetAddDialogComponent } from './widget/widget-add-dialog/widget-add-dialog.component';


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DeveloperRoutingModule
  ],
  declarations: [
    WidgetListComponent,
    WidgetAddDialogComponent
  ]
})
export class DeveloperModule { }
