import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserApi } from 'api/user.api';
import { ProfessionalBackgroundDetail, QueryResult, User } from 'api/api.types';
import { UserGroupApi } from 'api/user-group.api';
import { FormService } from 'core/form.service';
import { TitleApi } from 'api/title.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UpdatePasswordDialog } from '../dialogs/update-password/update-password.dialog';

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
    public staticDataCache: StaticDataCache,
    public updatePasswordDialog: UpdatePasswordDialog,
  ) {
  }

  ngOnInit() {
    this.form.controls.professionalBackgroundId.valueChanges.subscribe((newValue) => {
      this.updateProfessionalBackgroundDetail(newValue);
    });
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
    title: new FormControl('', { nonNullable: true }),
    firstName: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    lastName: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    userGroupId: new FormControl('', { validators: [Validators.required], nonNullable: true }),

    staffRoleId: new FormControl<string | null>(null),
    staffRoleFreeText: new FormControl('', { nonNullable: true }),

    universityJobTitle: new FormControl('', { nonNullable: true }),
    disciplineIds: new FormControl<string[]>([], { nonNullable: true }),
    disciplineFreeText: new FormControl('', { nonNullable: true }),
    siteId: new FormControl<string | null>(null),

    professionalBackgroundId: new FormControl<string | null>(null),
    professionalBackgroundFreeText: new FormControl('', { nonNullable: true }),

    professionalBackgroundDetailId: new FormControl<string | null>(null),
    professionalBackgroundDetailFreeText: new FormControl('', { nonNullable: true }),

    startDateForRSS: new FormControl<string | null>(null),
    endDateForRSS: new FormControl<string | null>(null),
    fullTimeEquivalentForRSS: new FormControl<number | null>(null),

    disabled: new FormControl<boolean>(false, { nonNullable: true }),
    hidden: new FormControl<boolean>(false, { nonNullable: true }),
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
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

  updatePassword() {
    this.updatePasswordDialog.open(this.item);
  }

  // Professional background detail

  professionalBackgroundDetail: ProfessionalBackgroundDetail[] = [];

  updateProfessionalBackgroundDetail(professionalBackgroundId: string | null) {
    this.professionalBackgroundDetail = [];
    this.staticDataCache.professionalBackgroundDetail.query({ skip: 0, take: 1000 }).subscribe((result: QueryResult<ProfessionalBackgroundDetail>) => {
      var applicable = result.items.filter(p => p.professionalBackgroundId == professionalBackgroundId);
      this.professionalBackgroundDetail = applicable;

      if (applicable.find(p => p.id == this.form.value.professionalBackgroundDetailId) === undefined) {
        this.form.patchValue({ professionalBackgroundDetailId: null, professionalBackgroundDetailFreeText: "" });
      }
    });
  }
}
