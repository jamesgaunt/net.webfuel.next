import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserGroupApi } from 'api/user-group.api';
import { FormService } from '../../../../core/form.service';
import { StaticDataUpdateOptions } from '../static-data-update-dialog/static-data-update-dialog.component';

export interface StaticDataCreateOptions {
  typeName: string;
}

@Component({
  selector: 'static-data-create-dialog-component',
  templateUrl: './static-data-create-dialog.component.html'
})
export class StaticDataCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<any>,
    private formService: FormService,
    @Inject(DIALOG_DATA) public options: StaticDataCreateOptions,
  ) {
  }

  form = new FormGroup({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    code: new FormControl('', { validators: [Validators.required], nonNullable: true }),
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
