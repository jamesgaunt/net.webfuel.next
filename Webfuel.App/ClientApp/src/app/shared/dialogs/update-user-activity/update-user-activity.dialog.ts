import { Component, DestroyRef, Injectable, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserActivity } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserActivityApi } from 'api/user-activity.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { debounceTime } from 'rxjs';

export interface UpdateUserActivityDialogData {
  userActivity: UserActivity;
}

@Injectable()
export class UpdateUserActivityDialog extends DialogBase<UserActivity, UpdateUserActivityDialogData> {
  open(data: UpdateUserActivityDialogData) {
    return this._open(UpdateUserActivityDialogComponent, data);
  }
}

@Component({
  selector: 'update-user-activity-dialog',
  templateUrl: './update-user-activity.dialog.html'
})
export class UpdateUserActivityDialogComponent extends DialogComponentBase<UserActivity, UpdateUserActivityDialogData>  {

  constructor(
    private formService: FormService,
    private userActivityApi: UserActivityApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue({
      id: this.data.userActivity.id,
      date: this.data.userActivity.date,
      workActivityId: this.data.userActivity.workActivityId!,
      workTimeInHours: this.data.userActivity.workTimeInHours!,
      description: this.data.userActivity.description
    });
    this.toggleFreeText();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { nonNullable: true }),
    date: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    workActivityId: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    workTimeInHours: new FormControl<number>(null!, { validators: [Validators.required, Validators.min(0), Validators.max(8)], nonNullable: true }),
    description: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.userActivityApi.update(this.form.getRawValue(), { successGrowl: "User Activity Updated" }).subscribe((result) => {
      this._closeDialog(result);
    })
  }

  cancel() {
    this._cancelDialog();
  }
}
