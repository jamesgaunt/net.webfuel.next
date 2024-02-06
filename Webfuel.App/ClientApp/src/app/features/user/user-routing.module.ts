import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UserListComponent } from './user/user-list/user-list.component';
import { UserItemComponent } from './user/user-item/user-item.component';
import { UserActivityComponent } from './user/user-activity/user-activity.component';

import { UserGroupListComponent } from './user-group/user-group-list/user-group-list.component';
import { UserGroupItemComponent } from './user-group/user-group-item/user-group-item.component';

import { UserGroupApi } from '../../api/user-group.api';
import { UserApi } from '../../api/user.api';
import { DeactivateService } from '../../core/deactivate.service';
import { ConfigurationService } from '../../core/configuration.service';
import { UserGroupClaimsComponent } from './user-group/user-group-claims/user-group-claims.component';
import { UserSupportTeamComponent } from './user/user-support-team/user-support-team.component';


const routes: Routes = [
  {
    path: 'user-list',
    component: UserListComponent,
    data: { activeSideMenu: 'Users' }
  },
  {
    path: 'user-item/:id',
    component: UserItemComponent,
    resolve: { user: UserApi.userResolver('id') },
    canDeactivate: [DeactivateService.isPristine<UserItemComponent>()],
    data: { activeSideMenu: 'Users' }
  },
  {
    path: 'user-support-team/:id',
    component: UserSupportTeamComponent,
    resolve: { user: UserApi.userResolver('id') },
    canDeactivate: [DeactivateService.isPristine<UserSupportTeamComponent>()],
    data: { activeSideMenu: 'Users' }
  },
  {
    path: 'user-activity/:id',
    component: UserActivityComponent,
    resolve: { user: UserApi.userResolver('id') },
    data: { activeSideMenu: 'Users' }
  },
  {
    path: 'user-group-list',
    component: UserGroupListComponent,
    data: { activeSideMenu: 'User Groups' }
  },
  {
    path: 'user-group-item/:id',
    component: UserGroupItemComponent,
    resolve: { userGroup: UserGroupApi.userGroupResolver('id') },
    canDeactivate: [DeactivateService.isPristine<UserGroupItemComponent>()],
    data: { activeSideMenu: 'User Groups' }
  },
  {
    path: 'user-group-claims/:id',
    component: UserGroupClaimsComponent,
    resolve: { userGroup: UserGroupApi.userGroupResolver('id') },
    canDeactivate: [DeactivateService.isPristine<UserGroupClaimsComponent>()],
    data: { activeSideMenu: 'User Groups' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
