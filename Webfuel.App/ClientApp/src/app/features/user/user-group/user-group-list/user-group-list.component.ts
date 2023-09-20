import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IQueryUserGroupListView, IUserGroupListView } from 'api/api.types';
import { UserGroupApi } from 'api/user-group.api';
import { DialogService } from 'core/dialog.service';
import { DataSource } from '../../../../shared/data-source';
import { UserGroupCreateDialogComponent } from '../user-group-create-dialog/user-group-create-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'user-group-list',
  templateUrl: './user-group-list.component.html'
})
export class UserGroupListComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    private userGroupApi: UserGroupApi,
  ) {
  }

  filterForm = new FormGroup({
    search: new FormControl('')
  });

  dataSource = new DataSource<IUserGroupListView, IQueryUserGroupListView>({
    fetch: (query) => this.userGroupApi.queryUserGroupListView(query),
    filterGroup: this.filterForm
  });

  add() {
    this.dialogService.open(UserGroupCreateDialogComponent, {
      callback: () => this.dataSource.fetch()
    });
  }

  edit(item: IUserGroupListView) {
    this.router.navigate(['user/user-group-item', item.id]);
  }

  delete(item: IUserGroupListView) {
    this.dialogService.confirmDelete({
      title: item.name,
      confirmedCallback: () => {
        this.userGroupApi.deleteUserGroup({ id: item.id }, { successGrowl: "User Group Deleted" }).subscribe((result) => {
          this.dataSource.fetch();
        })
      }
    });
  }
}
