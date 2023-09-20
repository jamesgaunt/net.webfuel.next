import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { UserRoutingModule } from './user-routing.module';

import { UserListComponent } from './user/user-list/user-list.component';
import { UserItemComponent } from './user/user-item/user-item.component';
import { UserCreateDialogComponent } from './user/user-create-dialog/user-create-dialog.component';
import { UserTabsComponent } from './user/user-tabs/user-tabs.component';

import { UserGroupListComponent } from './user-group/user-group-list/user-group-list.component';
import { UserGroupItemComponent } from './user-group/user-group-item/user-group-item.component';
import { UserGroupCreateDialogComponent } from './user-group/user-group-create-dialog/user-group-create-dialog.component';
import { UserGroupTabsComponent } from './user-group/user-group-tabs/user-group-tabs.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    UserRoutingModule
  ],
  declarations: [
    UserListComponent,
    UserItemComponent,
    UserCreateDialogComponent,
    UserTabsComponent,

    UserGroupListComponent,
    UserGroupItemComponent,
    UserGroupCreateDialogComponent,
    UserGroupTabsComponent,
  ]
})
export class UserModule { }
