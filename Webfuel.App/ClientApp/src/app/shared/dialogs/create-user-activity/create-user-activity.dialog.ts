import { Component, DestroyRef, Injectable, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserActivity } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserActivityApi } from 'api/user-activity.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';
import { debounceTime } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Injectable()
export class CreateUserActivityDialog extends DialogBase<UserActivity> {
  open() {
    return this._open(CreateUserActivityDialogComponent, undefined);
  }
}

@Component({
  selector: 'create-user-activity-dialog',
  templateUrl: './create-user-activity.dialog.html'
})
export class CreateUserActivityDialogComponent extends DialogComponentBase<UserActivity> {

  constructor(
    private formService: FormService,
    private userActivityApi: UserActivityApi,
    public staticDataCache: StaticDataCache
  ) {
    super();
  }

  form = new FormGroup({
    date: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    workActivityId: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    workTimeInHours: new FormControl<number>(null!, { validators: [Validators.required, Validators.min(0), Validators.max(8)], nonNullable: true }),
    description: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.userActivityApi.create(this.form.getRawValue(), { successGrowl: "User Activity Added" }).subscribe((result) => {
      this._closeDialog(result);
    })
  }

  cancel() {
    this._cancelDialog();
  }
}
