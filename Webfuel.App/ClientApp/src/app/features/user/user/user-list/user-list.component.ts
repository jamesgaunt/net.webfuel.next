import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserApi } from 'api/user.api';
import { User } from '../../../../api/api.types';
import { UserGroupApi } from '../../../../api/user-group.api';
import { CreateUserDialog } from '../dialogs/create-user/create-user.dialog';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';

@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html'
})
export class UserListComponent {
  constructor(
    private router: Router,
    private createUserDialog: CreateUserDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public userApi: UserApi,
    public userGroupApi: UserGroupApi
  ) {
  }

  add() {
    this.createUserDialog.open();
  }

  edit(item: User) {
    this.router.navigate(['user/user-item', item.id]);
  }

  delete(item: User) {
    this.confirmDeleteDialog.open({ title: "User" }).subscribe(() => {
      this.userApi.delete({ id: item.id }, { successGrowl: "User Deleted" }).subscribe();
    });
  }
}
