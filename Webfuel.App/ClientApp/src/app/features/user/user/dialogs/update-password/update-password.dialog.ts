import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { GrowlService } from 'core/growl.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { UserLoginApi } from 'api/user-login.api';
import { User } from '../../../../../api/api.types';

@Injectable()
export class UpdatePasswordDialog extends DialogBase<true> {
  open(user: User) {
    return this._open(UpdatePasswordDialogComponent, user);
  }
}

@Component({
  selector: 'update-password-dialog',
  templateUrl: './update-password.dialog.html'
})
export class UpdatePasswordDialogComponent extends DialogComponentBase<true, User> {

  constructor(
    private formService: FormService,
    private growlService: GrowlService,
    private userLoginApi: UserLoginApi,
  ) {
    super();
    this.form.patchValue({ userId: this.data.id });
  }
   
  form = new FormGroup({
    userId: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    newPassword: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.userLoginApi.updatePassword(this.form.getRawValue()).subscribe(() => {
      this.growlService.growlSuccess("Password changed");
      this._closeDialog(true);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
