import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IQueryUserListView, IUserListView } from 'api/api.types';
import { UserApi } from 'api/user.api';
import { DialogService } from 'core/dialog.service';
import { DataSource } from '../../../../shared/data-source';
import { UserCreateDialogComponent } from '../user-create-dialog/user-create-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html'
})
export class UserListComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    private userApi: UserApi,
  ) {
  }

  filterForm = new FormGroup({
    search: new FormControl('')
  });

  dataSource = new DataSource<IUserListView, IQueryUserListView>({
    fetch: (query) => this.userApi.queryUserListView(query),
    filterGroup: this.filterForm
  });

  add() {
    this.dialogService.open(UserCreateDialogComponent, {
      callback: () => this.dataSource.fetch()
    });
  }

  edit(item: IUserListView) {
    this.router.navigate(['user/user-item', item.id]);
  }

  delete(item: IUserListView) {
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
