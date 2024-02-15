import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';

export interface UpdateStaticDataData {
  data: any;
  typeName: string;
  enableHidden: boolean;
  enableFreeText: boolean;
  enableAlias: boolean;
}

@Injectable()
export class UpdateStaticDataDialog extends DialogBase<any, UpdateStaticDataData> {
  open(data: UpdateStaticDataData) {
    return this._open(UpdateStaticDataDialogComponent, data);
  }
}

@Component({
  selector: 'update-static-data-dialog',
  templateUrl: './update-static-data.dialog.html'
})
export class UpdateStaticDataDialogComponent extends DialogComponentBase<any, UpdateStaticDataData> {

  constructor(
    private formService: FormService,
  ) {
    super();
    this.form.patchValue(this.data.data);
  }

  form = new FormGroup({
    id: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    hidden: new FormControl(false),
    default: new FormControl(false),
    freeText: new FormControl(false),
    alias: new FormControl('', { nonNullable: true })
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;
    this._closeDialog(this.form.getRawValue());
  }

  cancel() {
    this._cancelDialog();
  }
}
