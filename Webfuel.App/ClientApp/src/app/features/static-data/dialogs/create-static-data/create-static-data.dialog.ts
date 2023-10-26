import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from '../../../../shared/common/dialog-base';

export interface CreateStaticDataDialogData{
  typeName: string;
  enableHidden: boolean;
  enableFreeText: boolean;
}

@Injectable()
export class CreateStaticDataDialog extends DialogBase<any, CreateStaticDataDialogData> {
  open(data: CreateStaticDataDialogData) {
    return this._open(CreateStaticDataDialogComponent, data);
  }
}

@Component({
  selector: 'create-static-data-dialog',
  templateUrl: './create-static-data.dialog.html'
})
export class CreateStaticDataDialogComponent extends DialogComponentBase<any, CreateStaticDataDialogData> {

  constructor(
    private formService: FormService,
  ) {
    super();
  }

  form = new FormGroup({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    hidden: new FormControl(false),
    default: new FormControl(false),
    freeText: new FormControl(false),
  });

  save() {
    if (!this.form.valid)
      return;
    this._closeDialog(this.form.getRawValue());
  }

  cancel() {
    this._cancelDialog();
  }
}
