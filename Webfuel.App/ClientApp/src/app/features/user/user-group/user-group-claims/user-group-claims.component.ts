import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserGroupApi } from 'api/user-group.api';
import { UserGroup } from '../../../../api/api.types';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'user-group-claims',
  templateUrl: './user-group-claims.component.html'
})
export class UserGroupClaimsComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    public userGroupApi: UserGroupApi,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.userGroup);
  }

  item!: UserGroup;

  reset(item: UserGroup) {
    this.item = item;
    this.form.patchValue({ id: item.id });
    this.form.patchValue(item.claims);
    this.sanitizeClaims();
    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    administrator: new FormControl<boolean>(false, { nonNullable: true }),
    canEditUsers: new FormControl<boolean>(false, { nonNullable: true }),
    canEditUserGroups: new FormControl<boolean>(false, { nonNullable: true }),
    canEditStaticData: new FormControl<boolean>(false, { nonNullable: true }),
    canEditReports: new FormControl<boolean>(false, { nonNullable: true }),
    canUnlockProjects: new FormControl<boolean>(false, { nonNullable: true }),
    canTriageSupportRequests: new FormControl<boolean>(false, { nonNullable: true }),
  });

  sanitizeClaims() {
    if (this.form.getRawValue().administrator === true) {
      this.form.patchValue({
        canEditUsers: true,
        canEditUserGroups: true,
        canEditStaticData: true,
        canEditReports: true,
        canUnlockProjects: true,
        canTriageSupportRequests: true,
      })
    }
  }

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
      return;

    this.userGroupApi.updateClaims(this.form.getRawValue(), { successGrowl: "User Group Updated" }).subscribe((result) => {
      this.reset(result);
      if(close)
        this.router.navigate(['user/user-group-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['user/user-group-list']);
  }
}
