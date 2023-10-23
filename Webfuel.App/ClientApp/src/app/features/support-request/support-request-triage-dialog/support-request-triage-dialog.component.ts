import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Query, SupportRequest } from '../../../api/api.types';
import { SupportRequestApi } from '../../../api/support-request.api';
import { FormService } from '../../../core/form.service';
import { StaticDataCache } from '../../../api/static-data.cache';
import { QueryOp } from '../../../api/api.enums';
import { Router } from '@angular/router';

export interface SupportRequestTriageDialogOptions {
  id: string;
}

@Component({
  selector: 'support-request-triage-dialog',
  templateUrl: './support-request-triage-dialog.component.html'
})
export class SupportRequestTriageDialogComponent {

  constructor(
    private router: Router,
    private dialogRef: DialogRef<SupportRequest>,
    private formService: FormService,
    private supportRequestApi: SupportRequestApi,
    public staticDataCache: StaticDataCache,
    @Inject(DIALOG_DATA) public options: SupportRequestTriageDialogOptions,
  ) {
    this.form.patchValue({ id: options.id });
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: Validators.required, nonNullable: true }),
    statusId: new FormControl<string>(null!, { validators: Validators.required, nonNullable: true })
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.supportRequestApi.triage(this.form.getRawValue()).subscribe((result) => {
      if (result == null) {
        this.dialogRef.close();
        return;
      }

      this.dialogRef.close();
      this.router.navigateByUrl(`/project/project-item/${result.id}`);
    })

  }

  cancel() {
    this.dialogRef.close();
  }

  filterStatus(query: Query) {
    query.filters = query.filters || [];
    query.filters.push({ field: 'default', op: QueryOp.Equal, value: false });
  }
}
