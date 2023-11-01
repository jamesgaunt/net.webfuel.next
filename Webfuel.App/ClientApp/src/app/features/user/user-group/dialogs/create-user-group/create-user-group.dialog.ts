import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserGroupApi } from 'api/user-group.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { UserGroup } from '../../../../../api/api.types';

@Injectable()
export class CreateUserGroupDialog extends DialogBase<UserGroup> {
  open() {
    return this._open(CreateUserGroupDialogComponent, undefined);
  }
}

@Component({
  selector: 'create-user-group-dialog',
  templateUrl: './create-user-group.dialog.html'
})
export class CreateUserGroupDialogComponent extends DialogComponentBase<UserGroup> {

  constructor(
    private formService: FormService,
    private userGroupApi: UserGroupApi,
  ) {
    super();
  }

  form = new FormGroup({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.userGroupApi.create(this.form.getRawValue(), { successGrowl: "User Group Created" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
