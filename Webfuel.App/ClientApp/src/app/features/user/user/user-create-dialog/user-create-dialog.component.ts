import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IUser } from 'api/api.types';
import { UserApi } from 'api/user.api';
import { GrowlService } from '../../../../core/growl.service';
import { FormManager } from '../../../../shared/form/form-manager';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'user-create-dialog-component',
  templateUrl: './user-create-dialog.component.html'
})
export class UserCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<IUser>,
    private formService: FormService,
    private userApi: UserApi,
  ) {
  }

  formManager = this.formService.buildManager({
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formManager.hasErrors())
      return;

    this.userApi.createUser(this.formManager.getRawValue(), { successGrowl: "User Created", errorHandler: this.formManager }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
