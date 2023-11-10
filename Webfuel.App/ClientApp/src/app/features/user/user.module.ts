import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { UserRoutingModule } from './user-routing.module';

import { UserListComponent } from './user/user-list/user-list.component';
import { UserItemComponent } from './user/user-item/user-item.component';
import { UserActivityComponent } from './user/user-activity/user-activity.component';
import { UserSupportTeamComponent } from './user/user-support-team/user-support-team.component';
import { UserTabsComponent } from './user/user-tabs/user-tabs.component';

import { UserGroupListComponent } from './user-group/user-group-list/user-group-list.component';
import { UserGroupItemComponent } from './user-group/user-group-item/user-group-item.component';
import { UserGroupClaimsComponent } from './user-group/user-group-claims/user-group-claims.component';
import { UserGroupTabsComponent } from './user-group/user-group-tabs/user-group-tabs.component';

import { CreateUserDialog, CreateUserDialogComponent } from './user/dialogs/create-user/create-user.dialog';
import { CreateUserGroupDialog, CreateUserGroupDialogComponent } from './user-group/dialogs/create-user-group/create-user-group.dialog';
import { UpdatePasswordDialog, UpdatePasswordDialogComponent } from './user/dialogs/update-password/update-password.dialog';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    UserRoutingModule
  ],
  declarations: [
    UserListComponent,
    UserItemComponent,
    UserActivityComponent,
    UserSupportTeamComponent,
    UserTabsComponent,

    UserGroupListComponent,
    UserGroupItemComponent,
    UserGroupClaimsComponent,
    UserGroupTabsComponent,

    CreateUserDialogComponent,
    CreateUserGroupDialogComponent,
    UpdatePasswordDialogComponent,
  ],
  providers: [
    CreateUserDialog,
    CreateUserGroupDialog,
    UpdatePasswordDialog,
  ]
})
export class UserModule { }
