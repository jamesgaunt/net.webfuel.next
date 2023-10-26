import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IStaticData } from 'api/api.types';
import { DialogService } from 'core/dialog.service';
import { FormService } from 'core/form.service';

export interface StaticDataCreateDialogData{
  typeName: string;
  enableHidden: boolean;
  enableFreeText: boolean;
}

@Injectable()
export class StaticDataCreateDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open<TCreate>(data: StaticDataCreateDialogData) {
    return this.dialogService.openComponent<TCreate, StaticDataCreateDialogData>(StaticDataCreateDialogComponent, data);
  }
}

@Component({
  selector: 'static-data-create-dialog-component',
  templateUrl: './static-data-create-dialog.component.html'
})
export class StaticDataCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<any>,
    private formService: FormService,
    @Inject(DIALOG_DATA) public data: StaticDataCreateDialogData,
  ) {
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
    this.dialogRef.close(this.form.getRawValue());
  }

  cancel() {
    this.dialogRef.close();
  }
}
