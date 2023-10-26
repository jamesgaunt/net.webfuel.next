import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ResearcherRoutingModule } from './researcher-routing.module';

import { ResearcherListComponent } from './researcher/researcher-list/researcher-list.component';
import { ResearcherItemComponent } from './researcher/researcher-item/researcher-item.component';
import { ResearcherTabsComponent } from './researcher/researcher-tabs/researcher-tabs.component';

import { ResearcherCreateDialogComponent, ResearcherCreateDialogService } from './researcher/dialogs/researcher-create-dialog/researcher-create-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ResearcherRoutingModule
  ],
  declarations: [
    ResearcherListComponent,
    ResearcherItemComponent,
    ResearcherTabsComponent,

    ResearcherCreateDialogComponent,
  ],
  providers: [
    ResearcherCreateDialogService
  ]
})
export class ResearcherModule { }
