import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSupport, ProjectSupportFile } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { ConfigurationService } from 'core/configuration.service';
import { FormService } from 'core/form.service';
import { ProjectSupportDialogComponentBase } from 'features/project/project/project-support/common/ProjectSupportDialogComponentBase';
import { DialogBase } from 'shared/common/dialog-base';
import { IsPrePostAwardEnum } from '../../../../../api/api.enums';
import { Validate } from '../../../../../shared/common/validate';

export interface CreateSupportRequestSupportDialogData {
  projectSupportGroupId: string;
}

@Injectable()
export class CreateSupportRequestSupportDialog extends DialogBase<ProjectSupport, CreateSupportRequestSupportDialogData> {
  open(data: CreateSupportRequestSupportDialogData) {
    return this._open(CreateSupportRequestSupportDialogComponent, data, {
      width: '1000px',
      disableClose: true,
    });
  }
}

@Component({
  selector: 'create-support-request-support-dialog',
  templateUrl: './create-support-request-support.dialog.html',
})
export class CreateSupportRequestSupportDialogComponent extends ProjectSupportDialogComponentBase<
  ProjectSupport,
  CreateSupportRequestSupportDialogData
> {
  constructor(
    private formService: FormService,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
    public configurationService: ConfigurationService
  ) {
    super();
    this.form.patchValue({ projectSupportGroupId: this.data.projectSupportGroupId });

    this.configurationService.configuration.subscribe((config) => {
      this.form.patchValue({
        adviserIds: config?.userId == null ? [] : [config.userId],
      });
    });
  }

  form = new FormGroup({
    projectSupportGroupId: new FormControl<string>('', { nonNullable: true }),
    date: new FormControl<string | null>(null),
    teamIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }),
    adviserIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }),
    supportProvidedIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }),
    description: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    workTimeInHours: new FormControl<number>(null!, { validators: [Validators.required, Validators.min(0), Validators.max(8)], nonNullable: true }),
    supportRequestedTeamId: new FormControl<string | null>(null),
    isPrePostAwardId: new FormControl<string>(IsPrePostAwardEnum.PreAward, { nonNullable: true }),

    files: new FormControl<ProjectSupportFile[]>([], { nonNullable: true }),
  });

  save() {
    if (this.submitting) return;
    this.submitting = true;

    if (this.form.valid === false) {
      this.deferredSubmitFiles();
    } else {
      this.submitFiles();
    }
  }

  deferredSubmitFiles() {
    // Sometimes there is a small delay in TinyMCE updating the validity of the form so we wait a second before trying again
    setTimeout(() => {
      if (this.formService.hasErrors(this.form)) {
        this.submitting = false;
        return;
      }
      this.submitFiles();
    }, 250);
  }

  cancel() {
    this._cancelDialog();
  }

  submitForm() {
    this.form.patchValue({ files: this.existingFiles });
    this.projectSupportApi.create(this.form.getRawValue(), { successGrowl: 'Support Added' }).subscribe(
      (result) => {
        this.submitting = false;
        this._closeDialog(result);
      },
      (error) => {
        this.submitting = false;
      }
    );
  }
}
