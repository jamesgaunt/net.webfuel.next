import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserApi } from 'api/user.api';
import { User, UserGroup } from 'api/api.types';
import { UserGroupApi } from 'api/user-group.api';
import { FormService } from 'core/form.service';
import { StaticDataCache } from 'api/static-data.cache';
import { DialogService } from 'core/dialog.service';

@Injectable()
export class UserCreateDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open() {
    return this.dialogService.openComponent<User>(UserCreateDialogComponent);
  }
}

@Component({
  selector: 'user-create-dialog-component',
  templateUrl: './user-create-dialog.component.html'
})
export class UserCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<User>,
    private formService: FormService,
    private userApi: UserApi,
    public userGroupApi: UserGroupApi,
    public staticDataCache: StaticDataCache
  ) {
  }

  form = new FormGroup({
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    title: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    firstName: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    lastName: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    userGroupId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.userApi.create(this.form.getRawValue(), { successGrowl: "User Created" }).subscribe((result) => {
      this.dialogRef.close(result);
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
