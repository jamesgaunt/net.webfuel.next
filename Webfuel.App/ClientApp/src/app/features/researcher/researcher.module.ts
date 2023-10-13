import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ResearcherRoutingModule } from './researcher-routing.module';

import { ResearcherListComponent } from './researcher/researcher-list/researcher-list.component';
import { ResearcherItemComponent } from './researcher/researcher-item/researcher-item.component';
import { ResearcherCreateDialogComponent } from './researcher/researcher-create-dialog/researcher-create-dialog.component';
import { ResearcherTabsComponent } from './researcher/researcher-tabs/researcher-tabs.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ResearcherRoutingModule
  ],
  declarations: [
    ResearcherListComponent,
    ResearcherItemComponent,
    ResearcherCreateDialogComponent,
    ResearcherTabsComponent,
  ]
})
export class ResearcherModule { }
