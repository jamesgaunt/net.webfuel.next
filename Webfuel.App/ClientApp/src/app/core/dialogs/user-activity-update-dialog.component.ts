import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from '../form.service';
import { UserActivityApi } from '../../api/user-activity.api';
import { StaticDataCache } from '../../api/static-data.cache';
import { UserActivity } from '../../api/api.types';

export interface IUserActivityUpdateDialogOptions {
  userActivity: UserActivity;
  callback?: (result: boolean) => void;
}

@Component({
  templateUrl: './user-activity-update-dialog.component.html'
})
export class UserActivityUpdateDialogComponent {

  constructor(
    private dialogRef: DialogRef<boolean>,
    private formService: FormService,
    private userActivityApi: UserActivityApi,
    public staticDataCache: StaticDataCache,
    @Inject(DIALOG_DATA) public data: IUserActivityUpdateDialogOptions,
  ) {
    this.form.patchValue(data.userActivity);
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
      this.dialogRef.close();
    })
  }

  cancel() {
    this.dialogRef.close(false);
  }
}
