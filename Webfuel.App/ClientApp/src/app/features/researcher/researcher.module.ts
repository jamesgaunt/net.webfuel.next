import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ResearcherRoutingModule } from './researcher-routing.module';

import { ResearcherListComponent } from './researcher/researcher-list/researcher-list.component';
import { ResearcherItemComponent } from './researcher/researcher-item/researcher-item.component';
import { ResearcherTabsComponent } from './researcher/researcher-tabs/researcher-tabs.component';

import { CreateResearcherDialog, CreateResearcherDialogComponent } from './researcher/dialogs/create-researcher/create-researcher.dialog';

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

    CreateResearcherDialogComponent,
  ],
  providers: [
    CreateResearcherDialog
  ]
})
export class ResearcherModule { }
