import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IUser } from 'api/api.types';
import { UserApi } from 'api/user.api';
import { GrowlService } from '../../../../core/growl.service';
import { FormManager } from '../../../../shared/form/form-manager';
import { FormService } from '../../../../core/form.service';
import { ActivatedRoute, ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

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
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.user);
  }

  item!: IUser;

  reset(item: IUser) {
    this.item = item;
    this.formManager.patchValue(item);
  }

  formManager = this.formService.buildManager({
    id: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
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
