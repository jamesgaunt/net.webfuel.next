import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IWidget } from 'api/api.types';
import { WidgetApi } from 'api/widget.api';
import { GrowlService } from '../../../../core/growl.service';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'widget-update-dialog-component',
  templateUrl: './widget-update-dialog.component.html'
})
export class WidgetUpdateDialogComponent {

  constructor(
    private dialogRef: DialogRef<IWidget>,
    private widgetApi: WidgetApi,
    private formService: FormService,
    @Inject(DIALOG_DATA) widget: IWidget,
  ) {
    this.formManager.patchValue(widget);
  }

  formManager = this.formService.buildManager({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl<string>(''!, { validators: [Validators.required], nonNullable: true }),
    age: new FormControl<number>(null!, { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formManager.hasErrors())
      return;

    this.widgetApi.updateWidget(this.formManager.getRawValue(), { successGrowl: "Widget Updated", errorHandler: this.formManager }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
