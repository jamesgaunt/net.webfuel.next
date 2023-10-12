import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserApi } from 'api/user.api';
import { DialogService } from 'core/dialog.service';
import { GridDataSource } from '../../../../shared/data-source/grid-data-source';
import _ from '../../../../shared/underscore';
import { UserCreateDialogComponent } from '../user-create-dialog/user-create-dialog.component';
import { UserGroupApi } from '../../../../api/user-group.api';
import { LookupDataSource } from '../../../../shared/data-source/lookup-data-source';
import { User, UserGroup } from '../../../../api/api.types';
import { IDataSource } from '../../../../shared/data-source/data-source';

@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html'
})
export class UserListComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    private userApi: UserApi,
    private userGroupApi: UserGroupApi
  ) {
  }

  userDataSource: IDataSource<User> = {
    fetch: (query) => this.userApi.queryUser(query)
  }

  userGroupDataSource: IDataSource<UserGroup> = {
    fetch: (query) => this.userGroupApi.queryUserGroup(query)
  }

  filterForm = new FormGroup({
    search: new FormControl('', { nonNullable: true })
  });

  dataSource = new GridDataSource<User>({
    fetch: (query) => this.userApi.queryUser(_.merge(query, this.filterForm.getRawValue())),
    filterGroup: this.filterForm
  });

  userGroupLookup = new LookupDataSource<UserGroup>({
    fetch: (query) => this.userGroupApi.queryUserGroup(query)
  })

  add() {
    this.dialogService.open(UserCreateDialogComponent, {
      callback: () => this.dataSource.fetch()
    });
  }

  edit(item: User) {
    this.router.navigate(['user/user-item', item.id]);
  }

  delete(item: User) {
    this.dialogService.confirmDelete({
      title: item.email,
      confirmedCallback: () => {
        this.userApi.deleteUser({ id: item.id }, { successGrowl: "User Deleted" }).subscribe((result) => {
          this.dataSource.fetch();
        })
      }
    });
  }
}
