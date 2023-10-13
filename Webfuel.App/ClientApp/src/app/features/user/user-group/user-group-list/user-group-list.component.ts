import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserGroupApi } from 'api/user-group.api';
import { DialogService } from 'core/dialog.service';
import { UserGroup } from '../../../../api/api.types';
import { UserGroupCreateDialogComponent } from '../user-group-create-dialog/user-group-create-dialog.component';

@Component({
  selector: 'user-group-list',
  templateUrl: './user-group-list.component.html'
})
export class UserGroupListComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    public userGroupApi: UserGroupApi,
  ) {
  }

  add() {
    this.dialogService.open(UserGroupCreateDialogComponent, {
      callback: () => this.userGroupApi.userGroupDataSource.changed.emit()
    });
  }

  edit(item: UserGroup) {
    this.router.navigate(['user/user-group-item', item.id]);
  }

  delete(item: UserGroup) {
    this.dialogService.confirmDelete({
      title: item.name,
      confirmedCallback: () => {
        this.userGroupApi.deleteUserGroup({ id: item.id }, { successGrowl: "User Group Deleted" }).subscribe((result) => {
          this.userGroupApi.userGroupDataSource.changed.emit();
        })
      }
    });
  }
}
