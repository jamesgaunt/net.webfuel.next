import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UserListComponent } from './user/user-list/user-list.component';
import { UserItemComponent } from './user/user-item/user-item.component';

import { UserGroupListComponent } from './user-group/user-group-list/user-group-list.component';
import { UserGroupItemComponent } from './user-group/user-group-item/user-group-item.component';

import { UserGroupApi } from '../../api/user-group.api';
import { UserApi } from '../../api/user.api';
import { DeactivateService } from '../../core/deactivate.service';

const routes: Routes = [
  {
    path: 'user-list',
    component: UserListComponent
  },
  {
    path: 'user-item/:id',
    component: UserItemComponent,
    resolve: { user: UserApi.userResolver('id') },
    canDeactivate: [DeactivateService.isPristine<UserItemComponent>()]
  },
  {
    path: 'user-group-list',
    component: UserGroupListComponent
  },
  {
    path: 'user-group-item/:id',
    component: UserGroupItemComponent,
    resolve: { userGroup: UserGroupApi.userGroupResolver('id') },
    canDeactivate: [DeactivateService.isPristine<UserGroupItemComponent>()]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
