import { Component, OnInit } from '@angular/core';
import { IdentityApi } from '../../api/identity.api';
import { FormService } from '../../core/form.service';
import { FormControl, Validators } from '@angular/forms';
import { IdentityService } from '../../core/identity.service';
import { Router } from '@angular/router';
import { GrowlService } from '../../core/growl.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

  constructor(
    private router: Router,
    private identityService: IdentityService,
    private formService: FormService,
    private growlService: GrowlService,
  ) {
  }

  invalidLogin = false;

  ngOnInit() {
  }

  formManager = this.formService.buildManager({
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    password: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  login() {
    if (this.formManager.hasErrors())
      return;

    console.log("OK");
    this.identityService.login(this.formManager.getRawValue()).subscribe((result) => {
      console.log(result);
      if (result) {
        this.router.navigateByUrl("/home");
      } else {
        this.growlService.growlDanger("Invalid username or password");
      }
    });
  }
}
