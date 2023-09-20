import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UserListComponent } from './user/user-list/user-list.component';
import { UserItemComponent, UserResolver } from './user/user-item/user-item.component';

import { UserGroupListComponent } from './user-group/user-group-list/user-group-list.component';
import { UserGroupItemComponent, UserGroupResolver } from './user-group/user-group-item/user-group-item.component';


const routes: Routes = [
  {
    path: 'user-list',
    component: UserListComponent
  },
  {
    path: 'user-item/:id',
    component: UserItemComponent,
    resolve: { user: UserResolver }
  },
  {
    path: 'user-group-list',
    component: UserGroupListComponent
  },
  {
    path: 'user-group-item/:id',
    component: UserGroupItemComponent,
    resolve: { userGroup: UserGroupResolver }
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
