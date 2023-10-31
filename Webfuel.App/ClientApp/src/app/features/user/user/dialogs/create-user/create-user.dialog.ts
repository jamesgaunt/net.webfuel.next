import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserGroupApi } from 'api/user-group.api';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';

@Injectable()
export class CreateUserDialog extends DialogBase<User> {
  open() {
    return this._open(CreateUserDialogComponent, undefined);
  }
}

@Component({
  selector: 'create-user-dialog',
  templateUrl: './create-user.dialog.html'
})
export class CreateUserDialogComponent extends DialogComponentBase<User> {

  constructor(
    private formService: FormService,
    private userApi: UserApi,
    public userGroupApi: UserGroupApi,
    public staticDataCache: StaticDataCache
  ) {
    super();
  }

  form = new FormGroup({
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    title: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    firstName: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    lastName: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    userGroupId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.userApi.create(this.form.getRawValue(), { successGrowl: "User Created" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
