import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { StaticDataRoutingModule } from './static-data-routing.module';

import { TitleComponent } from './types/title.component';
import { FundingStreamComponent } from './types/funding-stream.component';
import { FundingBodyComponent } from './types/funding-body.component';
import { GenderComponent } from './types/gender.component';
import { ResearchMethodologyComponent } from './types/research-methodology.component';
import { ProjectStatusComponent } from './types/project-status.component';
import { ApplicationStageComponent } from './types/application-stage.component';
import { FundingCallTypeComponent } from './types/funding-call-type.component';
import { SubmissionStageComponent } from './types/submission-stage.component';
import { HowDidYouFindUsComponent } from './types/how-did-you-find-us.component';
import { SupportProvidedComponent } from './types/support-provided.component';
import { WorkActivityComponent } from './types/work-activity.component';
import { UserDisciplineComponent } from './types/user-discipline.component';

import { CreateStaticDataDialog, CreateStaticDataDialogComponent } from './dialogs/create-static-data/create-static-data.dialog';
import { UpdateStaticDataDialog, UpdateStaticDataDialogComponent } from './dialogs/update-static-data/update-static-data.dialog';

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
    ProjectStatusComponent,
    ApplicationStageComponent,
    FundingCallTypeComponent,
    SubmissionStageComponent,
    HowDidYouFindUsComponent,
    SupportProvidedComponent,
    WorkActivityComponent,
    UserDisciplineComponent,

    CreateStaticDataDialogComponent,
    UpdateStaticDataDialogComponent,
  ],
  providers: [
    CreateStaticDataDialog,
    UpdateStaticDataDialog,
  ]
})
export class StaticDataModule { }
