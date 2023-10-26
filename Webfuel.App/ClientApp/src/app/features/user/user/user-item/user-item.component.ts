import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserApi } from 'api/user.api';
import { User } from 'api/api.types';
import { UserGroupApi } from 'api/user-group.api';
import { FormService } from 'core/form.service';
import { TitleApi } from 'api/title.api';
import { StaticDataCache } from 'api/static-data.cache';

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
    public staticDataCache: StaticDataCache
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
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    title: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    firstName: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    lastName: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    userGroupId: new FormControl('', { validators: [Validators.required], nonNullable: true }),

    rssJobTitle: new FormControl('', { nonNullable: true }),
    universityJobTitle: new FormControl('', { nonNullable: true }),
    professionalBackground: new FormControl('', {  nonNullable: true }),
    specialisation: new FormControl('', { nonNullable: true }),
    disciplineIds: new FormControl<string[]>([], { nonNullable: true }),

    startDateForRSS: new FormControl<string | null>(null),
    endDateForRSS: new FormControl<string | null>(null),
    fullTimeEquivalentForRSS: new FormControl<number | null>(null),
    siteId: new FormControl<string | null>(null),

    disabled: new FormControl<boolean>(false, { nonNullable: true }),
    hidden: new FormControl<boolean>(false, { nonNullable: true }),
  });

  save(close: boolean) {
    if (!this.form.valid)
      return;

    this.userApi.update(this.form.getRawValue(), { successGrowl: "User Updated" }).subscribe((result) => {
      this.reset(result);
      if(close)
        this.router.navigate(['user/user-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['user/user-list']);
  }
}
