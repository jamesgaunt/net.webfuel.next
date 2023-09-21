import { DialogRef } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserGroupApi } from 'api/user-group.api';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'user-group-create-dialog-component',
  templateUrl: './user-group-create-dialog.component.html'
})
export class UserGroupCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<UserGroupApi>,
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

    this.userGroupApi.createUserGroup(this.form.getRawValue(), { successGrowl: "User Group Created" }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
