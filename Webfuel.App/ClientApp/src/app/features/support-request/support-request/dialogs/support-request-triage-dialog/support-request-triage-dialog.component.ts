import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project, Query, SupportRequest } from 'api/api.types';
import { SupportRequestApi } from 'api/support-request.api';
import { FormService } from 'core/form.service';
import { StaticDataCache } from 'api/static-data.cache';
import { QueryOp } from 'api/api.enums';
import { Router } from '@angular/router';
import { DialogService } from '../../../../../core/dialog.service';

export interface SupportRequestTriageDialogData {
  id: string;
}

@Injectable()
export class SupportRequestTriageDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open(data: SupportRequestTriageDialogData) {
    return this.dialogService.openComponent<Project, SupportRequestTriageDialogData>(SupportRequestTriageDialogComponent, data);
  }
}

@Component({
  selector: 'support-request-triage-dialog',
  templateUrl: './support-request-triage-dialog.component.html'
})
export class SupportRequestTriageDialogComponent {

  constructor(
    private router: Router,
    private dialogRef: DialogRef<Project>,
    private formService: FormService,
    private supportRequestApi: SupportRequestApi,
    public staticDataCache: StaticDataCache,
    @Inject(DIALOG_DATA) public data: SupportRequestTriageDialogData,
  ) {
    this.form.patchValue({ id: data.id });
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

      this.dialogRef.close(result);
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
