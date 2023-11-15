import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TestComponent } from './test/test.component';
import { UserLoginComponent } from './user-login/user-login.component';

const routes: Routes = [
  {
    path: 'test',
    component: TestComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'user-login',
    component: UserLoginComponent,
    data: { activeSideMenu: 'Configuration' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DeveloperRoutingModule {

}
