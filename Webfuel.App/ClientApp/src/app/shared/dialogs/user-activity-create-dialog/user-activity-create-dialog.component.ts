import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { UserActivityApi } from 'api/user-activity.api';
import { StaticDataCache } from 'api/static-data.cache';
import { DialogService } from 'core/dialog.service';
import { UserActivity } from 'api/api.types';

@Injectable()
export class UserActivityCreateDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open() {
    return this.dialogService.openComponent<UserActivity>(UserActivityCreateDialogComponent);
  }
}

@Component({
  templateUrl: './user-activity-create-dialog.component.html'
})
export class UserActivityCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<UserActivity>,
    private formService: FormService,
    private userActivityApi: UserActivityApi,
    public staticDataCache: StaticDataCache
  ) {
  }

  form = new FormGroup({
    date: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    workActivityId: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    description: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.userActivityApi.create(this.form.getRawValue(), { successGrowl: "User Activity Added" }).subscribe((result) => {
      this.dialogRef.close(result);
    })
  }

  cancel() {
    this.dialogRef.close();
  }
}
