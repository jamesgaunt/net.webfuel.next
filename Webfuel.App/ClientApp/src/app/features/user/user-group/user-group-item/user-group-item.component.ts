import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IUserGroup } from 'api/api.types';
import { UserGroupApi } from 'api/user-group.api';
import { GrowlService } from '../../../../core/growl.service';
import { FormManager } from '../../../../shared/form/form-manager';
import { FormService } from '../../../../core/form.service';
import { ActivatedRoute, ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'user-group-item',
  templateUrl: './user-group-item.component.html'
})
export class UserGroupItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private userGroupApi: UserGroupApi,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.userGroup);
  }

  item!: IUserGroup;

  reset(item: IUserGroup) {
    this.item = item;
    this.formManager.patchValue(item);
  }

  formManager = this.formService.buildManager({
    id: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formManager.hasErrors())
      return;

    this.userGroupApi.updateUserGroup(this.formManager.getRawValue(), { successGrowl: "User Group Updated", errorHandler: this.formManager }).subscribe((result) => {
      this.router.navigate(['user/user-group-list']);
    });
  }

  cancel() {
    this.router.navigate(['user/user-group-list']);
  }
}

export const UserGroupResolver: ResolveFn<IUserGroup> =
  (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IUserGroup> => {
    return inject(UserGroupApi).resolveUserGroup({ id: route.paramMap.get('id')! });
  };
