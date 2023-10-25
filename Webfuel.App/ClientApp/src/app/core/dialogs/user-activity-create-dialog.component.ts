import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from '../form.service';
import { UserActivityApi } from '../../api/user-activity.api';
import { StaticDataCache } from '../../api/static-data.cache';

export interface IUserActivityCreateDialogOptions {
  callback?: (result: boolean) => void;
}

@Component({
  templateUrl: './user-activity-create-dialog.component.html'
})
export class UserActivityCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<boolean>,
    private formService: FormService,
    private userActivityApi: UserActivityApi,
    public staticDataCache: StaticDataCache,
    @Inject(DIALOG_DATA) public data: IUserActivityCreateDialogOptions | undefined,
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
      this.dialogRef.close();
    })
  }

  cancel() {
    this.dialogRef.close(false);
  }
}
