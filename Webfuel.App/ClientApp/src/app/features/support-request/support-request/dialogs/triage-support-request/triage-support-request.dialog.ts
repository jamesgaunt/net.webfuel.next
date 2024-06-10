import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IsPrePostAwardEnum, QueryOp, SupportRequestStatusEnum } from 'api/api.enums';
import { Project, Query, SupportRequest } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { SupportRequestApi } from 'api/support-request.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';
export interface TriageSupportRequestDialogData {
  id: string;
}

@Injectable()
export class TriageSupportRequestDialog extends DialogBase<SupportRequest, TriageSupportRequestDialogData>  {
  open(data: TriageSupportRequestDialogData) {
    return this._open(TriageSupportRequestDialogComponent, data);
  }
}

@Component({
  selector: 'triage-support-request-dialog',
  templateUrl: './triage-support-request.dialog.html'
})
export class TriageSupportRequestDialogComponent extends DialogComponentBase<SupportRequest, TriageSupportRequestDialogData>  {

  constructor(
    private router: Router,
    private formService: FormService,
    private supportRequestApi: SupportRequestApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue({ id: this.data.id });

    this.staticDataCache.supportProvided.query({ skip: 0, take: 100 }).subscribe((result) => {
      this.form.patchValue({ supportProvidedIds: _.map(_.filter(result.items, (p) => p.default), q => q.id) });
    });

    this.form.controls.statusId.valueChanges.subscribe(value => {
      if (value == SupportRequestStatusEnum.ReferredToNIHRRSSExpertTeams) {
        this.form.controls.workTimeInHours.setValidators([Validators.required, Validators.min(0), Validators.max(8)]);
      }
      else {
        this.form.controls.workTimeInHours.setValidators([]);
      }
      this.form.controls.workTimeInHours.updateValueAndValidity();
    });
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: Validators.required, nonNullable: true }),
    statusId: new FormControl<string>(null!, { validators: Validators.required, nonNullable: true }),
    supportProvidedIds: new FormControl<string[]>([], { nonNullable: true }),
    description: new FormControl<string>('', { nonNullable: true }),
    workTimeInHours: new FormControl<number>(null!, { nonNullable: true }),
    supportRequestedTeamId: new FormControl<string | null>(null),
    isPrePostAwardId: new FormControl<string>(IsPrePostAwardEnum.PreAward, { nonNullable: true }),
    triageNote: new FormControl<string>('', { nonNullable: true }),
  });

  get promoting() {
    return this.form.value.statusId == SupportRequestStatusEnum.ReferredToNIHRRSSExpertTeams;
  }

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.supportRequestApi.triage(this.form.getRawValue()).subscribe((result) => {
      this._closeDialog(result);
      if (result.projectId != null)
        this.router.navigateByUrl(`/project/project-item/${result.projectId}`);
    })
  }

  cancel() {
    this._cancelDialog();
  }

  filterStatus(query: Query) {
    query.filters = query.filters || [];
    query.filters.push({ field: 'default', op: QueryOp.Equal, value: false });
  }
}
