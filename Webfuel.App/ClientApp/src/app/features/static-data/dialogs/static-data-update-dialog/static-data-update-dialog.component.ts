import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserGroupApi } from 'api/user-group.api';
import { FormService } from '../../../../core/form.service';
import { DialogService } from '../../../../core/dialog.service';
import { IStaticData } from '../../../../api/api.types';

export interface StaticDataUpdateData {
  data: any;
  typeName: string;
  enableHidden: boolean;
  enableFreeText: boolean;
}

@Injectable()
export class StaticDataUpdateDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open<TUpdate>(data: StaticDataUpdateData) {
    return this.dialogService.openComponent<TUpdate, StaticDataUpdateData>(StaticDataUpdateDialogComponent, data);
  }
}

@Component({
  selector: 'static-data-update-dialog-component',
  templateUrl: './static-data-update-dialog.component.html'
})
export class StaticDataUpdateDialogComponent {

  constructor(
    private dialogRef: DialogRef<any>,
    private formService: FormService,
    @Inject(DIALOG_DATA) public data: StaticDataUpdateData,
  ) {
    this.form.patchValue(data.data);
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
