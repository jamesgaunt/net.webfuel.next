import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserActivity } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserActivityApi } from 'api/user-activity.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';

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
    this.form.patchValue(this. data.userActivity);
  }

  form = new FormGroup({
    id: new FormControl<string>('', { nonNullable: true }),
    date: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    workActivityId: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    description: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.userActivityApi.update(this.form.getRawValue(), { successGrowl: "User Activity Updated" }).subscribe((result) => {
      this._closeDialog(result);
    })
  }

  cancel() {
    this._cancelDialog();
  }
}