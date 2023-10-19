import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TitleComponent } from './types/title.component';
import { FundingStreamComponent } from './types/funding-stream.component';
import { FundingBodyComponent } from './types/funding-body.component';
import { GenderComponent } from './types/gender.component';
import { ResearchMethodologyComponent } from './types/research-methodology.component';
import { ProjectStatusComponent } from './types/project-status.component';

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
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StaticDataRoutingModule { }
