import { Component, OnInit, inject } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { IQueryUserGroup, IUser, IUserGroup } from 'api/api.types';
import { UserApi } from 'api/user.api';
import { Observable } from 'rxjs';
import { UserGroupApi } from '../../../../api/user-group.api';
import { FormService } from '../../../../core/form.service';
import { GridDataSource } from '../../../../shared/data-source/grid-data-source';
import _ from '../../../../shared/underscore';
import { SelectDataSource } from '../../../../shared/data-source/select-data-source';

@Component({
  selector: 'user-item',
  templateUrl: './user-item.component.html'
})
export class UserItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private userApi: UserApi,
    private userGroupApi: UserGroupApi
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.user);
  }

  userGroupDataSource = new SelectDataSource<IUserGroup, IQueryUserGroup>({
    fetch: (query) => this.userGroupApi.queryUserGroup(_.merge({ search: '' }, query))
  })

  item!: IUser;

  reset(item: IUser) {
    this.item = item;
    this.formManager.patchValue(item);
  }

  formManager = this.formService.buildManager({
    id: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    userGroupId: new FormControl(null!, { validators: [Validators.required], nonNullable: true })
  });

  save() {
    if (this.formManager.hasErrors())
      return;

    this.userApi.updateUser(this.formManager.getRawValue(), { successGrowl: "User Updated", errorHandler: this.formManager }).subscribe((result) => {
      this.router.navigate(['user/user-list']);
    });
  }

  cancel() {
    this.router.navigate(['user/user-list']);
  }
}

export const UserResolver: ResolveFn<IUser> =
  (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IUser> => {
    return inject(UserApi).resolveUser({ id: route.paramMap.get('id')! });
  };
