import { DialogRef, DIALOG_DATA } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ITenant } from 'api/api.types';
import { TenantApi } from 'api/tenant.api';
import { GrowlService } from '../../../../core/growl.service';
import { FormManager } from '../../../../shared/form/form-manager';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'tenant-create-dialog-component',
  templateUrl: './tenant-create-dialog.component.html'
})
export class TenantCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<ITenant>,
    private formService: FormService,
    private tenantApi: TenantApi,
  ) {
  }

  formManager = this.formService.buildManager({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formManager.hasErrors())
      return;

    this.tenantApi.createTenant(this.formManager.getRawValue(), { successGrowl: "Tenant Created", errorHandler: this.formManager }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
