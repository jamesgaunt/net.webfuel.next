import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IUserGroup } from 'api/api.types';
import { UserGroupApi } from 'api/user-group.api';
import { GrowlService } from '../../../../core/growl.service';
import { FormManager } from '../../../../shared/form/form-manager';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'user-group-create-dialog-component',
  templateUrl: './user-group-create-dialog.component.html'
})
export class UserGroupCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<IUserGroup>,
    private formService: FormService,
    private userGroupApi: UserGroupApi,
  ) {
  }

  formManager = this.formService.buildManager({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formManager.hasErrors())
      return;

    this.userGroupApi.createUserGroup(this.formManager.getRawValue(), { successGrowl: "User Group Created", errorHandler: this.formManager }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
