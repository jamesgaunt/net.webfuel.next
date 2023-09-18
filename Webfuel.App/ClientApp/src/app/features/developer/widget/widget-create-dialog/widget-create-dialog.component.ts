import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IWidget } from 'api/api.types';
import { WidgetApi } from 'api/widget.api';
import { GrowlService } from '../../../../core/growl.service';
import { FormManager } from '../../../../shared/form/form-manager';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'widget-create-dialog-component',
  templateUrl: './widget-create-dialog.component.html'
})
export class WidgetCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<IWidget>,
    private formService: FormService,
    private widgetApi: WidgetApi,
  ) {
  }

  formManager = this.formService.buildManager({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    age: new FormControl<number>(null!, { validators: [Validators.required, Validators.max(100)], nonNullable: true })
  });

  save() {
    if (this.formManager.hasErrors())
      return;

    this.widgetApi.createWidget(this.formManager.getRawValue(), { successGrowl: "Widget Created", errorHandler: this.formManager }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
