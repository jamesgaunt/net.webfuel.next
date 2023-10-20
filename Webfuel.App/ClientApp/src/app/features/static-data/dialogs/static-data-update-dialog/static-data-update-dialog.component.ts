import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserGroupApi } from 'api/user-group.api';
import { FormService } from '../../../../core/form.service';

export interface StaticDataUpdateOptions {
  data: any;
  typeName: string;
  enableHidden: boolean;
  enableFreeText: boolean;
}

@Component({
  selector: 'static-data-update-dialog-component',
  templateUrl: './static-data-update-dialog.component.html'
})
export class StaticDataUpdateDialogComponent {

  constructor(
    private dialogRef: DialogRef<any>,
    private formService: FormService,
    @Inject(DIALOG_DATA) public options: StaticDataUpdateOptions,
  ) {
    this.form.patchValue(options.data);
  }

  form = new FormGroup({
    id: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    hidden: new FormControl(false),
    default: new FormControl(false),
    freeText: new FormControl(false),
  });

  save() {
    if (!this.form.valid)
      return;
    this.dialogRef.close(this.form.getRawValue());
  }

  cancel() {
    this.dialogRef.close();
  }
}
