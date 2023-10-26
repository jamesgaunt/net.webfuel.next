import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { GrowlService } from 'core/growl.service';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';

@Injectable()
export class ChangePasswordDialog extends DialogBase<true> {
  open() {
    return this._open(ChangePasswordDialogComponent, undefined);
  }
}

@Component({
  selector: 'change-password-dialog',
  templateUrl: './change-password.dialog.html'
})
export class ChangePasswordDialogComponent extends DialogComponentBase<true> {

  constructor(
    private formService: FormService,
    private growlService: GrowlService,
    private userApi: UserApi,
  ) {
    super();
  }

  form = new FormGroup({
    currentPassword: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    newPassword: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    confirmNewPassword: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.userApi.changePassword(this.form.getRawValue()).subscribe(() => {
      this.growlService.growlSuccess("Password changed");
      this.close(true);
    });
  }

  cancel() {
    this.cancel();
  }
}
