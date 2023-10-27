import { Component, OnInit } from '@angular/core';
import { FormService } from '../../core/form.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../../core/identity.service';
import { Router } from '@angular/router';
import { GrowlService } from '../../core/growl.service';
import { LoginService } from '../../core/login.service';
import { UserLoginApi } from '../../api/user-login.api';

@Component({
  selector: 'forgotten-password',
  templateUrl: './forgotten-password.component.html'
})
export class ForgottenPasswordComponent implements OnInit {

  constructor(
    private router: Router,
    private userLoginApi: UserLoginApi,
    private formService: FormService,
    private growlService: GrowlService,
  ) {
  }

  emailSent = false;

  ngOnInit() {
  }

  form = new FormGroup({
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  forgottenPassword() {
    if (!this.form.valid)
      return;

    this.userLoginApi.sendPasswordResetEmail(this.form.getRawValue()).subscribe((result) => {
      this.emailSent = true;
    })
  }

  returnToLogin() {
    this.router.navigateByUrl("/login");
  }
}
