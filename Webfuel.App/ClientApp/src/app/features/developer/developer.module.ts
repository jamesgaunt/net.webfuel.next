import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { DeveloperRoutingModule } from './developer-routing.module';

import { WidgetListComponent } from './widget/widget-list/widget-list.component';
import { WidgetDialogComponent } from './widget/widget-dialog/widget-dialog.component';


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    DeveloperRoutingModule
  ],
  declarations: [
    WidgetListComponent,
    WidgetDialogComponent
  ]
})
export class DeveloperModule { }
