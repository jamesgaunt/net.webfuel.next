import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TitleComponent } from './types/title.component';
import { FundingStreamComponent } from './types/funding-stream.component';


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
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StaticDataRoutingModule { }
