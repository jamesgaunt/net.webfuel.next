import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { QueryOp } from 'api/api.enums';
import { Project, Query } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { SupportRequestApi } from 'api/support-request.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';

export interface TriageSupportRequestDialogData {
  id: string;
}

@Injectable()
export class TriageSupportRequestDialog extends DialogBase<Project, TriageSupportRequestDialogData>  {
  open(data: TriageSupportRequestDialogData) {
    return this._open(TriageSupportRequestDialogComponent, data);
  }
}

@Component({
  selector: 'triage-support-request-dialog',
  templateUrl: './triage-support-request.dialog.html'
})
export class TriageSupportRequestDialogComponent extends DialogComponentBase<Project, TriageSupportRequestDialogData>  {

  constructor(
    private router: Router,
    private formService: FormService,
    private supportRequestApi: SupportRequestApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue({ id: this.data.id });
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
        this.cancel();
        return;
      }

      this.close(result);
      this.router.navigateByUrl(`/project/project-item/${result.id}`);
    })
  }

  cancel() {
    this.cancel();
  }

  filterStatus(query: Query) {
    query.filters = query.filters || [];
    query.filters.push({ field: 'default', op: QueryOp.Equal, value: false });
  }
}
