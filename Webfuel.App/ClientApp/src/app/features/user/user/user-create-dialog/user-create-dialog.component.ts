import { DialogRef } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserApi } from 'api/user.api';
import { User, UserGroup } from '../../../../api/api.types';
import { UserGroupApi } from '../../../../api/user-group.api';
import { FormService } from '../../../../core/form.service';
import { SelectDataSource } from '../../../../shared/data-source/select-data-source';

@Component({
  selector: 'user-create-dialog-component',
  templateUrl: './user-create-dialog.component.html'
})
export class UserCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<User>,
    private formService: FormService,
    private userApi: UserApi,
    private userGroupApi: UserGroupApi
  ) {
  }

  userGroupDataSource = new SelectDataSource<UserGroup>({
    fetch: (query) => this.userGroupApi.queryUserGroup(query)
  })

  form = new FormGroup({
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    userGroupId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.userApi.createUser(this.form.getRawValue(), { successGrowl: "User Created" }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
