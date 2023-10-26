import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserGroupApi } from 'api/user-group.api';
import { UserGroup } from '../../../../api/api.types';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'user-group-item',
  templateUrl: './user-group-item.component.html'
})
export class UserGroupItemComponent implements OnInit {

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
    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (!this.form.valid)
      return;

    this.userGroupApi.update(this.form.getRawValue(), { successGrowl: "User Group Updated" }).subscribe((result) => {
      this.reset(result);
      this.router.navigate(['user/user-group-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['user/user-group-list']);
  }
}
