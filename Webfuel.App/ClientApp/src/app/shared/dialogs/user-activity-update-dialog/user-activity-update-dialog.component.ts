import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { UserActivityApi } from 'api/user-activity.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserActivity } from 'api/api.types';
import { DialogService } from 'core/dialog.service';

export interface UserActivityUpdateDialogData {
  userActivity: UserActivity;
}

@Injectable()
export class UserActivityUpdateDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open(data: UserActivityUpdateDialogData) {
    return this.dialogService.openComponent<UserActivity, UserActivityUpdateDialogData>(UserActivityUpdateDialogComponent, data);
  }
}


@Component({
  templateUrl: './user-activity-update-dialog.component.html'
})
export class UserActivityUpdateDialogComponent {

  constructor(
    private dialogRef: DialogRef<UserActivity>,
    private formService: FormService,
    private userActivityApi: UserActivityApi,
    public staticDataCache: StaticDataCache,
    @Inject(DIALOG_DATA) public data: UserActivityUpdateDialogData,
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
      this.dialogRef.close(result);
    })
  }

  cancel() {
    this.dialogRef.close();
  }
}
