import { Component, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { UserApi } from 'api/user.api';
import { Observable } from 'rxjs';
import { User, UserGroup } from '../../../../api/api.types';
import { UserGroupApi } from '../../../../api/user-group.api';
import { FormService } from '../../../../core/form.service';
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

  userGroupDataSource = new SelectDataSource<UserGroup>({
    fetch: (query) => this.userGroupApi.queryUserGroup(query)
  })

  item!: User;

  reset(item: User) {
    this.item = item;
    this.form.patchValue(item);
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    email: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    userGroupId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true })
  });

  save() {
    if (!this.form.valid)
      return;

    this.userApi.updateUser(this.form.getRawValue(), { successGrowl: "User Updated" }).subscribe((result) => {
      this.reset(result);
      this.router.navigate(['user/user-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['user/user-list']);
  }
}
