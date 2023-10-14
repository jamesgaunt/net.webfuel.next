import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserApi } from 'api/user.api';
import { DialogService } from 'core/dialog.service';
import { User } from '../../../../api/api.types';
import { UserGroupApi } from '../../../../api/user-group.api';
import { UserCreateDialogComponent } from '../user-create-dialog/user-create-dialog.component';

@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html'
})
export class UserListComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    public userApi: UserApi,
    public userGroupApi: UserGroupApi
  ) {
  }

  add() {
    this.dialogService.open(UserCreateDialogComponent, {
    });
  }

  edit(item: User) {
    this.router.navigate(['user/user-item', item.id]);
  }

  delete(item: User) {
    this.dialogService.confirmDelete({
      title: item.email,
      confirmedCallback: () => {
        this.userApi.delete({ id: item.id }, { successGrowl: "User Deleted" }).subscribe((result) => {
        })
      }
    });
  }
}
