import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserGroupApi } from 'api/user-group.api';
import { DialogService } from 'core/dialog.service';
import { GridDataSource } from '../../../../shared/data-source/grid-data-source';
import { UserGroupCreateDialogComponent } from '../user-group-create-dialog/user-group-create-dialog.component';
import { Router } from '@angular/router';
import _ from '../../../../shared/underscore';
import { UserGroup } from '../../../../api/api.types';
import { BehaviorSubject } from 'rxjs';

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
    search: new FormControl('', { nonNullable: true }),
  });

  dataSource = new GridDataSource<UserGroup>({
    fetch: (query) => this.userGroupApi.queryUserGroup(_.merge(query, this.filterForm.getRawValue())),
    filterGroup: this.filterForm
  });

  add() {
    this.dialogService.open(UserGroupCreateDialogComponent, {
      callback: () => this.dataSource.fetch()
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
          this.dataSource.fetch();
        })
      }
    });
  }
}
