import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserLoginApi } from '../../../api/user-login.api';


@Component({
  selector: 'user-login',
  templateUrl: './user-login.component.html'
})
export class UserLoginComponent {

  constructor(
    public userLoginApi: UserLoginApi
  ) { }



}
