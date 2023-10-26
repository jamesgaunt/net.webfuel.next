import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserGroupApi } from 'api/user-group.api';
import { UserGroup } from '../../../../api/api.types';
import { CreateUserGroupDialog } from '../dialogs/create-user-group/create-user-group.dialog';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';

@Component({
  selector: 'user-group-list',
  templateUrl: './user-group-list.component.html'
})
export class UserGroupListComponent {
  constructor(
    private router: Router,
    private createUserGroupDialog: CreateUserGroupDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public userGroupApi: UserGroupApi,
  ) {
  }

  add() {
    this.createUserGroupDialog.open();
  }

  edit(item: UserGroup) {
    this.router.navigate(['user/user-group-item', item.id]);
  }

  delete(item: UserGroup) {
    this.confirmDeleteDialog.open({ title: "User Group" }).subscribe(() => {
      this.userGroupApi.delete({ id: item.id }, { successGrowl: "User Group Deleted" }).subscribe();
    });
  }
}
