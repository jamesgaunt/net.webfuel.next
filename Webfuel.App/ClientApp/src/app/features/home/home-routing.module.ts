import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { MyActivityComponent } from './my-activity/my-activity.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    data: { activeSideMenu: 'Home' }
  },
  {
    path: 'my-activity',
    component: MyActivityComponent,
    data: { activeSideMenu: 'My Activity' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
