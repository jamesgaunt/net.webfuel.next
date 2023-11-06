import { Component, OnInit } from '@angular/core';
import { FormService } from '../../core/form.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../../core/identity.service';
import { Router } from '@angular/router';
import { GrowlService } from '../../core/growl.service';
import { LoginService } from '../../core/login.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

  constructor(
    private router: Router,
    private loginService: LoginService,
    private formService: FormService,
    private growlService: GrowlService,
  ) {
  }

  errorMessage = "";

  processing = false;

  ngOnInit() {
  }

  form = new FormGroup({
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    password: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  login() {
    if (this.formService.hasErrors(this.form)) {
      this.errorMessage = "Please enter your email address and password";
      return;
    }

    this.errorMessage = "";

    this.processing = true;
    this.loginService.login(this.form.getRawValue()).subscribe({
      next: (result) => {
        if (result) {
          this.router.navigateByUrl("/home");
        } else {
          this.errorMessage = "Invalid email address or password";
        }
        this.processing = false;
      },
      error: (err) => {
        this.errorMessage = "Invalid email address or password";
        this.processing = false;
      }
    });
  }

  forgottenPassword() {
    this.router.navigateByUrl("/forgotten-password");
  }
}
