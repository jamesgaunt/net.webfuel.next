import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project } from '../../../api/api.types';
import { UserApi } from '../../../api/user.api';
import { FormService } from '../../../core/form.service';
import { GrowlService } from '../../../core/growl.service';
import { DialogService } from '../../../core/dialog.service';

@Injectable()
export class ChangePasswordDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open() {
    return this.dialogService.openComponent<true>(ChangePasswordDialogComponent);
  }
}

@Component({
  templateUrl: './change-password-dialog.component.html'
})
export class ChangePasswordDialogComponent {

  constructor(
    private dialogRef: DialogRef<true>,
    private formService: FormService,
    private growlService: GrowlService,
    private userApi: UserApi,
  ) {
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
      this.dialogRef.close(true);
    });

  }

  cancel() {
    this.dialogRef.close();
  }
}
