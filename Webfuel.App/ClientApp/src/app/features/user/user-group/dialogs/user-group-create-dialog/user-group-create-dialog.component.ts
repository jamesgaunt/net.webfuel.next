import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserGroupApi } from 'api/user-group.api';
import { FormService } from 'core/form.service';
import { DialogService } from 'core/dialog.service';
import { UserGroup } from '../../../../../api/api.types';

@Injectable()
export class UserGroupCreateDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open() {
    return this.dialogService.openComponent<UserGroup>(UserGroupCreateDialogComponent);
  }
}

@Component({
  selector: 'user-group-create-dialog-component',
  templateUrl: './user-group-create-dialog.component.html'
})
export class UserGroupCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<UserGroup>,
    private formService: FormService,
    private userGroupApi: UserGroupApi,
  ) {
  }

  form = new FormGroup({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (!this.form.valid)
      return;

    this.userGroupApi.create(this.form.getRawValue(), { successGrowl: "User Group Created" }).subscribe((result) => {
      this.dialogRef.close(result);
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
