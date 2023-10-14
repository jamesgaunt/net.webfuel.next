import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserApi } from 'api/user.api';
import { User } from '../../../../api/api.types';
import { UserGroupApi } from '../../../../api/user-group.api';
import { FormService } from '../../../../core/form.service';
import { TitleApi } from '../../../../api/title.api';

@Component({
  selector: 'user-item',
  templateUrl: './user-item.component.html'
})
export class UserItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    public userApi: UserApi,
    public userGroupApi: UserGroupApi,
    public titleApi: TitleApi,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.user);
  }

  item!: User;

  reset(item: User) {
    this.item = item;
    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    email: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    userGroupId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    birthday: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    title: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    multi: new FormControl<string[]>([], { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (!this.form.valid)
      return;

    this.userApi.update(this.form.getRawValue(), { successGrowl: "User Updated" }).subscribe((result) => {
      this.reset(result);
      this.router.navigate(['user/user-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['user/user-list']);
  }
}
