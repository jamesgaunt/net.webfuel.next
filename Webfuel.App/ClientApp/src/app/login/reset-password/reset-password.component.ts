import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormService } from '../../core/form.service';
import { GrowlService } from '../../core/growl.service';
import { LoginService } from '../../core/login.service';
import { UserLoginApi } from '../../api/user-login.api';
import { ErrorService } from '../../core/error.service';

@Component({
  selector: 'reset-password',
  templateUrl: './reset-password.component.html'
})
export class ResetPasswordComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userLoginApi: UserLoginApi,
    private formService: FormService,
    private errorService: ErrorService,
  ) {
  }

  errorMessage = "";

  processing = false;

  passwordReset = false;

  ngOnInit() {
    this.form.patchValue({
      userId: this.route.snapshot.params.uid,
      passwordResetToken: this.route.snapshot.params.tid
    })
  }

  form = new FormGroup({
    userId: new FormControl('', { nonNullable: true }),
    passwordResetToken: new FormControl('', { nonNullable: true }),
    newPassword: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    confirmNewPassword: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  resetPassword() {
    if (this.formService.hasErrors(this.form)) {
      this.errorMessage = "Please complete all fields";
      return;
    }

    this.userLoginApi.resetPassword(this.form.getRawValue()).subscribe({
      next: (result) => {
        this.passwordReset = true;
      }, error: (err) => {
        console.log(err);
        this.errorMessage = this.errorService.extractErrorMessage(err);
      }
    });
  }

  returnToLogin() {
    this.router.navigateByUrl("/login");
  }
}
