import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
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
import { AgeRangeComponent } from './types/age-range';
import { EthnicityComponent } from './types/ethnicity.component';
import { DisabilityComponent } from './types/disability.component';

const routes: Routes = [
  {
    path: 'title',
    component: TitleComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'funding-stream',
    component: FundingStreamComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'funding-body',
    component: FundingBodyComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'gender',
    component: GenderComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'research-methodology',
    component: ResearchMethodologyComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'project-status',
    component: ProjectStatusComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'application-stage',
    component: ApplicationStageComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'funding-call-type',
    component: FundingCallTypeComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'submission-stage',
    component: SubmissionStageComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'comms',
    component: HowDidYouFindUsComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'support-provided',
    component: SupportProvidedComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'work-activity',
    component: WorkActivityComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'user-discipline',
    component: UserDisciplineComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'age-range',
    component: AgeRangeComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'ethnicity',
    component: EthnicityComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'disability',
    component: DisabilityComponent,
    data: { activeSideMenu: 'Configuration' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StaticDataRoutingModule { }
