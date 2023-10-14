import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { StaticDataRoutingModule } from './static-data-routing.module';

import { StaticDataCreateDialogComponent } from './dialogs/static-data-create-dialog/static-data-create-dialog.component';
import { StaticDataUpdateDialogComponent } from './dialogs/static-data-update-dialog/static-data-update-dialog.component';

import { TitleComponent } from './types/title.component';
import { FundingStreamComponent } from './types/funding-stream.component';
import { FundingBodyComponent } from './types/funding-body.component';
import { GenderComponent } from './types/gender.component';
import { ResearchMethodologyComponent } from './types/research-methodology.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    StaticDataRoutingModule
  ],
  declarations: [
    TitleComponent,
    FundingStreamComponent,
    FundingBodyComponent,
    GenderComponent,
    ResearchMethodologyComponent,

    StaticDataCreateDialogComponent,
    StaticDataUpdateDialogComponent,
  ]
})
export class StaticDataModule { }
