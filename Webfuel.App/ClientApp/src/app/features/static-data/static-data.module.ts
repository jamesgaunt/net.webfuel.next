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
import { ProjectStatusComponent } from './types/project-status.component';
import { ApplicationStageComponent } from './types/application-stage.component';
import { FundingCallTypeComponent } from './types/funding-call-type.component';
import { SubmissionStageComponent } from './types/submission-stage.component';
import { HowDidYouFindUsComponent } from './types/how-did-you-find-us.component';
import { SupportProvidedComponent } from './types/support-provided.component';
import { WorkActivityComponent } from './types/work-activity.component';
import { UserDisciplineComponent } from './types/user-discipline.component';

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

    StaticDataCreateDialogComponent,
    StaticDataUpdateDialogComponent,
  ]
})
export class StaticDataModule { }
