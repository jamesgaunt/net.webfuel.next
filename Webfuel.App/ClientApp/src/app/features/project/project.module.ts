import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ProjectRoutingModule } from './project-routing.module';

import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectItemComponent } from './project-item/project-item.component';
import { ProjectCreateDialogComponent } from './project-create-dialog/project-create-dialog.component';
import { ProjectTabsComponent } from './project-tabs/project-tabs.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ProjectRoutingModule
  ],
  declarations: [
    ProjectListComponent,
    ProjectItemComponent,
    ProjectCreateDialogComponent,
    ProjectTabsComponent,
  ]
})
export class ProjectModule { }
